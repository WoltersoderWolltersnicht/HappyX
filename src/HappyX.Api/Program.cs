using HappyX.Api.Middlewares;
using HappyX.Api.Swagger;
using HappyX.Infrastructure.Data;
using HappyX.Infrastructure.Data.EF;

var builder = WebApplication.CreateBuilder(args);

DatabaseOptions databaseOptions = builder.Configuration.GetSection("DatabaseOptions").Get<DatabaseOptions>();

builder.Services.AddDatabase(databaseOptions);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "HappyXApi", Version = "v1" });
    c.OperationFilter<AddRequiredHeaderParameter>();
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HappyXApi v1");
    
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware<ErrorFilterMiddleware>();
app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
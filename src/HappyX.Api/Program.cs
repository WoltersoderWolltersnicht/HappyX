using HappyX.Infrastructure.Data.EF;
using HappyX.Infrastructure.Data.EF.Postgres;

var builder = WebApplication.CreateBuilder(args);

DatabaseOptions databaseOptions = builder.Configuration.GetSection("DataBaseOptions").Get<DatabaseOptions>();

    builder.Services.AddPostgres(databaseOptions);


builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MetadataRestAPI", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetadataRestAPI v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();
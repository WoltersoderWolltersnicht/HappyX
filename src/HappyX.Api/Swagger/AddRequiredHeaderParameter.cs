using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HappyX.Api.Swagger;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        if (context.MethodInfo.DeclaringType?.Name != "StatusController")
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authentication",
                In = ParameterLocation.Header,
                Description = "Auth header",
                Required = true,

                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
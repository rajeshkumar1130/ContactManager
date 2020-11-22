using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ContactManager.Api
{
    /// <summary>
    /// Filter class to add header parameteres to the Swagger file
    /// </summary>
    public class AddRequiredHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();


            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "ClientId",
                In = ParameterLocation.Header,
                Description = "Application Client ID",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
           
        }
    }
}

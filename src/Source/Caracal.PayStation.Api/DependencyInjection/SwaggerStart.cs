using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Caracal.PayStation.Api.DependencyInjection {
    public static class SwaggerStart {
        public static void AddSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Caracal PayStation Api",
                    Description = "Facilitate in the processing of payments.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact {
                        Name = "Ettiene Mare",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/test"),
                    },
                    License = new OpenApiLicense {
                        Name = "MIT",
                        Url = new Uri("https://example.com/license"),
                    },
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerDocs(this IApplicationBuilder app) {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Caracal.PayStation.Api v1");
                c.InjectStylesheet("/swagger-ui/custom.css");
                c.InjectJavascript("/swagger-ui/custom.js");
                c.DocumentTitle = "Caracal Pay Station";
            });
        }
    }
}
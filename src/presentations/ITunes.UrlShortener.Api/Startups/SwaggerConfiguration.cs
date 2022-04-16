using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ITunes.UrlShortener.Api.Startups
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerGenHan(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ITunes.UrlShortener API",
                    Description = "Using for url short",
                    TermsOfService = new Uri("https://github.com/gitunes"),
                    Contact = new OpenApiContact
                    {
                        Name = "gitunes",
                        Email = "itunes@dogusgrubu.microsoft.com",
                        Url = new Uri("https://github.com/gitunes"),
                    }
                });
            });
        }

        public static void UseSwaggerHan(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITunes.UrlShortner Api v1");
            });

        }
    }
}

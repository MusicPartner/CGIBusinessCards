// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Program (Main)
// 
// Description:   
// 
// Changes:
// 1.01.001    First version
// 
// TODO:
// F1000:
// 
// ****************************************************

using CGI.BusinessCards.Web.Api.Infrastructure.Common;
using CGI.BusinessCards.Web.Api.Infrastructure.Configuration;
using CGI.BusinessCards.Web.Api.Services;
using CGI.BusinessCards.Web.Api.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

namespace BusinessCards.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                //Documentation
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CGI BusinessCards",
                    Description = "Web API for CGI BusinessCards backend",
                    Contact = new OpenApiContact
                    {
                        Name = "Keivan Kechmiri",
                        Email = string.Empty
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CGI",
                        Url = new Uri("https://www.cgi.se")
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "CGI BusinessCards",
                    Description = "Web API version 2 for CGI BusinessCards backend",
                    Contact = new OpenApiContact
                    {
                        Name = "Keivan Kechmiri",
                        Email = string.Empty,
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CGI",
                        Url = new Uri("https://www.cgi.se")
                    }
                });

                //Authentication (Web API)//TODO: Add efter JWT Authentication is Implemented

                //Add Auth to UI
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            });

            // Get Secrets and Store Static
            ApplicationSettingsGlobal.businessCardsSwaggerUser = builder.Configuration["CGIBusinessCards:SwaggerAuthenticationUser"] ?? "DonNotUse"; // Default if Not Set
            ApplicationSettingsGlobal.businessCardsSwaggerPassword = builder.Configuration["CGIBusinessCards:SwaggerAuthenticationPassword"] ?? "!DoNotUse"; // Default if Not Set

            // Singletons

            // Transients
            builder.Services.AddTransient<IBusinessCardService, BusinessCardService>();

            // Scoped
            builder.Services.AddScoped<BaseDataAccess>();
            builder.Services.AddScoped<BusinessCardDataAccess>();

            // Initialize
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger Authentication
                app.UseSwaggerAuthorized();

                // Enable Swagger
                app.UseSwagger();

                // Enable SwaggerUI
                app.UseSwaggerUI(options =>
                {
                    SwaggerUIOptionsExtensions.SwaggerEndpoint(options, "/swagger/v1/swagger.json", "CGI BusinessCards API V1");
                    SwaggerUIOptionsExtensions.SwaggerEndpoint(options, "/swagger/v2/swagger.json", "CGI BusinessCards API V2");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
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

using Microsoft.AspNetCore.Builder;

namespace CGI.BusinessCards.Web.Api.Infrastructure.Common
{
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}

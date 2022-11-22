// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Global (ApplicationSettings)
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

using Microsoft.Extensions.Configuration;

namespace CGI.BusinessCards.Web.Api.Infrastructure.Configuration
{
    public static class ApplicationSettingsGlobal
    {
        // Global settings unique to this Application Database
        public const string _appDatabaseConnectionString = "CGI_BusinessCardsDB_ConnectionString";

        // Global Secrets
        public static string businessCardsSwaggerUser { get; set; }
        public static string businessCardsSwaggerPassword { get; set; }

        //Static Configuration
        public static IConfiguration _appConfiguration { get; set; }
    }
}
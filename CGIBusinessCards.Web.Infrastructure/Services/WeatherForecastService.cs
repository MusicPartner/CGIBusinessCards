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

using CGI.BusinessCards.Web.Infrastructure.Helpers;
using CGI.BusinessCards.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CGI.BusinessCards.Web.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/WeatherForecast";

        public WeatherForecastService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Api.Models.Entities.WeatherForecastModel>> Find()
        {
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAsync<List<Api.Models.Entities.WeatherForecastModel>>();
        }
    }
}

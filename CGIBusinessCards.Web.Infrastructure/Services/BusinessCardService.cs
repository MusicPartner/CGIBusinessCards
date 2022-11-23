// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-23
// 
// Module:        Service (BusinessCard)
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

using CGI.BusinessCards.Web.Api.Models.Entities.Dto;
using CGI.BusinessCards.Web.Infrastructure.Extensions;
using CGI.BusinessCards.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CGI.BusinessCards.Web.Services
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/businesscard";

        public BusinessCardService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<BusinessCardDTO>> Get()
        {
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAsync<List<BusinessCardDTO>>();
        }
    }
}

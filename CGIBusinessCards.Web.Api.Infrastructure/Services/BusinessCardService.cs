// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
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

using CGI.BusinessCards.Web.Api.Infrastructure.Common;
using CGI.BusinessCards.Web.Api.Models.Entities;
using CGI.BusinessCards.Web.Api.Services.Interfaces;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;

namespace CGI.BusinessCards.Web.Api.Services
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly ILogger<BusinessCardService> _logger;

        private readonly BusinessCardDataAccess _businessCardDataAccess;

        public BusinessCardService(ILogger<BusinessCardService> logger, BusinessCardDataAccess businessCardDataAccess)
        {
            // Logger
            _logger = logger;

            // DataAccess
            _businessCardDataAccess = businessCardDataAccess;
        }

        public IEnumerable<BusinessCard> GetAll(string pairingToken)
        {
            // Get All BusinessCards            
            var liBusinessCards = new List<BusinessCard>();

            // Get from DB
            liBusinessCards = _businessCardDataAccess.DbGetAll(pairingToken);

            return liBusinessCards;
        }

        public BusinessCard Get(int businessCardId)
        {
            // Get A BusinessCard
            var bcBusinessCard = new BusinessCard();

            // Get from DB
            bcBusinessCard = _businessCardDataAccess.DbGet(businessCardId);

            return bcBusinessCard;
        }
    }
}

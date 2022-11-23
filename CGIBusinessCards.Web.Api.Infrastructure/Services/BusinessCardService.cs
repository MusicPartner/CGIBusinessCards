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
using CGI.BusinessCards.Web.Api.Models.Entities.Dto;
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

        public IEnumerable<BusinessCard> GetAll()
        {
            // Get All BusinessCards            
            var liBusinessCards = new List<BusinessCard>();

            // Get from DB
            liBusinessCards = _businessCardDataAccess.DbGetAll();

            // Return Result
            return liBusinessCards;
        }

        public BusinessCard Get(int iBusinessCardId)
        {
            // Get A BusinessCard
            var bcBusinessCard = new BusinessCard();

            // Get from DB
            bcBusinessCard = _businessCardDataAccess.DbGet(iBusinessCardId);

            // Return Result
            return bcBusinessCard;
        }

        public BusinessCard Add(BusinessCard bcBusinessCard)
        {
            // Add to DB
            BusinessCard bcAddBusinessCardResult = _businessCardDataAccess.DbAdd(bcBusinessCard);

            // Return Result
            return bcAddBusinessCardResult;
        }

        public bool Update(int iBusinessCardId, BusinessCard bcBusinessCard)
        {
            // Update in DB
            bool bUpdateResult = _businessCardDataAccess.DbUpdate(iBusinessCardId, bcBusinessCard);

            // Return Result //TODO Return UpdatedBusinessCard
            return bUpdateResult;
        }

        public bool Delete(int iBusinessCardId)
        {
            // Delete from DB
            bool bDeleteResult = _businessCardDataAccess.DbDelete(iBusinessCardId);

            // Return Result
            return bDeleteResult;
        }
    }
}

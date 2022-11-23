// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        DataAccess (BusinessCard)
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

using CGI.BusinessCards.Web.Api.Infrastructure.Extensions;
using CGI.BusinessCards.Web.Api.Models.Entities;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Data;

namespace CGI.BusinessCards.Web.Api.Infrastructure.Common
{
    public class BusinessCardDataAccess
    {
        private readonly ILogger<BusinessCardDataAccess> _logger;

        private readonly BaseDataAccess _baseDataAccess;

        public BusinessCardDataAccess(ILogger<BusinessCardDataAccess> logger, BaseDataAccess baseDataAccess)
        {
            // Logger
            _logger = logger;

            // DataAccess
            _baseDataAccess = baseDataAccess;
        }

        public List<BusinessCard> DbGetAll()
        {
            var liBusinessCards = new List<BusinessCard>();

            string sSql = "";
            sSql = "SELECT tblBusinessCards.ID AS BusinessCardID, tblBusinessCards.FirstName, tblBusinessCards.LastName, tblBusinessCards.PhoneNumber, tblBusinessCards.Email, tblBusinessCards.Image ";
            sSql += "FROM    tblBusinessCards";

            using (SqlDataReader srdBusinessCards = _baseDataAccess.GetDataReader(sSql, null, CommandType.Text))
            {
                while (srdBusinessCards.Read())
                {
                    var uBusinessCard = new BusinessCard
                    {
                        Id = srdBusinessCards.GetDef<int>("BusinessCardID"),
                        FirstName = srdBusinessCards.GetDef<string>("FirstName").ToString(),
                        LastName = srdBusinessCards.GetDef<string>("LastName"),
                        PhoneNumber = srdBusinessCards.GetDef<string>("PhoneNumber"),
                        Image = srdBusinessCards.GetDef<string>("Image")
                    };

                    liBusinessCards.Add(uBusinessCard);
                }
            }

            return liBusinessCards;
        }

        public BusinessCard DbGet(int iBusinessCardId)
        {
            var bcBusinessCard = new BusinessCard();

            string sSql = "";

            sSql = "SELECT	tblBusinessCards.ID AS BusinessCardID, tblBusinessCards.FirstName, tblBusinessCards.LastName, tblBusinessCards.PhoneNumber, tblBusinessCards.Email, tblBusinessCards.Image ";
            sSql += "FROM    tblBusinessCards ";
            sSql += "WHERE  (tblBusinessCards.ID = @iBusinessCardId)";

            // Add Query Parameters
            List<SqlParameter> parameterGetBusinessCard = new List<SqlParameter>();
            parameterGetBusinessCard.Add(_baseDataAccess.GetParameter("@iBusinessCardId", iBusinessCardId));

            using (SqlDataReader srdBusinessCard = _baseDataAccess.GetDataReader(sSql, parameterGetBusinessCard, CommandType.Text))
            {
                while (srdBusinessCard.Read())
                {
                    bcBusinessCard.Id = srdBusinessCard.GetDef<int>("BusinessCardID");
                    bcBusinessCard.FirstName = srdBusinessCard.GetDef<string>("FirstName");
                    bcBusinessCard.LastName = srdBusinessCard.GetDef<string>("LastName");
                    bcBusinessCard.PhoneNumber = srdBusinessCard.GetDef<string>("PhoneNumber");
                    bcBusinessCard.Image = srdBusinessCard.GetDef<string>("Image");
                }
            }

            return bcBusinessCard;
        }

        public BusinessCard DbAdd(int iBusinessCardId)
        {

            return null;
        }

        public BusinessCard DbUpdate(int iBusinessCardId)
        {

            return null;
        }

        public BusinessCard DbDelete(int iBusinessCardId)
        {

            return null;
        }
    }
}
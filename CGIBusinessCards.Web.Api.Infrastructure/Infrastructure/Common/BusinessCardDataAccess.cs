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

using System;
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
                        Email = srdBusinessCards.GetDef<string>("Email"),
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
                    bcBusinessCard.Email = srdBusinessCard.GetDef<string>("Email");
                    bcBusinessCard.Image = srdBusinessCard.GetDef<string>("Image");
                }
            }

            return bcBusinessCard;
        }

        public BusinessCard DbAdd(BusinessCard bcBusinessCard)
        {
            var bcAddedBusinessCard = new BusinessCard();

            // Add BusinessCard
            string sSql = "INSERT INTO tblBusinessCards (FirstName, LastName, PhoneNumber, Email, Image) ";
            sSql += "VALUES (@sFirstName, @sLastName, @sPhoneNumber, @sEmail, @sImage);";
            sSql += "SELECT SCOPE_IDENTITY()";

            // Add Query Parameters
            List<SqlParameter> parameterAddBusinessCard = new List<SqlParameter>();
            parameterAddBusinessCard.Add(_baseDataAccess.GetParameter("@sFirstName", bcBusinessCard.FirstName));
            parameterAddBusinessCard.Add(_baseDataAccess.GetParameter("@sLastName", bcBusinessCard.LastName));
            parameterAddBusinessCard.Add(_baseDataAccess.GetParameter("@sPhoneNumber", bcBusinessCard.PhoneNumber));
            parameterAddBusinessCard.Add(_baseDataAccess.GetParameter("@sEmail", bcBusinessCard.Email));
            parameterAddBusinessCard.Add(_baseDataAccess.GetParameter("@sImage", bcBusinessCard.Image));

            try
            {
                int iAddBusinessCardID = Convert.ToInt32(_baseDataAccess.ExecuteScalar(sSql, parameterAddBusinessCard, CommandType.Text));

                // Check if Add was Success
                if (iAddBusinessCardID > 0)
                {
                    // Get BusinessCard
                    bcAddedBusinessCard = DbGet(iAddBusinessCardID);
                }
                else
                {
                    // Add BusinessCard Failed
                    _logger.LogError("Database Add Failed: Add BusinessCard"); // Log
                }
            }
            catch (SqlException ex)
            {
                // Log
                _logger.LogError("Database Add Failed: Add BusinessCard", ex);

                // Error Add BusinessCard Return Not Succesful
                return bcAddedBusinessCard;
            }

            return bcAddedBusinessCard;
        }

        public bool DbUpdate(int iBusinessCardId, BusinessCard bcBusinessCard)
        {
            // Validate BusinessCard ID
            if (iBusinessCardId == 0)
            {
                // Not a Valid BusinessCard ID Return Not Succesful 
                return false;
            }

            // Update BusinessCard
            string sSql = "UPDATE tblBusinessCards ";
            sSql += "SET FirstName = @sFirstName, LastName = @sLastName, PhoneNumber = @sPhoneNumber, Email = @sEmail, Image = @sImage ";
            sSql += "WHERE (tblBusinessCards.ID =  @iBusinessCardID)";

            // Add Query Parameters
            List<SqlParameter> parameterUpdateBusinessCard = new List<SqlParameter>();
            parameterUpdateBusinessCard.Add(_baseDataAccess.GetParameter("@iBusinessCardId", iBusinessCardId));
            parameterUpdateBusinessCard.Add(_baseDataAccess.GetParameter("@sFirstName", bcBusinessCard.FirstName));
            parameterUpdateBusinessCard.Add(_baseDataAccess.GetParameter("@sLastName", bcBusinessCard.LastName));
            parameterUpdateBusinessCard.Add(_baseDataAccess.GetParameter("@sPhoneNumber", bcBusinessCard.PhoneNumber));
            parameterUpdateBusinessCard.Add(_baseDataAccess.GetParameter("@sEmail", bcBusinessCard.Email));
            parameterUpdateBusinessCard.Add(_baseDataAccess.GetParameter("@sImage", bcBusinessCard.Image));

            try
            {
                _baseDataAccess.ExecuteNonQuery(sSql, parameterUpdateBusinessCard, CommandType.Text);
            }
            catch (SqlException ex)
            {
                // Log
                _logger.LogError("Database Update Failed: Update BusinessCard", ex);

                // Error Update BusinessCard Return Not Succesful
                return false;
            }

            return true;
        }

        public bool DbDelete(int iBusinessCardId)
        {
            // Validate BusinessCard ID
            if (iBusinessCardId == 0)
            {
                // Not a Valid BusinessCard ID Return Not Succesful 
                return false;
            }

            // Update BusinessCard
            string sSql = "DELETE FROM tblBusinessCards WHERE ID = @iBusinessCardId";

            // Add Query Parameters
            List<SqlParameter> parameterDeleteBusinessCard = new List<SqlParameter>();
            parameterDeleteBusinessCard.Add(_baseDataAccess.GetParameter("@iBusinessCardId", iBusinessCardId));

            try
            {
                _baseDataAccess.ExecuteNonQuery(sSql, parameterDeleteBusinessCard, CommandType.Text);
            }
            catch (SqlException ex)
            {
                // Log
                _logger.LogError("Database Delete Failed: Delete BusinessCard", ex);

                // Error Delete BusinessCard Return Not Succesful
                return false;
            }

            return true;
        }
    }
}
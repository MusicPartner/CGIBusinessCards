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

        public List<BusinessCard> DbGetAll(string pairingToken)
        {
            var liBusinessCards = new List<BusinessCard>();

            string sSql = "";
            sSql = "SELECT	{tblCustomer}.ID AS CustomerID, {tblCustomer}.Name AS CustomerName, {tblCommonFacilityPrefix}UserDetails.FacilityToken, {tblCommonFacilityPrefix}UserDetails.Pincode ";
            sSql += "FROM    {tblCommonFacilityPrefix}Facilities INNER JOIN ";
            sSql += "        {tblCommonFacilityPrefix}FacilityUsers ON {tblCommonFacilityPrefix}Facilities.ID = {tblCommonFacilityPrefix}FacilityUsers.FacilityID INNER JOIN ";
            sSql += "        {tblCustomer} ON {tblCommonFacilityPrefix}FacilityUsers.CustomerID = {tblCustomer}.ID LEFT OUTER JOIN ";
            sSql += "        {tblCommonFacilityPrefix}UserDetails ON {tblCommonFacilityPrefix}FacilityUsers.CustomerID = {tblCommonFacilityPrefix}UserDetails.CustomerID ";
            sSql += "WHERE   ({tblCommonFacilityPrefix}Facilities.PairingToken = @PairingToken)";

            // Add Query Parameters
            List<SqlParameter> parameterListBusinessCards = new List<SqlParameter>();
            parameterListBusinessCards.Add(_baseDataAccess.GetParameter("@PairingToken", pairingToken));

            using (SqlDataReader srdBusinessCards = _baseDataAccess.GetDataReader(sSql, parameterListBusinessCards, CommandType.Text))
            {
                while (srdBusinessCards.Read())
                {
                    var uBusinessCard = new BusinessCard
                    {
                        FirstName = srdBusinessCards.GetDef<int>("CustomerID").ToString(),
                        LastName = srdBusinessCards.GetDef<string>("FacilityToken"),
                        PhoneNumber = srdBusinessCards.GetDef<string>("CustomerName"),
                        Image = srdBusinessCards.GetDef<string>("Pincode")
                    };

                    liBusinessCards.Add(uBusinessCard);
                }
            }

            return liBusinessCards;
        }

        public BusinessCard DbGet(int businessCardId)
        {
            var uBusinessCard = new BusinessCard();

            string sSql = "";

            // Add Query Parameters
            List<SqlParameter> parameterCountBusinessCardDetail = new List<SqlParameter>();
            parameterCountBusinessCardDetail.Add(_baseDataAccess.GetParameter("@businessCardId", businessCardId));

            int iCountBusinessCardDetail = (int)_baseDataAccess.ExecuteScalar("SELECT Count(*) FROM {tblCommonFacilityPrefix}UserDetails WHERE CustomerID = @userId", parameterCountBusinessCardDetail, CommandType.Text);

            //Check if we found a BusinessCardDetail, if not Add
            if (iCountBusinessCardDetail == 0)
            {
                //Check if UserDetails are missing
                sSql = "INSERT INTO {tblCommonFacilityPrefix}UserDetails (CustomerID) ";
                sSql += "VALUES (@businessCardId)";

                // Add Query Parameters
                List<SqlParameter> parameterAddBusinessCardDetail = new List<SqlParameter>();
                parameterAddBusinessCardDetail.Add(_baseDataAccess.GetParameter("@businessCardId", businessCardId));

                try
                {
                    _baseDataAccess.ExecuteNonQuery(sSql, parameterAddBusinessCardDetail, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    // Log
                    _logger.LogError(ex.Message, ex);

                    // Error Add UserDetail Return Not Succesful
                    return null;
                }
            }

            sSql = "SELECT {tblCustomer}.ID AS CustomerID, {tblCustomer}.Name AS CustomerName, {tblCommonFacilityPrefix}UserDetails.FacilityToken, {tblCommonFacilityPrefix}UserDetails.Pincode ";
            sSql += "FROM   {tblCustomer} INNER JOIN ";
            sSql += "       {tblCommonFacilityPrefix}UserDetails ON {tblCustomer}.ID = {tblCommonFacilityPrefix}UserDetails.CustomerID ";
            sSql += "WHERE  ({tblCommonFacilityPrefix}UserDetails.CustomerID = @userId)";

            // Add Query Parameters
            List<SqlParameter> parameterListBusinessCard = new List<SqlParameter>();
            parameterListBusinessCard.Add(_baseDataAccess.GetParameter("@businessCardId", businessCardId));

            using (SqlDataReader srdBusinessCard = _baseDataAccess.GetDataReader(sSql, parameterListBusinessCard, CommandType.Text))
            {
                while (srdBusinessCard.Read())
                {
                    uBusinessCard.FirstName = srdBusinessCard.GetDef<int>("CustomerID").ToString();
                    uBusinessCard.LastName = srdBusinessCard.GetDef<string>("FacilityToken");
                    uBusinessCard.PhoneNumber = srdBusinessCard.GetDef<string>("CustomerName");
                    uBusinessCard.Image = srdBusinessCard.GetDef<string>("Pincode");
                }
            }

            return uBusinessCard;
        }
    }
}
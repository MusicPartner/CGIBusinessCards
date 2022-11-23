// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Interface (BusinessCardService)
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

using CGI.BusinessCards.Web.Api.Models.Entities;
using CGI.BusinessCards.Web.Api.Models.Entities.Dto;

using System.Collections.Generic;

namespace CGI.BusinessCards.Web.Api.Services.Interfaces
{
    public interface IBusinessCardService
    {
        IEnumerable<BusinessCard> GetAll();
        BusinessCard Get(int iBusinessCardId);
        BusinessCard Add(BusinessCardDTO bcdDusinessCard);
        BusinessCard Update(BusinessCardDTO bcdDusinessCard);
        BusinessCard Delete(int iBusinessCardId);
    }
}
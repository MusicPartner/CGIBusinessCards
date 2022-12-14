// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-23
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

using CGI.BusinessCards.Web.Api.Models.Entities.Dto;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CGI.BusinessCards.Web.Services.Interfaces
{
    public interface IBusinessCardService
    {
        Task<IEnumerable<BusinessCardDTO>> Get();
    }
}

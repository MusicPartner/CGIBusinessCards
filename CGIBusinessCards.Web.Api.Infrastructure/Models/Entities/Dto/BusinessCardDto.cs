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

using System.ComponentModel.DataAnnotations;

namespace CGI.BusinessCards.Web.Api.Models.Entities.Dto
{
    public class BusinessCardDTO
    {
        [Required]
        public string FirstNAme { get; set; }

        [Required]
        public string LastNAme { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string Image { get; set; }
    }
}

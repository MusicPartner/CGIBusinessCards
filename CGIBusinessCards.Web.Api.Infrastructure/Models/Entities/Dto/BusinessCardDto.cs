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
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstNAme { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastNAme { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }
    }
}

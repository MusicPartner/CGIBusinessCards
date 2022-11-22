// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Entities (BusinessCard)
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

using System;
using System.ComponentModel.DataAnnotations;

namespace CGI.BusinessCards.Web.Api.Models.Entities
{
    public class BusinessCard
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
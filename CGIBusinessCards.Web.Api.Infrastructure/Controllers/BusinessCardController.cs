// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Controller (BusinessCardController)
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
using CGI.BusinessCards.Web.Api.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Linq;

namespace CGI.BusinessCards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessCardController : ControllerBase
    {
        private readonly ILogger<BusinessCardController> _logger;

        private readonly IBusinessCardService _businessCardService;

        public BusinessCardController(ILogger<BusinessCardController> logger, IBusinessCardService businessCardService)
        {
            // Logger
            _logger = logger;

            // Service
            _businessCardService = businessCardService;
        }

        [HttpGet(Name = "GetBusinessCard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get()
        {
            // Get All BusinessCards
            IEnumerable<BusinessCard> businessCards = _businessCardService.GetAll("");

            //Check if NotFound
            if (businessCards.Count() == 0)
            {
                // Log
                _logger.LogInformation("Get BusinessCards Not Found");

                // Return NotFound
                return new NotFoundResult();
            }

            // Convert to DTO
            List<BusinessCardDTO> businessCardsDTO = new List<BusinessCardDTO>();

            // Loop and Convert
            foreach (BusinessCard businessCard in businessCards)
            {
                businessCardsDTO.Add(new BusinessCardDTO
                {
                    FirstNAme = businessCard.FirstName,
                    LastNAme = businessCard.LastName,
                    PhoneNumber = businessCard.PhoneNumber,
                    Email = businessCard.Email,
                    Image = businessCard.Image
                });
            }

            //Return List of BusinessCards (DTO)
            return Ok(businessCardsDTO);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(BusinessCardDTO businessCardDTO)
        {
            if (businessCardDTO.FirstNAme.Contains("XYZ Widget"))
            {
                // Log
                _logger.LogWarning("Create BusinessCard Failed");

                // Return Bad Request
                return new BadRequestResult();
            }

            //_productContext.Products.Add(product);
            //await _productContext.SaveChangesAsync();

            return CreatedAtAction("", "", null, null); //nameof(GetById_IActionResult), new { id = product.Id }, product
        }
    }
}
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

        [HttpGet(Name = "GetBusinessCards")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get()
        {
            // Get All BusinessCards
            IEnumerable<BusinessCard> businessCards = _businessCardService.GetAll();

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

        [HttpGet("{iBusinessCardId:int}", Name = "GetBusinessCard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int iBusinessCardId)
        {
            // Validate BusinessCard ID
            if (iBusinessCardId == 0)
            {
                // Log
                _logger.LogWarning("Get BusinessCard Invalid Id");

                // Return BadRequest
                return BadRequest();
            }

            // Get All BusinessCards
            BusinessCard bcBusinessCards = _businessCardService.Get(iBusinessCardId);

            //Check if NotFound
            if (bcBusinessCards.Id == 0)
            {
                // Log
                _logger.LogInformation("Get BusinessCard Not Found");

                // Return NotFound
                return new NotFoundResult();
            }

            // Convert to DTO
            BusinessCardDTO bcdBusinessCardDTO = new BusinessCardDTO()
            {
                FirstNAme = bcBusinessCards.FirstName,
                LastNAme = bcBusinessCards.LastName,
                PhoneNumber = bcBusinessCards.PhoneNumber,
                Email = bcBusinessCards.Email,
                Image = bcBusinessCards.Image
            };

            //Return List of BusinessCard (DTO)
            return Ok(bcdBusinessCardDTO);
        }

        [HttpPost(Name = "AddBusinessCard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Add([FromBody]BusinessCardDTO bcBusinessCardDTO)
        {
            if (bcBusinessCardDTO == null)
            {
                // Log
                _logger.LogWarning("Create BusinessCard Failed");

                // Return Bad Request
                return BadRequest(bcBusinessCardDTO);
            }

            if (bcBusinessCardDTO.Id > 0)
            {
                // Log
                _logger.LogError("Create BusinessCard Invalid Id");

                // Return Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //_productContext.Products.Add(product);
            //await _productContext.SaveChangesAsync();

            return CreatedAtAction("", "", null, null); //nameof(GetById_IActionResult), new { id = product.Id }, product
        }

        [HttpPut("{iBusinessCardId:int}", Name = "UpdateBusinessCard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int iBusinessCardId, [FromBody] BusinessCardDTO bcBusinessCardDTO)
        {
            if (bcBusinessCardDTO == null)
            {
                // Log
                _logger.LogWarning("Create BusinessCard Failed");

                // Return Bad Request
                return BadRequest(bcBusinessCardDTO);
            }

            if (bcBusinessCardDTO.Id > 0)
            {
                // Log
                _logger.LogError("Create BusinessCard Invalid Id");

                // Return Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //_productContext.Products.Add(product);
            //await _productContext.SaveChangesAsync();

            return CreatedAtAction("", "", null, null); //nameof(GetById_IActionResult), new { id = product.Id }, product
        }

        [HttpDelete("{iBusinessCardId:int}", Name = "DeleteBusinessCard")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int iBusinessCardId)
        {

            return null;
        }
        
    }
}
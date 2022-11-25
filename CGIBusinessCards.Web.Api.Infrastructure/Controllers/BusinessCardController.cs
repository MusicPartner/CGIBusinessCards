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
        public ActionResult GetBusinessCards()
        {
            // Get All BusinessCards
            IEnumerable<BusinessCard> businessCards = _businessCardService.GetAll();

            // Check if NotFound
            if (businessCards.Count() == 0)
            {
                // Log
                _logger.LogInformation("Get BusinessCards Not Found");

                // Return NotFound
                return StatusCode(StatusCodes.Status404NotFound);
            }

            // Convert to DTO
            List<BusinessCardDTO> businessCardsDTO = new List<BusinessCardDTO>();

            // Loop and Convert
            foreach (BusinessCard businessCard in businessCards)
            {
                businessCardsDTO.Add(new BusinessCardDTO
                {
                    Id = businessCard.Id,
                    FirstName = businessCard.FirstName,
                    LastName = businessCard.LastName,
                    PhoneNumber = businessCard.PhoneNumber,
                    Email = businessCard.Email,
                    Image = businessCard.Image
                });
            }

            // Return List of BusinessCards (DTO)
            return Ok(businessCardsDTO);
        }

        [HttpGet("{iBusinessCardId:int}", Name = "GetBusinessCard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetBusinessCard(int iBusinessCardId)
        {
            // Validate BusinessCard ID
            if (iBusinessCardId == 0)
            {
                // Log
                _logger.LogWarning("Get BusinessCard Invalid Id");

                // Return BadRequest
                return BadRequest();
            }

            // Get BusinessCard
            BusinessCard bcBusinessCards = _businessCardService.Get(iBusinessCardId);

            //Check if NotFound
            if (bcBusinessCards.Id == 0)
            {
                // Log
                _logger.LogInformation("Get BusinessCard Not Found");

                // Return NotFound
                return StatusCode(StatusCodes.Status404NotFound);
            }

            // Convert to DTO
            BusinessCardDTO bcdBusinessCardDTO = new BusinessCardDTO()
            {
                Id = bcBusinessCards.Id,
                FirstName = bcBusinessCards.FirstName,
                LastName = bcBusinessCards.LastName,
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
        public ActionResult AddBusinessCard([FromBody]BusinessCardDTO bcBusinessCardDTO)
        {
            // Validate Input
            if (bcBusinessCardDTO == null)
            {
                // Log
                _logger.LogWarning("Add BusinessCard Failed");

                // Return Bad Request
                return BadRequest(bcBusinessCardDTO);
            }

            // Validate Input ID
            if (bcBusinessCardDTO.Id != 0)
            {
                // Log
                _logger.LogError("Add BusinessCard Invalid Id");

                // Return Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Convert from DTO
            BusinessCard bcBusinessCard = new BusinessCard()
            {
                FirstName = bcBusinessCardDTO.FirstName,
                LastName = bcBusinessCardDTO.LastName,
                PhoneNumber = bcBusinessCardDTO.PhoneNumber,
                Email = bcBusinessCardDTO.Email,
                Image = bcBusinessCardDTO.Image
            };

            // Add BusinessCard
            BusinessCard bcAddBusinessCardResult = _businessCardService.Add(bcBusinessCard);

            // Validate if Succesful
            if (bcAddBusinessCardResult.Id == 0)
            {
                // Log
                _logger.LogError("Add BusinessCard Failed");

                // Return Bad Request
                return BadRequest();
            }

            return CreatedAtRoute(nameof(GetBusinessCard), new { iBusinessCardId = bcAddBusinessCardResult.Id }, bcAddBusinessCardResult);
        }

        [HttpPut("{iBusinessCardId:int}", Name = "UpdateBusinessCard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateBusinessCard(int iBusinessCardId, [FromBody] BusinessCardDTO bcBusinessCardDTO)
        {
            // Validate
            if (bcBusinessCardDTO == null)
            {
                // Log
                _logger.LogWarning("Update BusinessCard Failed");

                // Return Bad Request
                return BadRequest(bcBusinessCardDTO);
            }

            // Validate ID
            if (iBusinessCardId == 0)
            {
                // Log
                _logger.LogError("Update BusinessCard Invalid Id");

                // Return Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Convert from DTO
            BusinessCard bcBusinessCard = new BusinessCard()
            {
                FirstName = bcBusinessCardDTO.FirstName,
                LastName = bcBusinessCardDTO.LastName,
                PhoneNumber = bcBusinessCardDTO.PhoneNumber,
                Email = bcBusinessCardDTO.Email,
                Image = bcBusinessCardDTO.Image
            };

            // Update BusinessCard
            bool bResultAddBusinessCard = _businessCardService.Update(iBusinessCardId, bcBusinessCard);

            // Check if Update Failed
            if (bResultAddBusinessCard == false)
            {
                // Log
                _logger.LogWarning("Update BusinessCard Failed");

                // Return Bad Request
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{iBusinessCardId:int}", Name = "DeleteBusinessCard")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteBusinessCard(int iBusinessCardId)
        {
            // Validate ID
            if (iBusinessCardId == 0)
            {
                // Log
                _logger.LogError("Delete BusinessCard Invalid Id");

                // Return Internal Server Error
                return BadRequest();
            }

            // Validate ID Exists
            BusinessCard bcBusinessCards = _businessCardService.Get(iBusinessCardId);
            if (bcBusinessCards.Id == 0)
            {
                // Log
                _logger.LogInformation("Delete BusinessCard Not Found");

                // Return NotFound
                return StatusCode(StatusCodes.Status404NotFound);
            }

            // Delete BusinessCard
            bool bResultDeleteBusinessCard = _businessCardService.Delete(iBusinessCardId);

            // Check if Delete Failed
            if (bResultDeleteBusinessCard == false)
            {
                // Log
                _logger.LogWarning("Delete BusinessCard Failed");

                // Return Bad Request
                return BadRequest();
            }

            // Return Result
            return NoContent();
        }
    }
}
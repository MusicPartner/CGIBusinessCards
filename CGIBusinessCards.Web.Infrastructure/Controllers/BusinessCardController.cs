// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-23
// 
// Module:        Controller (BusinessCard)
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

using CGI.BusinessCards.Web.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace CGI.BusinessCards.Web.Controllers
{
    public class BusinessCardController : Controller
    {
        private readonly IBusinessCardService _service;

        public BusinessCardController(IBusinessCardService service)
        {
            _service = service ?? throw new ArgumentException(nameof(service));
        }

        // GET: BusinessCardController
        public async Task<IActionResult> BusinessCardIndex()
        {
            // Get BusinessCards from API
            var bcBusinessCards = await _service.Get();

            return View(bcBusinessCards);
        }

        // GET: BusinessCardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BusinessCardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessCardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BusinessCardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BusinessCardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BusinessCardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BusinessCardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

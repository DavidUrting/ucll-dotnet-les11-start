using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using AdventureWorks.Web.Areas.Sales.Models;
using AdventureWorks.Web.Models;
using AdventureWorks.Web.Models.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Net;

namespace AdventureWorks.Web.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class CustomerController : Controller
    {
        private const int MAX_REQUESTS_PER_MINUTE = 100;

        private IWebHostEnvironment _webHostEnvironment;
        private ICustomerManager _manager;

        public CustomerController(IWebHostEnvironment webHostEnvironment, ICustomerManager manager)
        {
            _webHostEnvironment = webHostEnvironment;
            _manager = manager;
        }

        [Route("zoeken")]
        public IActionResult Search()
        {
            int requestsPastMinute = 0; // TODO: aantal requests in laatste minuut van huidige user bepalen.
            if (requestsPastMinute > MAX_REQUESTS_PER_MINUTE)
            {
                return StatusCode((int)HttpStatusCode.TooManyRequests);
            }
            SearchViewModel vm = new SearchViewModel();

            SetCustomerOfTheDay();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Search(SearchQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Keyword)
                && char.IsDigit(query.Keyword[0]))
            {
                ModelState.AddModelError("keyword", "A search keyword should not start with a digit.");
            }

            if (ModelState.IsValid)
            {
                var customers = _manager.SearchCustomers(query.Keyword)
                    .Take(query.MaxResults)
                    .Select(c => new CustomerViewModel()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email
                    });
                return Json(customers);
            }
            else return BadRequest(ModelState);
        }

        // Tonen van details van een klant
        public IActionResult Details(int id)
        {
            Customer c = _manager.GetCustomer(id);
            if (c != null)
            {
                CustomerViewModel vm = new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email
                };
                SetCustomerOfTheDay();
                return View(vm);
            }
            else return NotFound();
        }

        public ActionResult Create()
        {
            SetCustomerOfTheDay();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Customer c = new Customer()
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Email = collection["Email"]
                };

                _manager.InsertCustomer(c);

                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Customer c = _manager.GetCustomer(id);
            CustomerViewModel vm = new CustomerViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            };

            SetCustomerOfTheDay();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Customer c = _manager.GetCustomer(id);

                c.FirstName = collection["FirstName"];
                c.LastName = collection["LastName"];
                c.Email = collection["Email"];

                _manager.UpdateCustomer(c);

                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Customer c = _manager.GetCustomer(id);
            CustomerViewModel vm = new CustomerViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            };

            SetCustomerOfTheDay();
            return View(vm);
        }

        // POST: Test/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _manager.DeleteCustomer(id);

                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult BlankOffer()
        {
            string fileOnServer = Path.Combine(
                _webHostEnvironment.ContentRootPath, @"Documents\offer_nl.pdf");
            return File(
                System.IO.File.ReadAllBytes(fileOnServer),
                "application/pdf");
        }

        public IActionResult SearchV2()
        {
            return RedirectToAction("Search");
        }

        public IActionResult CustomerOfTheDay()
        {
            SetCustomerOfTheDay();
            return PartialView();
        }

        private void SetCustomerOfTheDay()
        {
            // TODO: data ophalen en viewmodel gebruiken i.p.v. ViewBag
            ViewBag.FirstName = "Lucy";
            ViewBag.Description = "...";
        }
    }
}
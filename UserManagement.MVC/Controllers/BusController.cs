using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.MVC.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class BusController : Controller
    {
        private readonly ILogger<BusController> _logger;
        BusDataAccessLayer BusContext = new BusDataAccessLayer();
        ReviewDataAccessLayer ReviewContext = new ReviewDataAccessLayer();

        public BusController(ILogger<BusController> logger)
        {
            _logger = logger;
        }

        

        public IActionResult Index()
        {
            List<Bus> buses = new List<Bus>();
            buses = BusContext.GetAllBuses().ToList();
            return View(buses);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> AddBus(Bus bus)
        {

            if (!ModelState.IsValid)
            {
                return View(bus);
            }
            if (bus.model != null)
            {             
                BusContext.AddBus(bus.model, bus.hourPrice, bus.maxPeople);
            }
            return RedirectToAction("AllBuses");
            
        }
        public IActionResult AllBuses()
        {
            List<Bus> buses = new List<Bus>();
            buses = BusContext.GetAllBuses().ToList();
            return View(buses);
        }
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteBus(int busID)
        {
            if (busID != null)
            {
                BusContext.DeleteBus(busID);
            }
            return RedirectToAction("AllBuses");
        }

        [AllowAnonymous]
        public IActionResult ViewBus(int Id)
        {
            Bus restaurant = new Bus();
            restaurant = BusContext.GetBuses(Id);
            List<Review> Reviews = new List<Review>();
            Reviews = ReviewContext.GetAllReviews(Id).ToList();
            BusReviewsModel restaurantReviewsModel = new BusReviewsModel();
            restaurantReviewsModel.Bus = restaurant;
            restaurantReviewsModel.Reviews = Reviews;
            return View(restaurantReviewsModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}

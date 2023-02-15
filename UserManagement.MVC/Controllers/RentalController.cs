using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;

namespace UserManagement.MVC.Controllers
{
   
    public class RentalController : Controller
    {

        private readonly ILogger<RentalController> _logger;
        RentalDataAccessLayer RentalContext = new RentalDataAccessLayer();
        RentCategoryDataAccessLayer CategoryContext = new RentCategoryDataAccessLayer();
        BusDataAccessLayer BusContext = new BusDataAccessLayer();

        public RentalController(ILogger<RentalController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index(int Id, string userID)
        {

            RentCategory Category = new RentCategory();
            Category = CategoryContext.GetCategory(Id);

            Bus Bus = new Bus();
            Bus = BusContext.GetBuses(Id);

            Rental Rental = new Rental();
            Rental = RentalContext.GetRental(userID);



            List<RentCategory> rentCategor = new List<RentCategory>();
            var allCategor = CategoryContext.GetAllCategory();

            List<Bus> buses = new List<Bus>();
            var allBuses = BusContext.GetAllBuses();

            List<Rental> rentals = new List<Rental>();
            var allrentals = RentalContext.GetAllRental();


            List<Rental> rental = new List<Rental>();
            rental = RentalContext.GetAllRental().ToList();

            List<Bus> bus = new List<Bus>();
            bus = BusContext.GetAllBuses().ToList();

            List<RentCategory> category = new List<RentCategory>();
            category = CategoryContext.GetAllCategory().ToList();

            int lengthRental = rental.Count;
            int lengthBus = bus.Count;
            int lengthCategory = category.Count;

            int j = 0;
            int k = 0;


            List<Bus> testBusy = new List<Bus>();
            List<Rental> testRental = new List<Rental>();


            foreach (var item in rental)
            {
                if (userID == Rental.userID) testRental.Add(item);
            }

            foreach (var testy in bus)
            {
                foreach (var item in rental)
                {
                    if (item.busID == testy.busID) testBusy.Add(testy);
                }

            }

            RentalModel rentalModel = new RentalModel();
            rentalModel.RentalList = testRental;
            rentalModel.BusList = testBusy;
            rentalModel.RentCategoryList = rentCategor;
            rentalModel.Bus = Bus;
            rentalModel.Rental = Rental;
            rentalModel.RentCategory = Category;

            return View(rentalModel);

        }


        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult ViewRental()
        {
            List<Bus> buses = new List<Bus>();
            buses = BusContext.GetAllBuses().ToList();

            List<RentCategory> rentCategory = new List<RentCategory>();
            rentCategory = CategoryContext.GetAllCategory().ToList();

            List<Rental> rental = new List<Rental>();
            rental = RentalContext.GetAllRental().ToList();

            int length = rental.Count;

            RentalModel rentalModel = new RentalModel();
            rentalModel.BusList = buses;
            rentalModel.RentalList = rental;
            rentalModel.RentCategoryList = rentCategory;

            return View(rentalModel);


        }
        [Authorize]
        public IActionResult RentForm(int busID, int categoryID, string rentalID, string email)
        {

            Bus bus = new Bus();
            bus = BusContext.GetBuses(busID);

            RentCategory category = new RentCategory();
            category = CategoryContext.GetCategory(categoryID);

            
            Rental rental = new Rental();
            rental.busID = bus.busID;
            rental.typeID = category.typeID;
            rental.finalPrice = category.priceForHour * bus.hourPrice;
            rental.userID = rentalID;
 

            return View(rental);

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRental(Rental rental)
        {

            if (!ModelState.IsValid)
            {
                return View(rental);
            }
            if (rental != null)
            {
                RentalContext.AddRental(rental.startDate,rental.pickupLocation,rental.userID,rental.busID, rental.description, rental.typeID,rental.Hours, rental.finalPrice);
            }


            return RedirectToAction("Index");
        }
        public IActionResult Wynik(Rental rental)
        {
            float price = rental.finalPrice * rental.Hours;

            ViewBag.Name1 = string.Format("startDate: {0} ", rental.startDate);
            ViewBag.Name2 = string.Format("Hours: {0} ", rental.Hours);
            ViewBag.Name3 = string.Format("pickupLocation: {0} ", rental.pickupLocation);
            ViewBag.Name4 = string.Format("descripiton: {0} ", rental.description);
            ViewBag.Name5 = string.Format("userID: {0} ", rental.userID);
            ViewBag.Name6 = string.Format("typeID: {0} ", rental.typeID);
            ViewBag.Name7 = string.Format("price for hour: {0} ", rental.finalPrice);
            ViewBag.Name8 = string.Format("finalPrice: {0} ", price);
            ViewBag.Name9 = string.Format("busID: {0} ", rental.busID);
    


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Controllers
{
    public class RentCategoryController : Controller
    {


        private readonly ILogger<RentCategoryController> _logger;
        RentCategoryDataAccessLayer CategoryContext = new RentCategoryDataAccessLayer();
        BusDataAccessLayer BusContext = new BusDataAccessLayer();
        ReviewDataAccessLayer ReviewContext = new ReviewDataAccessLayer();

        public RentCategoryController(ILogger<RentCategoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Bus> buses = new List<Bus>();
            buses = BusContext.GetAllBuses().ToList();

            List<RentCategory> rentCategory = new List<RentCategory>();
            rentCategory = CategoryContext.GetAllCategory().ToList();

            CategoryBuses categoryBuses = new CategoryBuses();
            categoryBuses.Buses = buses;
            categoryBuses.RentCategories = rentCategory;

            return View(categoryBuses);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string restaurantName, int priceForHour, string description)
        {
            if (restaurantName != null)
            {
                CategoryContext.AddCategory(restaurantName, priceForHour, description);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ViewRentCategory(int Id)
        {
            RentCategory Category = new RentCategory();
            Category = CategoryContext.GetCategory(Id);
            List<Review> Reviews = new List<Review>();
            Reviews = ReviewContext.GetAllReviews(Id).ToList();
            CategoryReviewsModel categoryReviewsModel = new CategoryReviewsModel();
            categoryReviewsModel.RentCategory = Category;
            categoryReviewsModel.Reviews = Reviews;

            List<Bus> buses = new List<Bus>();
            buses = BusContext.GetAllBuses().ToList();


            return View(categoryReviewsModel);


        }
        public IActionResult BusCategory(int Id)
        {
            RentCategory Category = new RentCategory();
            Category = CategoryContext.GetCategory(Id);

            List<Bus> buses = new List<Bus>();
            buses = BusContext.GetAllBuses().ToList();

            CategoryBusModel categoryBusModel = new CategoryBusModel();

            categoryBusModel.RentCategory = Category;
            categoryBusModel.Bus = buses;

            return View(categoryBusModel);


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        ReviewDataAccessLayer ReviewContext = new ReviewDataAccessLayer();

        public ReviewController(ILogger<ReviewController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int busID, string comment, string userId)
        {
            if (busID != null && comment != null)
            {
                ReviewContext.AddReview(busID, userId, comment );
            }
            return RedirectToAction("ViewBus", "Bus", new {Id= busID });
        }

        
        public async Task<IActionResult> DeleteReview(string Id, string bus)
        {
            if (Id != null)
            {
                ReviewContext.DeleteReview(Id);
            }
            return RedirectToAction("ViewBus", "Bus", new { Id = bus });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

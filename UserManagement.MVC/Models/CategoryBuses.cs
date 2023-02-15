using System.Collections.Generic;

namespace UserManagement.MVC.Models
{
    public class CategoryBuses
    {
        public IEnumerable<UserManagement.MVC.Models.Bus> Buses { get; set; }
        public IEnumerable<UserManagement.MVC.Models.RentCategory> RentCategories { get; set; }
    }

}


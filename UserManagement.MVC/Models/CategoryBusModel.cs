using System.Collections.Generic;

namespace UserManagement.MVC.Models
{
    public class CategoryBusModel
    {
        public RentCategory RentCategory { get; set; }
        public List<Bus> Bus { get; set; }

        public CategoryBusModel()
        {
            this.RentCategory = new RentCategory();
            this.Bus = new List<Bus>();
        }
    }
}

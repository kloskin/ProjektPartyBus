using System.Collections.Generic;
using System.Diagnostics;

namespace UserManagement.MVC.Models
{
    public class RentalModel
    {
        
        public Rental Rental {  get; set; }
        public List<Rental> RentalList { get; set; }
        public RentCategory RentCategory { get; set; }

        public List<RentCategory> RentCategoryList { get; set; }
        public List<Bus> BusList { get; set; }
        public Bus Bus { get; set; }

        public RentalModel()
        {
            this.Rental = new Rental();
            this.RentalList = new List<Rental>();
            this.RentCategory = new RentCategory();
            this.RentCategoryList = new List<RentCategory>();
            this.Bus = new Bus();
            this.BusList = new List<Bus>();

        }
        

        public IEnumerable<UserManagement.MVC.Models.Bus> Buses { get; set; }
        public IEnumerable<UserManagement.MVC.Models.RentCategory> RentCategories { get; set; }

        public IEnumerable<UserManagement.MVC.Models.Rental> Rentals { get; set; }


    }
}

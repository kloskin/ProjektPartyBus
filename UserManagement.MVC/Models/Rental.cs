using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class Rental
    {
        public int rentID { get; internal set; }

        public string startDate { get; set; }

        public int Hours { get; set; }

        public string description { get; set; }
        public string pickupLocation { get; set; }
        public string userID { get; set; }
        public int busID { get; set; }

        public float finalPrice { get; set; }
        public int typeID { get; set; }


    }
}

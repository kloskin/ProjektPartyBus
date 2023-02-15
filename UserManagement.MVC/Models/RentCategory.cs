using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class RentCategory
    {
        public int typeID { get; internal set; }
        public string categoryName { get; set; }


        public int priceForHour { get; set; }
        public string description { get; set; }

    }
}

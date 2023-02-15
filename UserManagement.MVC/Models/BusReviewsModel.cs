using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class BusReviewsModel
    {
        public Bus Bus { get; set; }
        public List<Review> Reviews { get; set; }

        public BusReviewsModel()
        {
            this.Bus = new Bus();
            this.Reviews = new List<Review>();
        }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int typeID { get; set; }
        public string busID { get; set; }
        public string Comment { get; set; }
        public string? User { get; set; }

    }
}

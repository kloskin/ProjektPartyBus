using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.MVC.Models
{
    public class Bus
    {
        public int busID { get; internal set; }
        public string model { get; set; }

        public float hourPrice { get; set; }

        public int maxPeople { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Account : TableRecord
    {
        public int AccountId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int HousingNumber { get; set; }

        public int FlatNumber { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
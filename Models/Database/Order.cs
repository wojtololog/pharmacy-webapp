using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Order : TableRecord
    {
        public int OrderId { get; set; }

        public DateTime Date { get; set; }

        public string TotalCost { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
        
        public List<OrderedMedicine> Orders { get; set; } = new List<OrderedMedicine>();
    }
}
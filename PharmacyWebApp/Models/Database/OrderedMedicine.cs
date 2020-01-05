using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class OrderedMedicine : TableRecord
    {
        public int OrderedMedicineId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int PriceId { get; set; }

        public Price Price { get; set; }

        public int OrdersNumber { get; set; }
    }
}
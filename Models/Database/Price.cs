using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Price : TableRecord
    {
        public int PriceId { get; set; }

        [Display(Name = "Cena (zł)")]

        [RegularExpression(@"^\d{1,10}[,]{0,1}\d{0,2}$", ErrorMessage = "Separatorem dziesiętnym jest przecinek.")]
        public string Value { get; set; }

        public int PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public int SizeId { get; set; }

        [Display(Name = "Wielkość")]

        public Size Size { get; set; }

        public List<OrderedMedicine> OrderedMedicines { get; set; } = new List<OrderedMedicine>();
    }
}
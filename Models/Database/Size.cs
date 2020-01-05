using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Size : TableRecord
    {
        public int SizeId { get; set; }

        [Display(Name = "Wielkość")]
        [Required(ErrorMessage = "Uzupełnij pole")]
        public string Value { get; set; }

        [Display(Name = "Jednostka")]
        [Required(ErrorMessage = "Uzupełnij pole")]
        public string Unit { get; set; }

        [Display(Name = "Obraz")]
        public byte[] Image { get; set; }

        [Display(Name = "Lek")]
        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; }

        public List<Price> Prices { get; set; } = new List<Price>();
    }
}
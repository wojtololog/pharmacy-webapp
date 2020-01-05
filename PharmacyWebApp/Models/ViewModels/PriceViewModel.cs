using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyWebApp.Models
{
    public class PriceViewModel
    {

        public int NumberOfNewRecords { get; set; }

        public string Message { get; set; }

        [Display(Name = "Klucz zabezpieczeń apteki")]
        public string PharmacyKey { get; set; }

        public int PharmacyId { get; set; }

        public List<string> SelectedMedicines { get; set; }

        [Display(Name = "Cena(zł)")]
        public List<Price> Prices { get; set; }

        [Display(Name = "Wielkość")]
        public List<List<SelectListItem>> SizesLists{get;set;}

        [Display(Name = "Lek")]
        public List<Medicine> Medicines { get; set; }

    }
}
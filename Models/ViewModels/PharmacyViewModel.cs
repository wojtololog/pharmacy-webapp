using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PharmacyWebApp.Models
{
    public class RegisterPharmacyViewModel
    {
        public Pharmacy Pharmacy { get; set; }

        public string SelectedProvinceId { get; set; }

        public string SelectedCityId { get; set; }
        
        public List<SelectListItem> Cities { get; set; } 
        public Province SelectedProvince { get; set; }

        public List<SelectListItem> Provinces { get; set; }
    }
}
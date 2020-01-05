using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    
    public class City : TableRecord
    {
        public int CityId { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Musisz wybrać województwo")]
        [Display(Name = "Województwo")]
        public Province Province { get; set; }

        public List<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();
    }
}
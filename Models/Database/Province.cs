using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Province : TableRecord
    {
        public int ProvinceId { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
    }
}
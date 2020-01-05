using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Concentration : TableRecord
    {
        public int ConcentrationId { get; set; }

        public string Value { get; set; }

        public string Unit { get; set; }

        public int ComponentId { get; set; }

        public Component Component { get; set; }

        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class AplicationMethod : TableRecord
    {
        public int AplicationMethodId { get; set; }

        public string Name { get; set; }

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();

    }
}
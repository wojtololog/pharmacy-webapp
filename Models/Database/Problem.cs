using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Problem : TableRecord
    {
        public int ProblemId { get; set; }

        public string Name { get; set; }

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Component : TableRecord
    {
        public int ComponentId { get; set; }

        public string Name { get; set; }

    }
}
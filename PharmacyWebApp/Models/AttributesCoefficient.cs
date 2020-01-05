using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class AttributesCoefficient
    {
        public double price = 1;
        public double pregnacy = 0.1;
        public double prescription = 0.12;
        public double components = 0.09;
        
        public double age = 0.09;
        public double size = 0.03;
        public double bodyPart = 0.085;

        public double aplicationMethod = 0.04;
        public double distance = 0.004;
        public double form = 0.08;

        public double pharmacyOpen = 0.003;
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class SearchResult
    {
        public double score;
        public Price price;

        public List<double> priceGrades;
        public List<double> distanceGrades;
        public List<double> sizeGrades;


        public SearchResult(Price price)
        {
            this.price = price;
            score = double.Parse(price.Value, CultureInfo.InvariantCulture);
        }
    }
}
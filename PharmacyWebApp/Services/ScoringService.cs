using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
using System.Globalization;

namespace PharmacyWebApp.Services
{
    public class ScoringService
    {
        private SearchViewModel model;
        private AttributesCoefficient attribute = new AttributesCoefficient();
        private List<Price> pricesList;
        private List<SearchResult> searchResults;

        private Membership distanceMembership;
        private Membership priceMembership;

        private Membership SizeSztMembership;
        private Membership SizeMlMembership;
        private Membership SizeMgMembership;


        public ScoringService(List<Price> prices, SearchViewModel model)
        {
            this.model = model;
            pricesList = prices;

            PreparePriceSections();
            PrepareDistanceSections();
            PrepareSizeSections();

            ScoreResults();
        }

        public List<SearchResult> GetResults()
        {
            return searchResults;
        }

        private void PreparePriceSections()
        {
            double min = pricesList.Min(x => double.Parse(x.Value, CultureInfo.InvariantCulture));
            double max = pricesList.Max(x => double.Parse(x.Value, CultureInfo.InvariantCulture));

            priceMembership = new Membership(min, max);
        }

        private void PrepareDistanceSections()
        {
            distanceMembership = new Membership(0, 2000);
            distanceMembership.sections.Last().c = double.MaxValue;
            distanceMembership.sections.Last().d = double.MaxValue;

        }

        private void PrepareSizeSections()
        {
            double min;
            double max;

            List<Price> list = pricesList.Where(x => x.Size.Unit.Equals("szt")).ToList();
            if (list.Count > 0)
            {
                min = list.Min(x => double.Parse(x.Size.Value));
                max = list.Max(x => double.Parse(x.Size.Value));
                SizeSztMembership = new Membership(min, max);
            }

            list = pricesList.Where(x => x.Size.Unit.Equals("ml")).ToList();
            if (list.Count > 0)
            {
                min = list.Min(x => double.Parse(x.Size.Value));
                max = list.Max(x => double.Parse(x.Size.Value));
                SizeMlMembership = new Membership(min, max);
            }

            list = pricesList.Where(x => x.Size.Unit.Equals("g")).ToList();
            if (list.Count > 0)
            {
                min = list.Min(x => double.Parse(x.Size.Value));
                max = list.Max(x => double.Parse(x.Size.Value));
                SizeMgMembership = new Membership(min, max);
            }
        }

        private void ScoreResults()
        {
            searchResults = new List<SearchResult>();

            foreach (Price p in pricesList)
            {
                SearchResult sr = new SearchResult(p);

                sr.score = attribute.price * double.Parse(p.Value);
                sr.priceGrades = priceMembership.CalculateSectionsMembership(double.Parse(p.Value));

                var sCoord = new GeoCoordinate(double.Parse(p.Pharmacy.Latitude),
                    double.Parse(p.Pharmacy.Longitude));

                double x = double.Parse(model.UserLatitude,
                    CultureInfo.InvariantCulture);

                double y = double.Parse(model.UserLongitude,
                    CultureInfo.InvariantCulture);

                var eCoord = new GeoCoordinate(x, y);

                double distance = sCoord.GetDistanceTo(eCoord);
                sr.distanceGrades = distanceMembership.CalculateSectionsMembership(distance);
                sr.score += attribute.distance * distance;

                sr.sizeGrades = CalculateSizeMembership(p);
                sr.score += CalculateSizeScore(sr.sizeGrades);

                sr.score += CalculatePenalty(sr);

                searchResults.Add(sr);
            }
        }

        public List<double> CalculateSizeMembership(Price p)
        {
            switch (p.Size.Unit)
            {
                case "szt":
                    return SizeSztMembership.CalculateSectionsMembership(double.Parse(p.Size.Value));

                case "ml":
                    return SizeMlMembership.CalculateSectionsMembership(double.Parse(p.Size.Value));

                case "g":
                    return SizeMgMembership.CalculateSectionsMembership(double.Parse(p.Size.Value));
            }

            return null;
        }

        public double CalculateSizeScore(List<double> list)
        {
            switch (model.SelectedSize)
            {
                case "xs":
                    return attribute.size * (1 - list[0]);

                case "s":
                    return attribute.size * (1 - list[1]);

                case "m":
                    return attribute.size * (1 - list[2]);

                case "l":
                    return attribute.size * (1 - list[3]);

                case "xl":
                    return attribute.size * (1 - list[4]);

                default:
                    break;
            }

            return 0;
        }

        public double CalculatePenalty(SearchResult sr)
        {
            double penalty = 0;
            Price p = sr.price;

            for (int i = 0; i < distanceMembership.sections.Count; ++i)
            {
                double distanceBPoint = distanceMembership.sections[i].b * sr.distanceGrades[i];

                if (distanceBPoint == 0)
                {
                    continue;
                }

                for (int j = 0; j < priceMembership.sections.Count; ++j)
                {
                    double priceBPoint = priceMembership.sections[j].b * sr.priceGrades[j];

                    if(priceBPoint == 0)
                    {
                        continue;
                    }

                    Medicine med = p.Size.Medicine;


                    if (!model.SelectedAgeCategory.Equals("-1") && med.AgeCategories.Where(x => model.SelectedAgeCategory.Equals(x.AgeCategoryId.ToString())) == null)
                    {
                        penalty += attribute.age * priceBPoint
                            + attribute.age * distanceBPoint;
                    }

                    if (!model.SelectedForm.Equals("-1") && !model.SelectedForm.Equals(med.FormId.ToString()))
                    {
                        penalty += attribute.form * priceBPoint
                            + attribute.form * distanceBPoint;
                    }

                    if (!model.SelectedAplicationMethod.Equals("-1") && !model.SelectedAplicationMethod.Equals(med.AplicationMethodId.ToString()))
                    {
                        penalty += attribute.aplicationMethod * priceBPoint
                            + attribute.aplicationMethod * distanceBPoint;
                    }

                    if (!model.SelectedBodyPart.Equals("-1") && med.BodyParts.Where(x => model.SelectedBodyPart.Equals(x.BodyPartId.ToString())) != null)
                    {
                        penalty += attribute.bodyPart * priceBPoint
                            + attribute.bodyPart * distanceBPoint;
                    }

                    if (model.SelectedComponents!= null && med.Concentrations.Where(x => model.SelectedComponents.Any(c => c.Equals(x.ComponentId.ToString()))) != null)
                    {
                        penalty += attribute.components * priceBPoint
                            + attribute.components * distanceBPoint;
                    }

                    if (model.Prescription == true && med.AvailableWithoutPrescription == false)
                    {
                        penalty += attribute.prescription * priceBPoint
                            + attribute.prescription * distanceBPoint;
                    }

                    if (model.PregnancyUseable == true && med.PregnancyUseable == true)
                    {
                        penalty += attribute.pregnacy * priceBPoint
                            + attribute.pregnacy * distanceBPoint;
                    }

                    penalty += checkTime(i, sr);
                }
               
            }

            return penalty;
        }

        private double checkTime(int i, SearchResult sr)
        {
            DateTime dt = DateTime.Now;

            TimeSpan start = TimeSpan.Zero;
            TimeSpan end = TimeSpan.Zero;
            TimeSpan now = DateTime.Now.TimeOfDay;

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (sr.price.Pharmacy.WorkingHour.MondayFrom != null && sr.price.Pharmacy.WorkingHour.MondayFrom != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.MondayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.MondayTo);
                    }
                    break;

                case DayOfWeek.Tuesday:
                    if (sr.price.Pharmacy.WorkingHour.TuesdayFrom != null && sr.price.Pharmacy.WorkingHour.TuesdayTo != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.TuesdayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.TuesdayTo);
                    }
                    break;

                case DayOfWeek.Wednesday:

                    if (sr.price.Pharmacy.WorkingHour.WednesdayFrom != null && sr.price.Pharmacy.WorkingHour.WednesdayTo != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.WednesdayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.WednesdayTo);
                    }
                    break;

                case DayOfWeek.Thursday:
                    if (sr.price.Pharmacy.WorkingHour.ThursdayFrom != null && sr.price.Pharmacy.WorkingHour.ThursdayTo != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.ThursdayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.ThursdayTo);
                    }
                    break;

                case DayOfWeek.Friday:
                    if (sr.price.Pharmacy.WorkingHour.FridayFrom != null && sr.price.Pharmacy.WorkingHour.FridayTo != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.FridayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.FridayTo);
                    }
                    break;

                case DayOfWeek.Saturday:
                    if (sr.price.Pharmacy.WorkingHour.SaturdayFrom != null && sr.price.Pharmacy.WorkingHour.SaturdayTo != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.SaturdayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.SaturdayTo);
                    }
                    break;

                case DayOfWeek.Sunday:
                    if (sr.price.Pharmacy.WorkingHour.SundayFrom != null && sr.price.Pharmacy.WorkingHour.SundayTo != null)
                    {
                        start = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.SundayFrom);
                        end = TimeSpan.Parse(sr.price.Pharmacy.WorkingHour.SundayTo);
                    }
                    break;

                default: break;
            }

            if (start == TimeSpan.Zero || (now > end || now < start))
            {
                return attribute.pharmacyOpen * priceMembership.sections[i].b * sr.priceGrades[i]
                + attribute.pharmacyOpen * distanceMembership.sections[i].b * sr.distanceGrades[i];
            }

            return 0;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PharmacyWebApp.Models;
using System.Data.Entity.Migrations;

namespace PharmacyWebApp.Services
{
    public class DatabaseService
    {
        public List<Province> LoadProvinces()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Provinces.Include("Cities").ToList();
            }
        }

        public List<City> LoadCities()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Cities.Include("Province").ToList();
            }
        }

        public List<City> LoadCitiesForProvince(int provinceId)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Cities.Include("Province").Where(x => x.ProvinceId == provinceId).ToList();
            }
        }

        public List<Component> LoadAllComponents()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Components.ToList();
            }
        }

        public List<Medicine> LoadAllMedicines()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Medicines.IncludeAll().Include("Sizes.Prices").OrderBy(x=>x.Name).ToList();
            }
        }

        public List<Size> LoadAllSizes()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Sizes.Include("Medicine")
                    .Include("Prices")
                    .Include("Medicine.Form")
                    .Include("Medicine.Concentrations")
                    .Include("Medicine.Concentrations.Component")
                    .Include("Medicine.BodyParts")
                    .Include("Medicine.Problems")
                    .Include("Medicine.AgeCategories")
                    .Include("Medicine.Pharmacy")
                    .Include("Medicine.AplicationMethod").ToList();
            }
        }

        public List<Problem> LoadAllProblems()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Problems.Include("Medicines").ToList();
            }
        }

        public List<Pharmacy> LoadAllPharmacies()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Pharmacies.ToList();
            }
        }

        public List<AgeCategory> LoadAgeCategories()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.AgeCategories.ToList();
            }
        }

        public List<Form> LoadAllForms()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Forms.ToList();
            }
        }

        public List<BodyPart> LoadAllBodyParts()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.BodyParts.ToList();
            }
        }

        public List<AplicationMethod> LoadAplicationMethods()
        {
            using (var context = new PharmacyDBContext())
            {
                return context.AplicationMethods.ToList();
            }
        }

        public List<Price> LoadPricesForPharmacy(int id)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Prices.Include("Size").Where(x => x.PharmacyId == id).ToList();
            }
        }

        public Size FindSizeById(int id)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Sizes.Include("Prices").Where(x => x.SizeId == id).First();
            }
        }

        public List<Price> LoadPricesForProblem(int id)
        {
            using (var context = new PharmacyDBContext())
            {

                List<Medicine> medicines = context.Medicines.IncludeAll().Include("Concentrations.Component")
                    .Where(x => x.Problems.Any(prob => prob.ProblemId == id)).ToList();


                List<Price> list = context.Prices.IncludeAll()
                    .Include("Size.Medicine")
                    .Include("Size.Medicine.Problems")
                    .Include("Size.Medicine.AplicationMethod")
                    .Include("Size.Medicine.Form")
                    .Include("Size.Medicine.AgeCategories")
                    .Include("Size.Medicine.BodyParts")
                    .Include("Size.Medicine.Concentrations")
                    .Include("Size.Medicine.Concentrations.Component")
                    .Include("Pharmacy.WorkingHour")
                    .Include("Size.Prices")
                    .ToList();

                list = list.Where(x => medicines.Any(med => med.MedicineId == x.Size.MedicineId)).ToList();
                return list;
            }
        }

        public int FindPharmacyIdByKey(string key)
        {
            using (var context = new PharmacyDBContext())
            {
                Pharmacy p = context.Pharmacies.Where(x => x.PharmacyKey.Equals(key) && x.RegistrationConfirmed == true).First();
                if (p == null)
                {
                    return -1;
                }
                else
                {
                    return p.PharmacyId;
                }
            }
        }

        public Medicine FindMedicineById(int id)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Medicines.Include("Sizes").Where(x => x.MedicineId == id).First();
            }
        }

        public List<Problem> FindProblemsByIds(List<string> ids)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.Problems.Where(x => ids.Contains(x.ProblemId.ToString())).ToList();
            }
        }

        public List<AgeCategory> FindAgeCategoriesByIds(List<string> ids)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.AgeCategories.Where(x => ids.Contains(x.AgeCategoryId.ToString())).ToList();
            }
        }

        public List<BodyPart> FindBodyPartsByIds(List<string> ids)
        {
            using (var context = new PharmacyDBContext())
            {
                return context.BodyParts.Where(x => ids.Contains(x.BodyPartId.ToString())).ToList();
            }
        }

        public void AddNewPharmacyToDb(Pharmacy newPharmacy)
        {
            using (var context = new PharmacyDBContext())
            {
                context.Pharmacies.AddOrUpdate(newPharmacy);
                context.WorkingHours.AddOrUpdate(newPharmacy.WorkingHour);
                context.SaveChanges();
            }
        }

        public void AddNewMedicineToDb(Medicine newMedicine)
        {
            using (var context = new PharmacyDBContext())
            {
                context.Medicines.AddOrUpdate(newMedicine);

                foreach (Problem p in newMedicine.Problems)
                {
                    context.Problems.Attach(p);
                }

                foreach (AgeCategory ac in newMedicine.AgeCategories)
                {
                    context.AgeCategories.Attach(ac);
                }

                foreach (BodyPart bp in newMedicine.BodyParts)
                {
                    context.BodyParts.Attach(bp);
                }

                context.SaveChanges();
            }
        }

        public void AddNewMedicineContainerToDb(Size size)
        {
            using (var context = new PharmacyDBContext())
            {
                context.Sizes.AddOrUpdate(size);
                context.SaveChanges();
            }
        }

        public void UpdatePrice(Price price)
        {
            using (var context = new PharmacyDBContext())
            {
                Price p = context.Prices.ToList().Find(x => x.PriceId == price.PriceId);

                if (p == null)
                {
                    context.Prices.Add(price);
                }
                else
                {
                    p.Value = price.Value;
                    context.Prices.AddOrUpdate(p);
                }

                context.SaveChanges();
            }
        }

        public Pharmacy ConfirmEmailForPharmacy(int id)
        {
            using (var context = new PharmacyDBContext())
            {
                Pharmacy p = context.Pharmacies.ToList().Find(x => x.PharmacyId == id);
                if (p == null)
                {
                    return null;
                }
                else
                {
                    p.RegistrationConfirmed = true;
                    context.Pharmacies.AddOrUpdate(p);
                    context.SaveChanges();

                    return p;
                }
            }
        }
    }
}
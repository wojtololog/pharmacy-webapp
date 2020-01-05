using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyWebApp.Services;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PharmacyWebApp.Controllers
{
    public class PharmacyController : BaseController
    {
        // GET: Pharmacy
        [AllowAnonymous]
        public ActionResult RegisterPharmacy()
        {
            RegisterPharmacyViewModel model = new RegisterPharmacyViewModel
            {
                Pharmacy = new Pharmacy()
                {
                    WorkingHour = new WorkingHour(),

                },
                SelectedProvince = new Province(),
                Cities = PrepareCityList(),
                Provinces = PrepareProvinceList(),
            };

            return View(model);
        }

        public List<SelectListItem> PrepareCityList()
        {
            List<City> cities = dbService.LoadCitiesForProvince(1);
            List<SelectListItem> slt = new List<SelectListItem>();
            foreach (City city in cities)
            {
                slt.Add(new SelectListItem
                {
                    Text = city.Name,
                    Value = city.CityId.ToString()

                });
            }
            return slt;
        }

        [HttpGet]
        public ActionResult PrepareCityList(string provinceId)
        {
            List<City> cities = dbService.LoadCitiesForProvince(int.Parse(provinceId));
            List<SelectListItem> slt = new List<SelectListItem>();
            foreach (City city in cities)
            {
                slt.Add(new SelectListItem
                {
                    Text = city.Name,
                    Value = city.CityId.ToString()

                });
            }
            return Json(slt, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> PrepareProvinceList()
        {
            List<Province> provinces = dbService.LoadProvinces();
            List<SelectListItem> slt = new List<SelectListItem>();
            foreach (Province province in provinces)
            {
                slt.Add(new SelectListItem
                {
                    Text = province.Name,
                    Value = province.ProvinceId.ToString()

                });
            }
            return slt;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterPharmacy(RegisterPharmacyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Pharmacy newPharmacy = model.Pharmacy;
                newPharmacy.CityId = int.Parse(model.SelectedCityId);
                newPharmacy.PharmacyKey = keyService.GenerateKey(40);

                dbService.AddNewPharmacyToDb(newPharmacy);

                var callbackUrl = Url.Action("CompletePharmacyRegistration", "Pharmacy", new
                {
                    pharmacyId = newPharmacy.PharmacyId,

                    code = keyService.GenerateKey(25)
                }, protocol: Request.Url.Scheme);

                await mailService.SendConfirmation(newPharmacy.Email, callbackUrl);
                
                return RedirectToAction("ConfirmEmail", "Pharmacy");
            }
            else
            {
                model.Cities = PrepareCityList();
                model.Provinces = PrepareProvinceList();
                return View(model);
            }
        }


        [AllowAnonymous]
        public async Task<ActionResult> CompletePharmacyRegistration(int pharmacyId, string code)
        {
            Pharmacy p = dbService.ConfirmEmailForPharmacy(pharmacyId);
            await mailService.SendGeneratedKey(p.Email, p.PharmacyKey);
            return View(model: p.PharmacyKey);
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail()
        {
            return View();
        }
    }
}
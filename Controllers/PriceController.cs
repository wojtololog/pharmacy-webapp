using PharmacyWebApp.Models;
using PharmacyWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PharmacyWebApp.Controllers
{
    public class PriceController : BaseController
    {
        [AllowAnonymous]
        public ActionResult LoadPrices()
        {
            PriceViewModel model = new PriceViewModel();
            
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadPrices(PriceViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PharmacyId = dbService.FindPharmacyIdByKey(model.PharmacyKey);
                if(model.PharmacyId != -1)
                {
                    TempData["PriceViewModel"] = model;
                    return RedirectToAction("SetPrices", "Price");
                }
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult SetPrices()
        {

            PriceViewModel model = TempData["PriceViewModel"] as PriceViewModel;

            if (model == null)
            {
                model = new PriceViewModel();
                model.PharmacyId = -1;
            }

            model.Medicines = dbService.LoadAllMedicines().ToList();
            model.SizesLists = new List<List<SelectListItem>>();

            if(model.Prices == null)
            {
                model.Prices = dbService.LoadPricesForPharmacy(model.PharmacyId);
            }

            if (model.SelectedMedicines == null)
            {
                model.SelectedMedicines = new List<string>();

                for (int i = 0; i < model.Prices.Count; ++i)
                {
                    model.SelectedMedicines.Add(model.Prices[i].Size.MedicineId.ToString());
                }
            }

            for (int i = 0; i < model.Prices.Count; ++i)
            {
                if(model.Prices[i].Size == null)
                {
                    model.Prices[i].Size = dbService.FindSizeById(model.Prices[i].SizeId);
                }

                model.SizesLists.Add(PrepareSizeListForMedicine(model.Prices[i].Size.MedicineId));
            }

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPrices(PriceViewModel model, string command)
        {
            if (command.StartsWith("Usuń"))
            {
                string s = command.Substring(4);
                model.Prices.RemoveAt(int.Parse(s));
            }
            else if (command.Equals("Dodaj"))
            {
                for(int i =0; i < model.NumberOfNewRecords;++i)
                {
                    Price price = new Price
                    {
                        Value = "0",
                        SizeId = 1,
                    };
                    if (model.Prices == null)
                    {
                        model.Prices = new List<Price>();
                    }
                    model.Prices.Add(price);
                    if (model.SelectedMedicines == null)
                    {
                        model.SelectedMedicines = new List<string>();
                    }
                    model.SelectedMedicines.Add("1");
                }
            }
            else if(command.Equals("Zapisz"))
            {
                foreach(Price p in model.Prices)
                {
                    p.PharmacyId = model.PharmacyId;
                    dbService.UpdatePrice(p);
                }

                model.Message = "Zapisano";
                model.Prices = null;
                model.SelectedMedicines = null;
            }

            TempData["PriceViewModel"] = model;
            return RedirectToAction("SetPrices", "Price");
        }

        public List<SelectListItem> PrepareSizeListForMedicine(int medicineId)
        {
            Medicine med = dbService.FindMedicineById(medicineId);

            List<SelectListItem> slt = new List<SelectListItem>();
            foreach (Size size in med.Sizes)
            {
                slt.Add(new SelectListItem
                {
                    Value = size.SizeId.ToString(),
                    Text = size.Value + size.Unit,
                });
            }

            return slt;
        }

        [HttpGet]
        public ActionResult PrepareSizeList(string medicineId)
        {
            Medicine med = dbService.FindMedicineById(int.Parse(medicineId));

            List<SelectListItem> slt = new List<SelectListItem>();
            foreach (Size size in med.Sizes.OrderBy(x=>x.Value))
            {
                slt.Add(new SelectListItem
                {
                    Value = size.SizeId.ToString(),
                    Text = size.Value + size.Unit,
                });
            }

            return Json(slt, JsonRequestBehavior.AllowGet);
        }
    }
}
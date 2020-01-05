using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyWebApp.Services;
using PharmacyWebApp.Models;
using System.Threading.Tasks;
using System.IO;
using PagedList;
using System.Globalization;

namespace PharmacyWebApp.Controllers
{
    public class MedicineController : BaseController
    {
        public ActionResult AddMedicine()
        {
            MedicineViewModel model = TempData["tmp"] as MedicineViewModel;

            if (model == null)
            {
                model = new MedicineViewModel();
            }

            PrepareMedicineModel(model);

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMedicine(MedicineViewModel model, string command)
        {
            if (command.StartsWith("Usuń"))
            {
                string s = command.Substring(4);
                model.SelectedComponents.RemoveAt(int.Parse(s));
                model.Concentrations.RemoveAt(int.Parse(s));
                TempData["tmp"] = model;
                return RedirectToAction("AddMedicine", "Medicine");
            }
            else if (command.Equals("Dodaj nowy składnik"))
            {
                PrepareMedicineModel(model);

                model.SelectedComponents.Add("");
                model.Concentrations.Add("");
                model.SelectedUnits.Add("");
            }
            else if (command.Equals("Dodaj lek do bazy"))
            {
                if (ModelState.IsValid)
                {
                    int pharmacyId = dbService.FindPharmacyIdByKey(model.PharmacyKey);
                    if (pharmacyId == -1)
                    {
                        ModelState.AddModelError(string.Empty, "Podany klucz apteki nie znajduje się w bazie");
                        return View(model);
                    }

                    model.Medicine.PharmacyId = pharmacyId;
                    dbService.AddNewMedicineToDb(PrepareMedicine(model));
                    model = new MedicineViewModel();
                    model.Messege = "Lek został dodany do bazy";
                    TempData["tmp"] = model;
                    return RedirectToAction("AddMedicine", "Medicine");
                }
            }

            return View(model);
        }

        // GET: Medicine
        public ActionResult AddContainer()
        {
            ContainerViewModel model = TempData["ContainerViewModel"] as ContainerViewModel;

            if(model == null)
            {
                model = new ContainerViewModel();
            }

            PrepareContainerModel(model);

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> AddContainer(HttpPostedFileBase file, ContainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        model.MedicineSize.Image = ms.GetBuffer();
                    }

                    dbService.AddNewMedicineContainerToDb(model.MedicineSize);
                    model = new ContainerViewModel();
                    model.Message = "Dodawanie zakończone sukcesem.";
                    TempData["ContainerViewModel"] = model;
                    return RedirectToAction("AddContainer", "Medicine");
                }
            }

            PrepareContainerModel(model);

            return View(model);
        }

        public ActionResult DisplayMedicines()
        {
            DisplayMedicinesViewModel model = TempData["DisplayMedicinesViewModel"] as DisplayMedicinesViewModel;

            if (model == null)
            {
                model = new DisplayMedicinesViewModel();
                model.PageSize = 20;
                model.PageNumber = 1;
                model.SortAttribute = "Nazwa";
            }
            List<Size> list = dbService.LoadAllSizes();

            if (!String.IsNullOrEmpty(model.SearchText))
            {
                list = list.Where(x => x.Value.Contains(model.SearchText)
                                       || x.Medicine.Name.Contains(model.SearchText)
                                       || x.Medicine.AplicationMethod.Name.Contains(model.SearchText)
                                       || x.Medicine.Form.Name.Contains(model.SearchText)
                                       || x.Unit.Contains(model.SearchText)).ToList();
            }

            if (model.SortOrderAsc == true)
            {
                switch (model.SortAttribute)
                {
                    case "Postać":
                        model.Sizes = list.OrderBy(x => x.Medicine.Form.Name).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    case "Sposób aplikacji":
                        model.Sizes = list.OrderBy(x => x.Medicine.AplicationMethod.Name).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    case "Wielkość":
                        model.Sizes = list.OrderBy(x => x.Value + x.Unit).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    case "Średnia cena":
                        model.Sizes = list.OrderBy(x => x.Prices.Average(z => double.Parse(z.Value, CultureInfo.InvariantCulture))).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    default:
                        model.Sizes = list.OrderBy(x => x.Medicine.Name).ToPagedList(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else
            {
                switch (model.SortAttribute)
                {
                    case "Postać":
                        model.Sizes = list.OrderByDescending(x => x.Medicine.Form.Name).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    case "Sposób aplikacji":
                        model.Sizes = list.OrderByDescending(x => x.Medicine.AplicationMethod.Name).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    case "Wielkość":
                        model.Sizes = list.OrderByDescending(x => x.Value).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    case "Średnia cena":
                        model.Sizes = list.OrderByDescending(x => x.Prices.Average(z => double.Parse(z.Value, CultureInfo.InvariantCulture))).ToPagedList(model.PageNumber, model.PageSize);
                        break;

                    default:
                        model.Sizes = list.OrderByDescending(x => x.Medicine.Name).ToPagedList(model.PageNumber, model.PageSize);
                        break;
                }
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DisplayMedicines(DisplayMedicinesViewModel model, string command, int? page)
        {
            model.PageSize = 20;

            if (command != null && !command.Equals(""))
            {
                if (model.SortAttribute != null && model.SortAttribute.Equals(command))
                {
                    model.SortOrderAsc = !model.SortOrderAsc;
                }
                else
                {
                    model.SortOrderAsc = true;
                }

                model.SortAttribute = command;
            }

            model.PageNumber = (page ?? 1);


            TempData["DisplayMedicinesViewModel"] = model;
            return RedirectToAction("DisplayMedicines", "Medicine");
        }

        [HttpGet]
        public ActionResult ShowMedicineInfo(string sizeId, string priceId, string problemId)
        {
            ShowMedicineViewModel model = TempData["ShowMedicineViewModel"] as ShowMedicineViewModel;

            if(model == null)
            {
                model = new ShowMedicineViewModel();
            }

            if (int.Parse(sizeId) != -1)
            {
                model.SelectedSize = dbService.LoadAllSizes().Single(x => x.SizeId == int.Parse(sizeId));
                model.Price = new Price();
                model.Price.Pharmacy = new Pharmacy();
                model.PriceNotNull = false;
            }
            else
            {
                int price = int.Parse(priceId);
                int problem = int.Parse(problemId);
                model.Price = dbService.LoadPricesForProblem(problem).Single(x=>x.PriceId == price);
                model.SelectedSize = model.Price.Size;
                model.PriceNotNull = true;
            }

            return View(model);
        }

        private void PrepareMedicineModel(MedicineViewModel model)
        {
            model.AgeCategories = dbService.LoadAgeCategories();
            model.Problems = dbService.LoadAllProblems();
            model.Components = dbService.LoadAllComponents();
            model.AplicationMethods = dbService.LoadAplicationMethods();
            model.Forms = dbService.LoadAllForms();
            model.BodyParts = dbService.LoadAllBodyParts();


            model.Units = new List<SelectListItem>();
            model.Units.Add(new SelectListItem() { Value = "mg", Text = "mg" });
            model.Units.Add(new SelectListItem() { Value = "mg/ml", Text = "mg/ml" });

            if (model.Medicine == null)
            {
                model.Medicine = new Medicine();
            }
            if (model.SelectedProblems == null)
            {
                model.SelectedProblems = new List<string>();
            }
            if (model.SelectedAgeCategories == null)
            {
                model.SelectedAgeCategories = new List<string>();
            }
            if (model.SelectedComponents == null)
            {
                model.SelectedComponents = new List<string>();
                model.SelectedComponents.Add("");
            }
            if (model.Concentrations == null)
            {
                model.Concentrations = new List<string>();
                model.Concentrations.Add("");
            }
            if (model.SelectedUnits == null)
            {
                model.SelectedUnits = new List<string>();
                model.SelectedUnits.Add("");
            }
        }

        private void PrepareContainerModel(ContainerViewModel model)
        {
            model.Medicines = dbService.LoadAllMedicines().OrderBy(x=>x.Name).ToList();
            model.Units = new List<SelectListItem>();
            model.Units.Add(new SelectListItem() { Value = "g", Text = "g" });
            model.Units.Add(new SelectListItem() { Value = "ml", Text = "ml" });
            model.Units.Add(new SelectListItem() { Value = "szt", Text = "szt" });

            if (model.MedicineSize == null)
            {
                model.MedicineSize = new Size();
            }
        }

        private Medicine PrepareMedicine(MedicineViewModel model)
        {
            Medicine med = model.Medicine;
            med.Problems = dbService.FindProblemsByIds(model.SelectedProblems);

            if (model.Concentrations != null)
            {
                for (int i = 0; i < model.Concentrations.Count; ++i)
                {
                    med.Concentrations.Add(new Concentration
                    {
                        Value = model.Concentrations[i] + model.SelectedUnits[i],
                        ComponentId = int.Parse(model.SelectedComponents[i]),
                    });
                }
            }

            med.AgeCategories = dbService.FindAgeCategoriesByIds(model.SelectedAgeCategories);
            med.AplicationMethodId = int.Parse(model.SelectedAplicationMethod);
            med.BodyParts = dbService.FindBodyPartsByIds(model.SelectedBodyParts);

            med.FormId = int.Parse(model.SelectedForm);

            return med;
        }
    }
}
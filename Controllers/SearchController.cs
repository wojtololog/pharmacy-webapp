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
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult SearchMedicine()
        {
            SearchViewModel model = TempData["SearchViewModel"] as SearchViewModel;

            if (model == null)
            {
                model = new SearchViewModel();
            }

            PrepareModel(model);

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchMedicine(SearchViewModel model, string command)
        {
            if (command.Equals("Szukaj"))
            {
                List<Price> prices = dbService.LoadPricesForProblem(int.Parse(model.SelectedProblem));
                ScoringService ss = new ScoringService(prices, model);
                model.FoundMedicines = ss.GetResults().OrderBy(x => x.score).Take(10).ToList();
            }

            TempData["SearchViewModel"] = model;
            return RedirectToAction("SearchMedicine", "Search");
        }

        private void PrepareModel(SearchViewModel model)
        {
            model.AgeCategories = dbService.LoadAgeCategories();
            model.AgeCategories.Insert(0,new AgeCategory { AgeCategoryId=-1, Name = "---WYBIERZ---" });

            model.AplicationMethods = dbService.LoadAplicationMethods();
            model.AplicationMethods.Insert(0, new AplicationMethod { AplicationMethodId = -1, Name = "---WYBIERZ---" });

            model.Components = dbService.LoadAllComponents();
            model.Components.Insert(0, new Component { ComponentId = -1, Name = "---WYBIERZ---" });

            model.Forms = dbService.LoadAllForms();
            model.Forms.Insert(0, new Form { FormId = -1, Name = "---WYBIERZ---" });

            model.Pharmacies = dbService.LoadAllPharmacies().Where(x=>x.RegistrationConfirmed == true).ToList();
            model.Problems = dbService.LoadAllProblems().Where(p => p.Medicines.Count > 0).OrderBy(p => p.Name).ToList();

            model.BodyParts = dbService.LoadAllBodyParts();
            model.BodyParts.Insert(0, new BodyPart { BodyPartId = -1, Name = "---WYBIERZ---" });

            model.SelectedComponents = new List<string>();

            model.Sizes = new List<SelectListItem>();
            model.Sizes.Add(new SelectListItem() { Value = "-1", Text = "---WYBIERZ---" });
            model.Sizes.Add(new SelectListItem() { Value = "xs", Text = "bardzo małe" });
            model.Sizes.Add(new SelectListItem() { Value = "s", Text = "małe" });
            model.Sizes.Add(new SelectListItem() { Value = "m", Text = "średnie" });
            model.Sizes.Add(new SelectListItem() { Value = "l", Text = "duże" });
            model.Sizes.Add(new SelectListItem() { Value = "xl", Text = "bardzo duże" });

        }

    }

}
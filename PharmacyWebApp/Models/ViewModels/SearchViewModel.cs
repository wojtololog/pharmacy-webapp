using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyWebApp.Models
{
    public class SearchViewModel
    {
        public int Zoom { get; set; }

        [Display(Name = "Szerokość")]
        public string UserLatitude { get; set; }

        [Display(Name = "Długość")]
        public string UserLongitude { get; set; }

        [Display(Name = "Postać")]
        public string SelectedForm { get; set; }

        [Display(Name = "Część ciała")]
        public string SelectedBodyPart { get; set; }

        [Display(Name = "Wielkość opakowania")]
        public string SelectedSize { get; set; }

        [Display(Name = "Problem")]
        public string SelectedProblem { get; set; }

        [Display(Name = "Pomijane składniki")]
        public List<string> SelectedComponents { get; set; }

        [Display(Name = "Sposób aplikacji")]
        public string SelectedAplicationMethod { get; set; }

        [Display(Name = "Kategoria wiekowa")]
        public string SelectedAgeCategory{ get; set; }

        [Display(Name = "Stosowanie u kobiet w ciąży")]
        public bool PregnancyUseable { get; set; }

        [Display(Name = "Tylko leki bez recepty")]
        public bool Prescription { get; set; }

        public List<Pharmacy> Pharmacies { get; set; }

        public List<Form> Forms { get; set; }

        public List<AplicationMethod> AplicationMethods { get; set; }

        public List<Component> Components { get; set; }

        public List<Problem> Problems { get; set; }

        public List<AgeCategory> AgeCategories { get; set; }

        public List<BodyPart> BodyParts { get; set; }

        public List<SelectListItem> Sizes { get; set; }

        public List<SearchResult> FoundMedicines { get; set; }

    }
}
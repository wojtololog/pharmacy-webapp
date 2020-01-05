using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyWebApp.Models
{
    public class MedicineViewModel
    {
        public string Messege { get; set; }

        public Medicine Medicine { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        [Display(Name = "Postać leku")]
        public string SelectedForm { get; set; }

        [Display(Name = "Sposób aplikacji")]
        public string SelectedAplicationMethod { get; set; }

        public List<string> SelectedProblems { get; set; }

        public List<string> SelectedAgeCategories { get; set; }

        public List<string> SelectedComponents { get; set; }

        public List<string> SelectedUnits { get; set; }

        public List<string> SelectedBodyParts { get; set; }

        [Display(Name = "Stężenie")]
        public List<string> Concentrations { get; set; }

        [Display(Name = "Części ciała")]
        public List<BodyPart> BodyParts { get; set; }

        [Display(Name = "Jednostka")]
        public List<SelectListItem> Units { get; set; }

        [Display(Name = "Problemy")]
        public List<Problem> Problems { get; set; }

        [Display(Name = "Kategorie wiekowe")]
        public List<AgeCategory> AgeCategories { get; set; }

        public List<Form> Forms { get; set; }

        [Display(Name = "Składniki")]
        public List<Component> Components { get; set; }

        public List<AplicationMethod> AplicationMethods { get; set; }

        [Display(Name = "Klucz zabezpieczeń apteki")]
        public string PharmacyKey { get; set; }
    }
}
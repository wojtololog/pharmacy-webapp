using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Medicine : TableRecord
    {
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        [Display(Name = "Nazwa leku")]
        public string Name { get; set; }

        [Display(Name = "Postać leku")]
        public int FormId { get; set; }

        [Display(Name = "Postać leku")]
        public Form Form { get; set; }

        [Display(Name = "Stosowanie u kobiet w ciąży")]
        public bool PregnancyUseable { get; set; }

        [Display(Name = "Dostępny bez recepty")]
        public bool AvailableWithoutPrescription { get; set; }

        [Display(Name = "Sposób aplikacji")]
        public int AplicationMethodId { get; set; }

        [Display(Name = "Sposób aplikacji")]
        public AplicationMethod AplicationMethod { get; set; }

        [Display(Name = "Treść ulotki")]
        public string Recommendations { get; set; }

        public int PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        [Display(Name = "Problemy")]
        public List<Problem> Problems { get; set; } = new List<Problem>();

        public List<Size> Sizes { get; set; } = new List<Size>();

        [Display(Name = "Substancje aktywne")]
        public List<Concentration> Concentrations { get; set; } = new List<Concentration>();

        [Display(Name = "Kategorie wiekowe")]
        public List<AgeCategory> AgeCategories { get; set; } = new List<AgeCategory>();

        [Display(Name = "Części ciała")]
        public List<BodyPart> BodyParts { get; set; } = new List<BodyPart>();

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class WorkingHour : TableRecord
    {
        [ForeignKey("Pharmacy")]
        public int WorkingHourId { get; set; }

        [Display(Name = "Poniedziałek")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string MondayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string MondayTo { get; set; }

        [Display(Name = "Wtorek")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string TuesdayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string TuesdayTo { get; set; } 

        [Display(Name = "Środa")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string WednesdayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string WednesdayTo { get; set; } 

        [Display(Name = "Czwartek")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string ThursdayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string ThursdayTo { get; set; } 

        [Display(Name = "Piątek")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string FridayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string FridayTo { get; set; } 

        [Display(Name = "Sobota")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string SaturdayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string SaturdayTo { get; set; } 

        [Display(Name = "Niedziela")]
        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string SundayFrom { get; set; }

        [RegularExpression(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Niepoprawny format godziny. Przykład poprawnej godziny \"12:30.\"")]
        public string SundayTo { get; set; } 

        public Pharmacy Pharmacy { get; set; } 


    }
}
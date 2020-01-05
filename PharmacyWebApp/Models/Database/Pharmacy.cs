using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmacyWebApp.Models
{
    public class Pharmacy : TableRecord
    {
        public int PharmacyId { get; set; }

        public string PharmacyKey { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić nazwę apteki")]
        [StringLength(100, ErrorMessage = "Nazwa firmy musi posiadać conajmniej 3 znaki", MinimumLength = 3)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić adres email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić numer")]
        [Display(Name = "Numer telefon")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić NIP")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Nieprawidłowy numer NIP")]
        [Display(Name = "NIP")]
        public string NIP { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić kod pocztowy")]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić nazwę ulicy")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić numer domu")]
        [Display(Name = "Numer domu")]
        public int HousingNumber { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić współrzędne")]
        [Display(Name = "Długość geograficzna")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić współrzędne")]
        [Display(Name = "Szerokość geograficzna")]
        public string Latitude { get; set; }

        public WorkingHour WorkingHour { get; set; }

        public int CityId { get; set; }

        [Display(Name = "Miejscowość")]
        public City City { get; set; }

        public List<Medicine> AddedMedicines { get; set; } = new List<Medicine>();

        public List<Price> Prices { get; set; } = new List<Price>();

        public bool RegistrationConfirmed { get; set; } = false;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyWebApp.Models
{
    public class ContainerViewModel
    {
        public string Message { get; set; }

        public Size MedicineSize { get; set; }

        [Display(Name = "Klucz apteki")]
        [Required(ErrorMessage = "Uzupełnij pole")]
        public string PharmacyKey { get; set; }

        [Display(Name = "Lek")]
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();

        public List<SelectListItem> Units { get; set; }

    }
}
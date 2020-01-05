using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Models
{
    public class DisplayMedicinesViewModel
    {
        [Display(Name = "Szukaj")]
        public string SearchText { get; set; }

        public string SortAttribute { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool SortOrderAsc { get; set; } = true;
        public IPagedList<Size> Sizes { get; set; }
    }
}
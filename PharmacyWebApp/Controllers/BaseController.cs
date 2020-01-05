using PharmacyWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyWebApp.Controllers
{
    public class BaseController : Controller
    {
        public DatabaseService dbService = new DatabaseService();
        public MailService mailService = new MailService();
        public KeyGeneratorService keyService = new KeyGeneratorService();
    }
}
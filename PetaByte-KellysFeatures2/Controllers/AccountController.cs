using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;

namespace PetaByte_KellysFeatures2.Controllers
{
    public class AccountController : Controller
    {
        PetaByteContext db = new PetaByteContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}
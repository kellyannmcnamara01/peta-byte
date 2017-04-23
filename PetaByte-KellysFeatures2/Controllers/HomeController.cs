using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;

namespace PetaByte_KellysFeatures2.Controllers
{
    public class HomeController : Controller
    {
        private PetaByteContext db = new PetaByteContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Alerts()
        {
            //grab all of the alerts that are published
            List<Alert> alerts = db.Alerts.Where(a => a.status == "Published").ToList();
            //call the partial view name and list
            return PartialView("_AlertsPartial", alerts);
        }
    }
}
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetaByte_KellysFeatures2.Controllers
{
    //think about the urls
    // /post
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

    }
}
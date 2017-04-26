using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;

//  Project: Temiskaming Hospital Website
//  Team Name: PetaByte
//  Class: Mobile Development
//  Professor: Lee Situ
//  Author: Kelly Ann McNamara
//  File Description: This file sets the outlining functionality for the Pages feature. Please see other comments below for specific details.


namespace PetaByte_KellysFeatures2.Controllers
{
    public class PagesController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Pages
        //[Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var pages = db.Pages.Include(p => p.Employee);
            return View(pages.ToList());
        }

        // GET: Pages/Details/5
        //[AllowAnonymous]
        [Route("{controller}/{action}/{pageUrl}")]
        public ActionResult Details(string pageUrl)
        {
            if (string.IsNullOrWhiteSpace(pageUrl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //NOTE (Kelly Ann McNamara): make sure it looks for the pageUrl as the url not the id
            //NOTE (Kelly Ann McNamara):  pages/details/pageUrl

            //NOTE (Kelly Ann McNamara): I changed out the below commented line to the one on line 39. I had to compare the string I was calling with the string that I wanted to use as the url
            //NOTE (Kelly Ann McNamara): Page page = db.Pages.Single( p => p.pageUrl == pageUrl);

            Page page = db.Pages.Where(p => p.pageUrl.Equals(pageUrl, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (page == null)
            {
                return HttpNotFound();
            }
            //viewbag 
            ViewBag.pageUrl = pageUrl;
            return View(page);
        }

        // GET: Pages/Create
        //[Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName");
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pageId,pageTitle,pageContent,timestamp,status,employeeId,pageUrl")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.timestamp = DateTime.Now;
                db.Pages.Add(page);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", page.employeeId);
            return View(page);
        }

        // GET: Pages/Edit/5
        //[Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", page.employeeId);
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598
        // text editor from : http://nicedit.com/
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pageId,pageTitle,pageContent,timestamp,status,employeeId,pageUrl")] Page page)
        {
            if (ModelState.IsValid)
            {

                page.timestamp = DateTime.Now;
                db.Entry(page).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", page.employeeId);
            return View(page);
        }

        // GET: Pages/Delete/5
        //[Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Pages/Delete/5
        //[Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

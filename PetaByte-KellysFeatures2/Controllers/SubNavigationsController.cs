using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;

namespace PetaByte_KellysFeatures2.Controllers
{
    public class SubNavigationsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: SubNavigations
        public ActionResult Index()
        {
            var subNavigations = db.SubNavigations.Include(s => s.Employee).Include(s => s.Navigation);
            return View(subNavigations.ToList());
        }

        // GET: SubNavigations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubNavigation subNavigation = db.SubNavigations.Find(id);
            if (subNavigation == null)
            {
                return HttpNotFound();
            }
            return View(subNavigation);
        }

        // GET: SubNavigations/Create
        public ActionResult Create()
        {
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName");
            ViewBag.navId = new SelectList(db.Navigations, "navId", "navTitle");
            return View();
        }

        // POST: SubNavigations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subNavId,navId,subNavTitle,subNavUrl,employeeId")] SubNavigation subNavigation)
        {
            if (ModelState.IsValid)
            {
                db.SubNavigations.Add(subNavigation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", subNavigation.employeeId);
            ViewBag.navId = new SelectList(db.Navigations, "navId", "navTitle", subNavigation.navId);
            return View(subNavigation);
        }

        // GET: SubNavigations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubNavigation subNavigation = db.SubNavigations.Find(id);
            if (subNavigation == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", subNavigation.employeeId);
            ViewBag.navId = new SelectList(db.Navigations, "navId", "navTitle", subNavigation.navId);
            return View(subNavigation);
        }

        // POST: SubNavigations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subNavId,navId,subNavTitle,subNavUrl,employeeId")] SubNavigation subNavigation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subNavigation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", subNavigation.employeeId);
            ViewBag.navId = new SelectList(db.Navigations, "navId", "navTitle", subNavigation.navId);
            return View(subNavigation);
        }

        // GET: SubNavigations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubNavigation subNavigation = db.SubNavigations.Find(id);
            if (subNavigation == null)
            {
                return HttpNotFound();
            }
            return View(subNavigation);
        }

        // POST: SubNavigations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubNavigation subNavigation = db.SubNavigations.Find(id);
            db.SubNavigations.Remove(subNavigation);
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

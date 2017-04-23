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
    public class NavigationsController : Controller 
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Navigations
        public ActionResult Index()
        {
            var navigations = db.Navigations.Include(n => n.Employee);
            return View(navigations.ToList());
        }

        // GET: Navigations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigation navigation = db.Navigations.Find(id);
            if (navigation == null)
            {
                return HttpNotFound();
            }
            return View(navigation);
        }

        // GET: Navigations/Create
        public ActionResult Create()
        {
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName");
            return View();
        }

        // POST: Navigations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "navId,navTitle,navUrl,employeeId")] Navigation navigation)
        {
            if (ModelState.IsValid)
            {
                db.Navigations.Add(navigation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", navigation.employeeId);
            return View(navigation);
        }

        // GET: Navigations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigation navigation = db.Navigations.Find(id);
            if (navigation == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", navigation.employeeId);
            return View(navigation);
        }

        // POST: Navigations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "navId,navTitle,navUrl,employeeId")] Navigation navigation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(navigation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", navigation.employeeId);
            return View(navigation);
        }

        // GET: Navigations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigation navigation = db.Navigations.Find(id);
            if (navigation == null)
            {
                return HttpNotFound();
            }
            return View(navigation);
        }

        // POST: Navigations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Navigation navigation = db.Navigations.Find(id);
            db.Navigations.Remove(navigation);
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

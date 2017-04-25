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
//  File Description: This file sets the outlining functionality for the Alerts feature. Please see other comments below for specific details. 

namespace PetaByte_KellysFeatures2.Controllers
{
    public class AlertsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Alerts
        [AllowAnonymous]
        public ActionResult Index()
        {
            var alerts = db.Alerts.Include(a => a.Employee);
            return View(alerts.ToList());
        }

        // GET: Alerts/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName");
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "akertId,alertText,timestamp,status,employeeId")] Alert alert)
        {
            //NOTE (Kelly Ann McNamara): place action into a try catch to validate and display errors
            try
            {
                if (ModelState.IsValid)
                {
                    //NOTE (Kelly Ann McNamara): have the timestamp input the current date and time
                    alert.timestamp = DateTime.Now;
                    db.Alerts.Add(alert);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception dex)
            {
                string err_message = "Please make sure all required fields are filled out correctly";
                ViewBag.Message = err_message;
            }

            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", alert.employeeId);
            return View(alert);
        }

        // GET: Alerts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", alert.employeeId);
            return View(alert);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "akertId,alertText,timestamp,status,employeeId")] Alert alert)
        {
            //NOTE (Kelly Ann McNamara): place ation into a try catch to validate and throw errors
            try
            {
                if (ModelState.IsValid)
                {
                    //NOTE (Kelly Ann McNamara): have the timestamp update when the user edits the alert
                    alert.timestamp = DateTime.Now;
                    db.Entry(alert).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception dex)
            {
                string err_message = "Please make sure all required fields are filled out correctly";
                ViewBag.Message = err_message;
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", alert.employeeId);
            return View(alert);
        }

        // GET: Alerts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return HttpNotFound();
            }
            return View(alert);
        }

        // POST: Alerts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alert alert = db.Alerts.Find(id);
            db.Alerts.Remove(alert);
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

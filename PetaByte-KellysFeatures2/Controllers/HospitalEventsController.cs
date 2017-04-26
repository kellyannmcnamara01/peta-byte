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
//  Author: Mahmud Al-Mohammad Amin
//  File Description: This controller sets the Actions for the Admin side logic and views. 

namespace PetaByte_KellysFeatures2.Controllers
{
    public class HospitalEventsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: HospitalEvents
        public ActionResult Index()
        {
            return View(db.HospitalEvents.ToList());
        }

        // GET: HospitalEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalEvent hospitalEvent = db.HospitalEvents.Find(id);
            if (hospitalEvent == null)
            {
                return HttpNotFound();
            }
            return View(hospitalEvent);
        }

        // GET: HospitalEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HospitalEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eventsId,evntName,evntDate,evntDesc,evntLoc")] HospitalEvent hospitalEvent)
        {
            if (ModelState.IsValid)
            {
                db.HospitalEvents.Add(hospitalEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hospitalEvent);
        }

        // GET: HospitalEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalEvent hospitalEvent = db.HospitalEvents.Find(id);
            if (hospitalEvent == null)
            {
                return HttpNotFound();
            }
            return View(hospitalEvent);
        }

        // POST: HospitalEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eventsId,evntName,evntDate,evntDesc,evntLoc")] HospitalEvent hospitalEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitalEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hospitalEvent);
        }

        // GET: HospitalEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HospitalEvent hospitalEvent = db.HospitalEvents.Find(id);
            if (hospitalEvent == null)
            {
                return HttpNotFound();
            }
            return View(hospitalEvent);
        }

        // POST: HospitalEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HospitalEvent hospitalEvent = db.HospitalEvents.Find(id);
            db.HospitalEvents.Remove(hospitalEvent);
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


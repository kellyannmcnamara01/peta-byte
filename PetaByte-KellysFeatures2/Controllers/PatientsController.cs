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
    public class PatientsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Patients
        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.Addresses1).Include(p => p.Contact).Include(p => p.EmerageContact).Include(p => p.Insurance);
            return View(patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet");
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum");
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName");
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patientId,firstName,middleName,lastName,DOB,gender,physican,joinDate,contactId,addressId,healthCardNum,emerageContactID,insuranceId,Address,City,Country,Allergies,relationship,email,phone,postal")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet", patient.addressId);
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum", patient.contactId);
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName", patient.emerageContactID);
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver", patient.insuranceId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet", patient.addressId);
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum", patient.contactId);
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName", patient.emerageContactID);
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver", patient.insuranceId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patientId,firstName,middleName,lastName,DOB,gender,physican,joinDate,contactId,addressId,healthCardNum,emerageContactID,insuranceId,Address,City,Country,Allergies,relationship,email,phone,postal")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet", patient.addressId);
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum", patient.contactId);
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName", patient.emerageContactID);
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver", patient.insuranceId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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

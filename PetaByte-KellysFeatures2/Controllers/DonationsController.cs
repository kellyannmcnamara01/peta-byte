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
    public class DonationsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Donations
        public ActionResult Index()
        {
            var donations = db.Donations.Include(d => d.DonationOccurence).Include(d => d.Donor);
            return View(donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            ViewBag.occurenceId = new SelectList(db.DonationOccurences, "donationOccurenceId", "donationOccurenceId");
            ViewBag.donorsId = new SelectList(db.Donors, "donorsId", "donorFirstName");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "donationsId,donationAmount,occurenceId,donationDate,donorsId,donorFN,donorLN,email,homeNum,workNum,mobileNum,addressStreet,addressProv,addressCountry,postal,addressCity,honorFN,honorLN,company")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.occurenceId = new SelectList(db.DonationOccurences, "donationOccurenceId", "donationOccurenceId", donation.occurenceId);
            ViewBag.donorsId = new SelectList(db.Donors, "donorsId", "donorFirstName", donation.donorsId);
            return View(donation);
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.occurenceId = new SelectList(db.DonationOccurences, "donationOccurenceId", "donationOccurenceId", donation.occurenceId);
            ViewBag.donorsId = new SelectList(db.Donors, "donorsId", "donorFirstName", donation.donorsId);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "donationsId,donationAmount,occurenceId,donationDate,donorsId,donorFN,donorLN,email,homeNum,workNum,mobileNum,addressStreet,addressProv,addressCountry,postal,addressCity,honorFN,honorLN,company")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.occurenceId = new SelectList(db.DonationOccurences, "donationOccurenceId", "donationOccurenceId", donation.occurenceId);
            ViewBag.donorsId = new SelectList(db.Donors, "donorsId", "donorFirstName", donation.donorsId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
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

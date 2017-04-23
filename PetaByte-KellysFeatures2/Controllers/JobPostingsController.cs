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
    public class JobPostingsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: JobPostings
        public ActionResult Index()
        {
            var jobPostings = db.JobPostings.Include(j => j.Employee);
            return View(jobPostings.ToList());
        }

        //// GET: JobPostings/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    JobPosting jobPosting = db.JobPostings.Find(id);
        //    if (jobPosting == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(jobPosting);
        //}

        // GET: JobPostings/Create
        public ActionResult Create()
        {
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName");
            return View();
        }

        // POST: JobPostings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jobId,jobTitle,jobDesc,employeeId,timestamp,status")] JobPosting jobPosting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    jobPosting.timestamp = DateTime.Now;
                    db.JobPostings.Add(jobPosting);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception dex)
            {
                string err_message = "Please make sure all required fields are filled out correctly";
                ViewBag.Message = err_message;
            }

            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", jobPosting.employeeId);
            return View(jobPosting);
        }

        // GET: JobPostings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", jobPosting.employeeId);
            return View(jobPosting);
        }

        // POST: JobPostings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jobId,jobTitle,jobDesc,employeeId,timestamp,status")] JobPosting jobPosting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(jobPosting).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception dex)
            {
                string err_message = "Please make sure all required fields are filled out correctly";
                ViewBag.Message = err_message;
            }
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "firstName", jobPosting.employeeId);
            return View(jobPosting);
        }

        // GET: JobPostings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // POST: JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPosting jobPosting = db.JobPostings.Find(id);
            db.JobPostings.Remove(jobPosting);
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

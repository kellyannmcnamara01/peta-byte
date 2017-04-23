using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;
using System.IO;

//  Project: Temiskaming Hospital Website
//  Team Name: PetaByte
//  Class: Mobile Development
//  Professor: Lee Situ
//  Author: Kelly Ann McNamara
//  File Description: This file sets the outlining functionality for the Job Applicants feature. Please see other comments below for specific details.


namespace PetaByte_KellysFeatures2.Controllers
{
    public class JobApplicantsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: JobApplicants
        public ActionResult Index()
        {
            var jobApplicants = db.JobApplicants.Include(j => j.JobPosting);
            return View(jobApplicants.ToList());
        }

        //NOTE (Kelly Ann McNamara): Partial view for job postings
        public PartialViewResult JobPosting()
        {
            //NOTE (Kelly Ann McNamara):  Populate a list to display all of the job postings that have the status of "Published" from the db
            List<JobPosting> postings = db.JobPostings.Where(jp => jp.status == "Published").ToList();
            return PartialView("_JobPostingsPartial", postings);
        }

        // GET: JobApplicants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            if (jobApplicant == null)
            {
                return HttpNotFound();
            }
            return View(jobApplicant);
        }

        // GET: JobApplicants/Create
        public ActionResult Create()
        {
            ViewBag.jobId = new SelectList(db.JobPostings, "jobId", "jobTitle");
            return View();
        }

        // POST: JobApplicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, [Bind(Include = "jobAppId,jobId,appJobTitle,appFirstName,appLastName,appEmail,appLinkedIn,appMessage,appCv,appOtherFile")] JobApplicant jobApplicant)
        {
            if (ModelState.IsValid)
            {
                //NOTE (Kelly Ann McNamara): if statement for file upload
                //NOTE (Kelly Ann McNamara): create var that returns the file name and extention
                //NOTE (Kelly Ann McNamara): have the getter and setter for appCV to equal the fileName the user uploads
                //NOTE (Kelly Ann McNamara): add all the user input to the db
                //NOTE (Kelly Ann McNamara): save changes
                //NOTE (Kelly Ann McNamara): create a file path to the uploaded content
                //NOTE (Kelly Ann McNamara): save said path
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    jobApplicant.appCv = fileName;
                    
                    db.JobApplicants.Add(jobApplicant);
                    db.SaveChanges();

                    var path = Path.Combine(Server.MapPath("~/Content/applicants/") + fileName);
                    file.SaveAs(path);
                }
                return RedirectToAction("Index");
            }

            ViewBag.jobId = new SelectList(db.JobPostings, "jobId", "jobTitle", jobApplicant.jobId);
            return View(jobApplicant);
        }

        // GET: JobApplicants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            if (jobApplicant == null)
            {
                return HttpNotFound();
            }
            ViewBag.jobId = new SelectList(db.JobPostings, "jobId", "jobTitle", jobApplicant.jobId);
            return View(jobApplicant);
        }

        // POST: JobApplicants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jobAppId,jobId,appJobTitle,appFirstName,appLastName,appEmail,appLinkedIn,appMessage,appCv,appOtherFile")] JobApplicant jobApplicant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobApplicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jobId = new SelectList(db.JobPostings, "jobId", "jobTitle", jobApplicant.jobId);
            return View(jobApplicant);
        }

        // GET: JobApplicants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            if (jobApplicant == null)
            {
                return HttpNotFound();
            }
            return View(jobApplicant);
        }

        // POST: JobApplicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobApplicant jobApplicant = db.JobApplicants.Find(id);
            db.JobApplicants.Remove(jobApplicant);
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

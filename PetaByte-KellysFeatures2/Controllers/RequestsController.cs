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
    public class RequestsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Employee).Include(r => r.Patient);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName");
            ViewBag.PatientId = new SelectList(db.Patients, "patientId", "firstName");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,PurposeOfVisit,HealthCardNum,EmployeeId,PreferredAppTime,PreferredDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                int id = request.EmployeeId;

                if(id == 0)
                {
                    ModelState.AddModelError("", "Please select a Physician");
                }

                string healthCardnum = request.HealthCardNum;

                Employee employee = db.Employees.Find(id);

                Patient healthCard = db.Patients.FirstOrDefault(p => p.healthCardNum == healthCardnum);

                if(healthCard != null)
                {
                    request.PatientFirstName = healthCard.firstName;
                    request.PatientLastName = healthCard.lastName;
                    request.PatientId = healthCard.patientId;
                    request.EmployeeId = employee.employeeId;
                    request.Status = "Pending";
                    request.DateofRequest = DateTime.Now;
                    db.Requests.Add(request);
                    db.SaveChanges();

                    ViewData["success"] = "Your appointment has been submitted";
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Healthcard Number");
                }/********************************************************************************This is the selected value from the patient*/
                ViewBag.Employee_id = new SelectList(db.Employees, "Id", "Firstname", "Lastname", request.EmployeeId);
                ViewBag.Patient_id = new SelectList(db.Patients, "Id", "Firstname", "Lastname");

                
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName", request.EmployeeId);
            ViewBag.PatientId = new SelectList(db.Patients, "patientId", "firstName", request.PatientId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName", request.EmployeeId);
            ViewBag.PatientId = new SelectList(db.Patients, "patientId", "firstName", request.PatientId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Status,PurposeOfVisit,HealthCardNum,PatientFirstName,PatientLastName,PatientId,PhysicianName,EmployeeId,DateofAppointment,PreferredAppTime,PreferredDate,DateofRequest,DateApproved")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName", request.EmployeeId);
            ViewBag.PatientId = new SelectList(db.Patients, "patientId", "firstName", request.PatientId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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

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
    public class EmployeeSchedulesController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: EmployeeSchedules
        public ActionResult Index()
        {
            var employeeSchedules = db.EmployeeSchedules.Include(e => e.Employee);
            return View(employeeSchedules.ToList());
        }

        // GET: EmployeeSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSchedule employeeSchedule = db.EmployeeSchedules.Find(id);
            if (employeeSchedule == null)
            {
                return HttpNotFound();
            }
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName");
            return View();
        }

        // POST: EmployeeSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Day,StartTime,BreakTimeBegin,BreakTimeend,EndTime,EmployeeId,EmployeeFirstName,EmployeeLastName")] EmployeeSchedule employeeSchedule)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeSchedules.Add(employeeSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSchedule employeeSchedule = db.EmployeeSchedules.Find(id);
            if (employeeSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Day,StartTime,BreakTimeBegin,BreakTimeend,EndTime,EmployeeId,EmployeeFirstName,EmployeeLastName")] EmployeeSchedule employeeSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "employeeId", "firstName", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSchedule employeeSchedule = db.EmployeeSchedules.Find(id);
            if (employeeSchedule == null)
            {
                return HttpNotFound();
            }
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSchedule employeeSchedule = db.EmployeeSchedules.Find(id);
            db.EmployeeSchedules.Remove(employeeSchedule);
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

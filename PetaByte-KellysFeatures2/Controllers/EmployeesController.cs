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
    public class EmployeesController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Addresses1).Include(e => e.Contact).Include(e => e.EmerageContact).Include(e => e.EmployeeStatu).Include(e => e.Insurance);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet");
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum");
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName");
            ViewBag.employeeStatusId = new SelectList(db.EmployeeStatus, "employeeStatusId", "status");
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeId,firstName,lastName,position,departmentId,contactId,addressId,emerageContactID,birthDate,sinNum,schedule,admin,employeeStatusId,insuranceId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet", employee.addressId);
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum", employee.contactId);
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName", employee.emerageContactID);
            ViewBag.employeeStatusId = new SelectList(db.EmployeeStatus, "employeeStatusId", "status", employee.employeeStatusId);
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver", employee.insuranceId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet", employee.addressId);
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum", employee.contactId);
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName", employee.emerageContactID);
            ViewBag.employeeStatusId = new SelectList(db.EmployeeStatus, "employeeStatusId", "status", employee.employeeStatusId);
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver", employee.insuranceId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "employeeId,firstName,lastName,position,departmentId,contactId,addressId,emerageContactID,birthDate,sinNum,schedule,admin,employeeStatusId,insuranceId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.addressId = new SelectList(db.Addresses1, "addressId", "addressStreet", employee.addressId);
            ViewBag.contactId = new SelectList(db.Contacts, "contactId", "mobileNum", employee.contactId);
            ViewBag.emerageContactID = new SelectList(db.EmerageContacts, "emerageContactId", "emergeFirstName", employee.emerageContactID);
            ViewBag.employeeStatusId = new SelectList(db.EmployeeStatus, "employeeStatusId", "status", employee.employeeStatusId);
            ViewBag.insuranceId = new SelectList(db.Insurances, "insuranceId", "insuranceProiver", employee.insuranceId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

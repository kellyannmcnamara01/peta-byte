/*
    Team: Petabyte(Temiskaming Hospital)
    Name: Wei Gao
    Description: 
        This file handles all the requirements from diferent views and replies from the model, 
        either save values to the database or send the inputs back to different views.  
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace PetaByte_KellysFeatures2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class PatientController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: Patients
        public ActionResult Admin()
        {
            var Patients = db.Patients;
            return View(Patients.ToList());
        }

        [AllowAnonymous]
        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Patient patient = db.Patients.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }
            catch (Exception dex)
            {
                ViewBag.Message = "Something went wrong: " + dex.Message;
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        // POST: Patients/Form
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Id,F_name,L_name,Address,City,Country,Email,Phone,Postal,Dob, Allergies, HCN, Insurance_PN")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    int id = patient.patientId;
                    var Patients = db.Patients.Find(id);
                    string newEmail = Patients.email;
                    string F_name = Patients.firstName;
                    string L_name = Patients.lastName;
                    string Address = Patients.Address;
                    string City = Patients.City;
                    string Country = Patients.Country;
                    string Postal = Patients.postal;
                    string Phone = Patients.phone;
                    string Dob = ((DateTime)Patients.DOB).ToString("yyyy-MM-dd");
                    string Allergies = Patients.Allergies;
                    string HCN = Patients.healthCardNum;
                    MailMessage email = new MailMessage();
                    string serverName = "smtp.gmail.com";
                    string username = "wgao12333";
                    string password = "password4444";
                    string fromEmail = "wgao12333@gmail.com";
                    int port = 587;
                    email.To.Add(new MailAddress(newEmail));
                    email.From = new MailAddress(fromEmail);
                    email.Subject = "Thank you for your submission";
                    var message = "Thank you for your submission, " + F_name + " " + L_name;
                    message = message + "<br>" + "Address: " + Address + "<br>" + "City: " + City + "<br>";
                    message = message + "Country: " + Country + "<br>" + "Postal: " + Postal + "<br>" + "Phone: " + Phone + "<br>" + "Dob: " + Dob + "<br>";
                    message = message + "Allergies: " + Allergies + "<br>" + "HCN: " + HCN + "<br>";
                    email.Body = string.Format(message);
                    email.IsBodyHtml = true;
                    email.BodyEncoding = System.Text.Encoding.UTF8;

                    using (SmtpClient client = new SmtpClient(serverName, port))
                    {
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(username, password);
                        await client.SendMailAsync(email);
                        ViewBag.sent = "Email sent";
                    }
                    return RedirectToAction("Detail", new { id = patient.patientId });
                }
            }
            catch (DataException dex)
            {
                ViewBag.Message = "Whoops!Something went wrong. Here's what went wrong: " + dex.Message;
            }
            return View();
        }
        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,F_name,L_name,Address,City,Country,Email,Phone,Postal,Dob, Allergies, HCN")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    return RedirectToAction("Admin");
                }
            }
            catch (DataException dex)
            {
                ViewBag.Message = "Whoops!Something went wrong. Here's what went wrong: " + dex.Message;
            }

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
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,F_name,L_name,Address,City,Country,Email,Phone,Postal,Dob, Allergies, HCN")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
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
            return RedirectToAction("Admin");
        }

        public JsonResult IsHCNAvailable(String Hcn, int PatientID)
        {
            return Json(!db.Patients.Any(x => x.patientId != PatientID && x.healthCardNum == Hcn), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmailAvailable(String Email, int PatientID)
        {
            return Json(!db.Patients.Any(x => x.patientId != PatientID && x.email == Email), JsonRequestBehavior.AllowGet);
        }

        // sample code
        public async Task<ActionResult> SendEmail(int id)
        {
            if (ModelState.IsValid)
            {
                var Patients = db.Patients.Find(id);
                string newEmail = Patients.email;
                string F_name = Patients.firstName;
                string L_name = Patients.lastName;
                MailMessage email = new MailMessage();
                string serverName = "smpt.gmail.com";
                string username = "wgao12333@gmail.com";
                string password = "password4444";
                int port = 587;
                email.To.Add(new MailAddress(newEmail));
                email.Subject = "Thank you for your submission";
                var message = "Thank you for your submission " + F_name + L_name;
                email.Body = string.Format(message);
                email.IsBodyHtml = true;
                email.BodyEncoding = System.Text.Encoding.UTF8;

                using (SmtpClient client = new SmtpClient(serverName, port))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);
                    await client.SendMailAsync(email);
                    ViewBag.sent = "Email sent";
                }
            }
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
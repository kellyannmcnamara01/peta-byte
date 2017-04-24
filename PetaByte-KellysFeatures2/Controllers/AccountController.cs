using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaByte_KellysFeatures2.Models;
using System.Net.Mail; // library specific for sending email via Simple Mail Transfer Protocol (SMTP); includes classes for creation of email message & passing of message to an SMTP server for sending
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.Entity;
using System.Net;
using System.Web.Security;

namespace PetaByte_KellysFeatures2.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        PetaByteContext db = new PetaByteContext();
        /*
     * Note(bryanstephens): next step - integrate Membership provider into projects to handle user validation & login (more secure; classes predefined to handle logging in, password reset, creation of user)
     */

            string param;

            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]
            // GET: Account
            public ActionResult Login(User user)
            {
                // ensure that password & email is to one user
                // count() ==> returns # of elements in sequence
                var count = db.Users.Where(u => u.Password.password1 == user.Password.password1 && u.Password.email == user.Password.email).Count();

                // if count returns 1
                if (count == 1)
                {
                    // manages form authentication; creates cookie for logged in user
                    FormsAuthentication.SetAuthCookie(user.Password.email, false);
                    Session["userId"] = user.userId.ToString();
                    Session["username"] = user.firstName + " " + user.lastName;
                    return RedirectToAction("Index", "Home");

                }
                // if count returns 0
                else
                {
                    ViewBag.Message = "Invalid User";

                    return View();
                }
            }

            public ActionResult Logout()
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            public ActionResult getPassword()
            {
                return View();
            }
            public ActionResult ResetPassword()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> ResetPassword(User user)
            {
                // check if modelstate is valid before proceeding
                if (ModelState.IsValid)
                {
                    /*
                     * Note(bryanstephens): when the user requests a password reset, a unique key is generated and sent to the requested user's email.
                     * This is done in order to validate the user. A reset password token must be unique, as well as only be valid for request for a
                     * password reset. If the user attempts to to reuse a reset password token, they will encounter an error.
                     */
                    // #1 ==> generate encrpyt key
                    // RNG ==> cryptographic Random Number Generator
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    //// byte ==> integral type; stores values (8-bit integer)
                    byte[] data = new byte[4];
                    //// store in byte to then call GetBytes() ==> fills byte array with result of rng
                    rng.GetBytes(data);
                    //// user BitConverter to change byte array into integer
                    int value = BitConverter.ToInt32(data, 0);
                    // #2 ==> assign encrpyt key to database
                    // LINQ ==> SELECT email FROM Password WHERE email = :email
                    var update = (from p in db.Passwords
                                  where p.email == user.Password.email
                                  select p).FirstOrDefault();
                    update.passwordReset = value.ToString();
                    // set reset token
                    // #3 ==> save token to database
                    db.SaveChanges();

                    /*
                        * Note(bryanstephens):  Once a password reset token has been requested, the must be notified of the change. B/c they are unable to log into their account, they will be sent a email (one that is registered in the system) in order to reset their password. Within the email, a link is also sent with the user, which contains a redirect to reset.cs, along with a reset token to authenticate the user's request
                    */
                    var firstname = db.Users.Where(x => x.Password.email == user.Password.email).FirstOrDefault().firstName;
                    var resetToken = db.Users.Where(x => x.Password.email == user.Password.email).FirstOrDefault().Password.passwordReset;
                    // body of email message
                    var emailBody = "<h3>Password Reset</h3> <p> Sorry to hear you forgot your password " + firstname + ". <a href=http://localhost:50213/account/reset?token=" + resetToken + "> click here to reset your password</a>";
                    // new instance of MailMessage
                    var msg = new MailMessage();
                    // receipient address
                    msg.To.Add(new MailAddress(user.Password.email));
                    msg.Subject = "Password Reset";
                    msg.Body = string.Format(emailBody);
                    // format body of email as html
                    msg.IsBodyHtml = true;
                    // smtp credentials
                    // ***credentials are stored in web.config file under system.net/mailSettings***
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(msg);
                        ViewBag.Sent = "Please check your email inbox for link to reset your password";
                    }
                }
                return View();
            }

            [HttpGet]
            public ActionResult reset()
            {
                param = Request.QueryString["token"];
                var validRequest = db.Users.Where(u => u.Password.passwordReset == param).Count();

                // if return is equal to 1 ==> user returns with valid reset password token
                if (validRequest == 1)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult reset(PasswordReset passwordReset)
            {
                /*
                *  Note(bryanstephens): token is a GET parameter received via an email user was sent to reset their password. 
                *  logic must check that the token belongs to a user, which allows them to reset their password.
                *  if the token is invalid, leave them no choice but to go back to Login Index
                *  Once a user navigates to Account/reset and reset's their password, token is destroyed.
                */
                if (ModelState.IsValid)
                {
                    param = Request.QueryString["token"];
                    // LINQ ==> SELECT email FROM Password WHERE email = :email
                    // Update password1 with value from resetpassword
                    var reset = (from p in db.Passwords
                                 where p.passwordReset == param
                                 select p).FirstOrDefault();
                    reset.email = db.Users.Where(x => x.Password.passwordReset == param).SingleOrDefault().Password.email;
                    reset.password1 = passwordReset.Password;
                    // set to reset token to NULL
                    reset.passwordReset = null;
                    db.SaveChanges();
                    ViewBag.Results = "Password reset";

                    return RedirectToAction("Login");
                }
                return View();
            }
            /*
             * Note(bryanstephens): This is for future work - To ensure user doesn't reuse same password, set up a constraint to checks against the password the user has already stored in DB.
             *  
             *   
             *  public JsonResult IsPasswordAlreadyInUse(User user, string Password)
             *  {
             *  return Json(!db.Users.Any(x => x.Password.email == user.Password.email && x.Password.password1 == Password),
             *  JsonRequestBehavior.AllowGet);
             *  }
             *  
             *  
             */
        }
    }

using NewsLetterAppMVC.Models;
using NewsLetterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsLetterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();
                //Below, another way to filter LINQ
                var signups = (from c in db.SignUps
                               where c.Removed == null
                               select c).ToList();
                var signupVms = new List<SignUpVm>();
                foreach (var signup in signups)
                {
                    var signupVm = new SignUpVm();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVms.Add(signupVm);
                }
                return View(signupVms);
            }
         }

        public ActionResult Unsubscribe(int Id)
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signup = db.SignUps.Find(Id);
                signup.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}


//----------------Admin function using ADO.NET

//string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, SocialSecurityNumber from SignUps";
//List<NewsletterSignUp> signups = new List<NewsletterSignUp>();

//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    SqlCommand command = new SqlCommand(queryString, connection);

//    connection.Open();

//    SqlDataReader reader = command.ExecuteReader();

//    while (reader.Read())
//    {
//        var signup = new NewsletterSignUp();
//        signup.Id = Convert.ToInt32(reader["Id"]);
//        signup.FirstName = reader["FirstName"].ToString();
//        signup.LastName = reader["LastName"].ToString();
//        signup.EmailAddress = reader["EmailAddress"].ToString();
//        signup.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();

//        signups.Add(signup);
//    }
//}
//var signupVms = new List<SignUpVm>();
//        foreach (var signup in signups)
//        {
//            var signupVm = new SignUpVm();
//signupVm.FirstName = signup.FirstName;
//            signupVm.LastName = signup.LastName;
//            signupVm.EmailAddress = signup.EmailAddress;
//            signupVms.Add(signupVm);
//        }

//        return View(signupVms);  
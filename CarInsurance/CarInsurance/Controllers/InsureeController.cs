using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Tables.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string FirstName, string LastName, string EmailAddress, DateTime DateOfBirth, int CarYear, string CarMake,string CarModel, bool DUI, int SpeedingTickets, bool CoverageType)
        {
            
                double Quote = 50.0;

                DateTime rightNow = DateTime.Today;
                int age = rightNow.Year - DateOfBirth.Year;

                if(age < 18)
                {
                   Quote += 100.0;
                }
                if(age > 19 && age < 25)
                {
                   Quote += 50.0;
                }
                if(age > 25)
                {
                   Quote += 25.0;
                }
                if(CarYear < 2000)
                {
                   Quote += 25.0;
                }
                if(CarYear > 2015)
                {
                   Quote += 25.0;
                }
                if(CarMake == "Porsche")
                {
                   Quote += 25.0;
                }
                if(CarMake == "Porsche" && CarModel == "911 Carrera")
                {
                   Quote += 25.0;
                }
                if(SpeedingTickets > 0)
                {
                    Quote += SpeedingTickets * 10;
                }
                if(DUI == true)
                {
                    Quote *= 1.25;
                }
                if(CoverageType == true)
                {
                    Quote *= 1.50;
                }

            using (InsuranceEntities db = new InsuranceEntities())
            {
                var newMember = new Table
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    EmailAddress = EmailAddress,
                    DateOfBirth = DateOfBirth,
                    CarYear = CarYear,
                    CarMake = CarMake,
                    CarModel = CarModel,
                    DUI = DUI,
                    SpeedingTickets = SpeedingTickets,
                    CoverageType = CoverageType,
                    Quote = Convert.ToDecimal(Quote),
                };
                db.Tables.Add(newMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
  
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Table table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = db.Tables.Find(id);
            db.Tables.Remove(table);
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

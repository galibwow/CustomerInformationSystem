using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerInformationSystem.Models;

namespace CustomerInformationSystem.Controllers
{
    public class CustomersController : Controller
    {
        private CustomerInfoEntities db = new CustomerInfoEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Category).Include(c => c.Designation).Include(c => c.Region);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CustomerCategory = new SelectList(db.Categories, "CatId", "CatName");
            ViewBag.ContactPersonPosition = new SelectList(db.Designations, "DesigId", "DesigName");
            ViewBag.ContactRegion = new SelectList(db.Regions, "RegId", "RegionName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomarId,CustomerCode,CustomerName,CustomerAddesss,PostCode,Telephone,Email,ContactPersonName,ContactPersonPosition,ContactRegion,CustomerCategory")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerCategory = new SelectList(db.Categories, "CatId", "CatName", customer.CustomerCategory);
            ViewBag.ContactPersonPosition = new SelectList(db.Designations, "DesigId", "DesigName", customer.ContactPersonPosition);
            ViewBag.ContactRegion = new SelectList(db.Regions, "RegId", "RegionName", customer.ContactRegion);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerCategory = new SelectList(db.Categories, "CatId", "CatName", customer.CustomerCategory);
            ViewBag.ContactPersonPosition = new SelectList(db.Designations, "DesigId", "DesigName", customer.ContactPersonPosition);
            ViewBag.ContactRegion = new SelectList(db.Regions, "RegId", "RegionName", customer.ContactRegion);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomarId,CustomerCode,CustomerName,CustomerAddesss,PostCode,Telephone,Email,ContactPersonName,ContactPersonPosition,ContactRegion,CustomerCategory")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerCategory = new SelectList(db.Categories, "CatId", "CatName", customer.CustomerCategory);
            ViewBag.ContactPersonPosition = new SelectList(db.Designations, "DesigId", "DesigName", customer.ContactPersonPosition);
            ViewBag.ContactRegion = new SelectList(db.Regions, "RegId", "RegionName", customer.ContactRegion);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

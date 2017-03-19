using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcWebApp.Contexts;
using MvcWebApp.Entities;

namespace MvcWebApp.Controllers
{
    [Authorize]
    public class ContactsAsyncController : Controller
    {
        private MvcWebAppContext db = new MvcWebAppContext();

        // GET: ContactsAsync
        public async Task<ActionResult> Index()
        {
            return View(await db.Contacts.ToListAsync());
        }

        // GET: ContactsAsync/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: ContactsAsync/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsAsync/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,EmailAddress,BirthDate,NumberOfComputers")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //Search by name
        public ViewResult Search(string FirstName, string LastName)
        {
            var contacts = from c in db.Contacts
                           select c;
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(LastName))
                {
                    contacts = contacts.Where(l => l.LastName == LastName);
                }
                if (!String.IsNullOrEmpty(FirstName))
                {
                    contacts = contacts.Where(l => l.FirstName == FirstName);
                }
            }
            return View(contacts.ToList());
        }

        public ViewResult Summary()
        {
            ContactsSummary summary = new ContactsSummary();

            summary.NumberOfContacts = db.Contacts.Count();
            summary.TotalOfComputers = db.Contacts.AsEnumerable().Sum(t => t.NumberOfComputers);
            ViewBag.Summary = summary;

            IQueryable<AddressesPerContact> data = from address in db.Addresses join contacts in db.Contacts on address.ContactID equals contacts.ID
                                                   group new { contacts.FirstName, contacts.LastName, address.ContactID } by address.ContactID into addressGroup
                                                   select new AddressesPerContact
                                                   {
                                                       FirstName = addressGroup.Select(f => f.FirstName).FirstOrDefault().ToString(),
                                                       LastName = addressGroup.Select(f => f.LastName).FirstOrDefault().ToString(),
                                                       NumberOfAddresses = addressGroup.Count()
                                                   };
            return View(data.ToList());
        }

        // GET: ContactsAsync/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: ContactsAsync/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,EmailAddress,BirthDate,NumberOfComputers")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: ContactsAsync/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: ContactsAsync/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
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

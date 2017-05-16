using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapitoleWebApi;
using CapitoleWebApi.Models;

namespace CapitoleWebApi.Controllers
{
    ///<summary>
    ///Class type controller to Facts.
    ///</summary>
    ///<remarks>
    ///CRUD using Entity Framework.
    ///Too create a new method for get random fact.
    ///</remarks> 
    public class factsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: facts
        public async Task<ActionResult> Index()
        {
            return View(await db.facts.ToListAsync());
        }

        // GET: facts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fact fact = await db.facts.FindAsync(id);
            if (fact == null)
            {
                return HttpNotFound();
            }
            return View(fact);
        }

        // GET: facts/Random
        ///<summary>
        ///Get random fact.
        ///</summary>
        ///<return>
        ///Return random fact.
        ///</return>
        public ActionResult Random()
        {
            // Fact identify.
            int id;

            Random rnd = new Random();

            // Get Random number. Range values between 1 to max row number to facts.
            id = rnd.Next(1, db.facts.Count());

            // Not use search async because need show partial view in other view. (Index.cshtml)
            fact fact = db.facts.Find(id);
            if (fact == null)
            {
                return HttpNotFound();
            }

            // Necesary for no show footer page.
            return PartialView(fact);
        }

        // GET: facts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: facts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,description")] fact fact)
        {
            if (ModelState.IsValid)
            {
                db.facts.Add(fact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fact);
        }

        // GET: facts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fact fact = await db.facts.FindAsync(id);
            if (fact == null)
            {
                return HttpNotFound();
            }
            return View(fact);
        }

        // POST: facts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,description")] fact fact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fact);
        }

        // GET: facts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fact fact = await db.facts.FindAsync(id);
            if (fact == null)
            {
                return HttpNotFound();
            }
            return View(fact);
        }

        // POST: facts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            fact fact = await db.facts.FindAsync(id);
            db.facts.Remove(fact);
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

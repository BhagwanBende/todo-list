using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TODO.Models;

namespace TODO.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using TODO.Models;
    using System.Data.Entity;

    public class TasksController : Controller
    {
        private ToDoDbContext db = new ToDoDbContext();

        // ✅ GET: Tasks (List with Search Functionality)
        [HttpGet]
        public ActionResult Index(string searchQuery = null)
        {
            var tasks = db.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchQuery) || t.Description.Contains(searchQuery));
            }

            return View(tasks.ToList());
        }

        // ✅ GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Task task = db.Tasks.Find(id);
            if (task == null)
                return HttpNotFound();

            return View(task);
        }

        // ✅ GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // ✅ POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,IsCompleted")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // ✅ GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Task task = db.Tasks.Find(id);
            if (task == null)
                return HttpNotFound();

            return View(task);
        }

        // ✅ POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,IsCompleted")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // ✅ GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Task task = db.Tasks.Find(id);
            if (task == null)
                return HttpNotFound();

            return View(task);
        }

        // ✅ POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ✅ Dispose Database Connection
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}

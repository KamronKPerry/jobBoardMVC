using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPJobBoard.DATA;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet;


namespace FPJobBoard.UI.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private FPJobBoardEntities db = new FPJobBoardEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            if (User.IsInRole("Admin"))
            {
                ViewBag.Title = "All Applications";
                var applications = db.Applications.Include(a => a.OpenPosition).Include(a => a.AspNetUser);
                return View(applications.ToList());
            }
            else if (User.IsInRole("Manager"))
            {
                ViewBag.Title = "Applications to Your Store";
                var locations = from l in db.Locations where l.ManagerID == user select l;
                //var specLoc = from z in db.Locations where z.LocationID == locations select z;
                //var positions = from p in db.OpenPositions where p.LocationID.ToString() == locations.ToString() select p;
                // var storeApps = from a in db.Applications where ( from p in db.OpenPositions where p.LocationID == (from l in db.Locations where l.ManagerID == user select l.LocationID ) select p) select a;
                var storeApplications = (db.Applications.Where(a => a.OpenPosition.Location.ManagerID == user));//.Include(a => a.OpenPosition).Include(a => a.AspNetUser));
                return View(storeApplications.ToList());
            }
            ViewBag.Title = "Your Applications";
            var myApplications = (db.Applications.Include(a => a.OpenPosition).Include(a => a.AspNetUser)).Where(a => a.UserID == user);
            return View(myApplications.ToList());

        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "PositionName");
            ViewBag.UserID = new SelectList(db.AspNetUsers.Where(y => y.Id == user), "Id", "FullName");
            return View(new Application());
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,OpenPositionID,UserID,ApplicationDate,ManagerNotes,IsDeclined,ResumeFilename")] Application application)
        {
           var user = User.Identity.GetUserId();
           // var applicant = ;
            //application.ResumeFilename = applicant.ToString();
                //from r in db.AspNetUsers where r.
            if (ModelState.IsValid)
            {
                application.ApplicationDate = DateTime.Now;
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.AspNetUsers.Where(y => y.Id == user), "Id", "FirstName", application.UserID);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "FirstName", application.UserID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin,Manager")]
        public ActionResult Edit([Bind(Include = "ApplicationID,OpenPositionID,UserID,ApplicationDate,ManagerNotes,IsDeclined,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "FirstName", application.UserID);
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Toggle(int? id)
        {
            Application application = db.Applications.Find(id);
            if (application.IsDeclined)
            {
            application.IsDeclined = false;

            }
            else
            {
                application.IsDeclined = true;
            }
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

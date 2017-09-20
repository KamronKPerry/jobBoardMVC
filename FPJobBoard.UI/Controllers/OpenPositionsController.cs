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

namespace FPJobBoard.UI.Controllers
{
    public class OpenPositionsController : Controller
    {
        private FPJobBoardEntities db = new FPJobBoardEntities();

        // GET: OpenPositions
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            if (User.IsInRole("Manager"))
            {
                ViewBag.Title = "Positions in your store";
                var locationPositions = (db.OpenPositions.Include(y => y.Location).Include(y => y.Position)).Where(y => y.Location.ManagerID == user);
                return View(locationPositions.ToList());
            }
            else if(User.IsInRole("Admin"))
            {
                ViewBag.Title = "All Positions";
            var allPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
            return View(allPositions.ToList());

            }
            ViewBag.Title = "Available Positions";
            var openPositions = (db.OpenPositions.Include(o => o.Location).Include(o => o.Position)).Where(o => o.IsOpen == true);
            return View(openPositions.ToList());
        }

        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // GET: OpenPositions/Create
        [Authorize(Roles ="Admin,Manager")]
        public ActionResult Create()
        {
            var id = User.Identity.GetUserId();
            var locations = from l in db.Locations where l.ManagerID == id select l;
            if (User.IsInRole("Manager"))
            {
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
            }
            else { 
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");}
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title");
            return View();
        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create([Bind(Include = "OpenPositionID,PositionID,LocationID,IsOpen")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var id = User.Identity.GetUserId();
            var locations = from l in db.Locations where l.ManagerID == id select l;
            if (User.IsInRole("Manager"))
            {
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
            }
            else
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            }
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // GET: OpenPositions/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            var user = User.Identity.GetUserId();
            var locations = from l in db.Locations where l.ManagerID == user select l;
            if (User.IsInRole("Manager"))
            {
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
            }
            else
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            }
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit([Bind(Include = "OpenPositionID,PositionID,LocationID,IsOpen")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var user = User.Identity.GetUserId();
            var locations = from l in db.Locations where l.ManagerID == user select l;
            if (User.IsInRole("Manager"))
            {
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
            }
            else
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            }
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // GET: OpenPositions/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
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

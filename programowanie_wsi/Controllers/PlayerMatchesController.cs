using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using programowanie_wsi.Models;

namespace programowanie_wsi.Controllers
{
    public class PlayerMatchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerMatch playerMatch = db.PlayerMatch.Find(id);
            if (playerMatch == null)
            {
                return HttpNotFound();
            }
           
            return View(playerMatch);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerMatch playerMatch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerMatch).State = EntityState.Modified;
                db.SaveChanges();
                
              return RedirectToAction("Details", "Matches", new { id = playerMatch.MatchID });
            }
          
            return View(playerMatch);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerMatch playerMatch = db.PlayerMatch.Find(id);
            if (playerMatch == null)
            {
                return HttpNotFound();
            }
            return View(playerMatch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerMatch playerMatch = db.PlayerMatch.Find(id);
            db.PlayerMatch.Remove(playerMatch);
            db.SaveChanges();
            return RedirectToAction("Details", "Matches", new { id = playerMatch.MatchID });
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

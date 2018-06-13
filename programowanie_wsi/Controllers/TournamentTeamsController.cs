using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using programowanie_wsi.Models;
using Microsoft.AspNet.Identity;

namespace programowanie_wsi.Controllers
{
    public class TournamentTeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tr = db.Tournament.Where(s => s.TournamentTeam.Any(c => c.TeamID == id));
            if(tr == null)
            {
                tr = null;
            }
            var team = db.Team.Where(s => s.TeamID == id).SingleOrDefault();
            TournamentTeamsViewModel tw = new TournamentTeamsViewModel
            {
                TournamentList = tr,
                Team = team
               
            };

            return View(tw);
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

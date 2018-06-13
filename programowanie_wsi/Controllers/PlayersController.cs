using programowanie_wsi.Authorize;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Models
{
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // id = id turnieju 
        public ActionResult Index(int? id, int? ids)
        {
            if (id == null && ids ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!ids.HasValue)
            {
                var playerlist1 = db.Player.Where(s => s.PlayerTeam.Any(c => c.TeamID == id && !c.TournamentID.HasValue && c.delete == false)).Include(p=>p.Position);
                playerlist1.ToList();
                PlayerTeamViewModel pl = new PlayerTeamViewModel
                {
                    PlayerList = playerlist1,
                    Team = db.Team.Where(c => c.TeamID == id).SingleOrDefault(),
                    Tournament = null
                    
                };
                return View(pl);
            }
            else {
                var playerlist = db.Player.Where(s => s.PlayerTeam.Any(c => c.TeamID == id && c.TournamentID == ids && c.delete == false)).Include(p=> p.Position);
                playerlist.ToList();

                PlayerTeamViewModel pl = new PlayerTeamViewModel
                {
                    PlayerList = playerlist,
                    Team = db.Team.Where(c => c.TeamID == id).SingleOrDefault(),
                    Tournament = db.Tournament.Where(c => c.TournamentID == ids).SingleOrDefault()
                };
                ViewBag.TeamID = ids;
                return View(pl);
            }
        }
        
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Player.Find(id);
            var list = db.PlayerTeam.Include(p => p.Tournament).Where(s=>s.PlayerID==id && s.TournamentID.HasValue);
            
            if(list != null)
            {
                ViewBag.List = list;
            }
         
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

      
        public ActionResult Create(int? id,int? ids)
        {
           
            TournamnetViewModel obj = new TournamnetViewModel();
            obj.TournamentTeam = db.TournamentTeam.Include(p => p.Tournament).Where(s => s.TeamID == id).ToList();

            ViewBag.TournamentID = ids;
            ViewBag.PositionID = new SelectList(db.Position, "PostionID", "position");
            return View(obj);
        
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamnetViewModel obj, int? id, int ?ids)
        {
            if (ModelState.IsValid)
            {
                db.Player.Add(obj.Player);

                if (!ids.HasValue)
                {

                    //dodanie zawodnika o nullowym ID turnieju
                    db.PlayerTeam.Add(new PlayerTeam()
                    {
                        PlayerID = obj.Player.PlayerID,
                        TeamID = (int)id

                    });
                }
                    //znalezienie wszystkich turniejów drużyny i dodanie zawodnika do wsyzstkich turniejów które rozgrywają 
                    if (obj.tournamnetList != null)
                    {
                        foreach (int item in obj.tournamnetList)
                    {
                            db.PlayerTeam.Add(new PlayerTeam()
                            {
                                PlayerID = obj.Player.PlayerID,
                                TeamID = (int)id,
                                TournamentID = item
                        });
                    }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index","Players", new { ids, id });
            }
            ViewBag.PositionID = new SelectList(db.Position, "PostionID", "position", obj.Player.PositionID);
            return View(obj.Player);
        }

        //[AuthorizePlayerAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionID = new SelectList(db.Position, "PostionID", "position", player.PositionID);

            return View(player);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id=player.PlayerID });
            }
            ViewBag.PositionID = new SelectList(db.Position, "PostionID", "position", player.PositionID);
            return View(player);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Player.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? ids, int? idss)
        {
            if(idss.HasValue)
            {
                var player = db.PlayerTeam.Where(s => s.PlayerID == id && s.TeamID == ids && s.TournamentID == idss).SingleOrDefault();
                player.delete = true;
            }
            else
            {
                var player = db.PlayerTeam.Where(s => s.PlayerID == id && s.TeamID == ids).ToList();
                foreach(var value in player)
                {
                    value.delete = true;
                }
            }
           
            db.SaveChanges();
            return RedirectToAction("Index", "Players", new { id = ids, ids = idss });
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

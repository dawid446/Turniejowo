using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using programowanie_wsi.Models;
using programowanie_wsi.Authorize;
using System.Globalization;

namespace programowanie_wsi.Controllers
{
    public class MatchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         
      
        public ActionResult Index(int id)
        {
            var lista = db.Match.Where(s => s.TournamentID == id).ToList();
            var druzyny = db.Team.Where(s => s.TournamentTeam.Any(c => c.TournamentID == id));
             
            List<Statistics> statystyka = new List<Statistics>();
            foreach (Team value in druzyny)
            {
                   statystyka.Add( new Statistics() {
                       id_file = value.FileID,
                       id_team = value.TeamID,
                       team_name = value.team_name
                    
                   });
            }
            foreach (Match value in lista)
            {
                if(value.isPlayed == true)
                {
                    var team = statystyka.Where(d => d.id_team == value.TeamID).FirstOrDefault();
                    var team1 = statystyka.Where(d => d.id_team == value.TeamID1).FirstOrDefault();
                    if (value.score == value.score1)
                    {
                            team.score_goal += value.score.GetValueOrDefault(); // jeśli jest nulem zwwraca zero
                            team.lose_goal += value.score1.GetValueOrDefault();
                            team.match++;
                            team.points += 1;
                            team.draw++;
                            team1.score_goal += value.score1.GetValueOrDefault(); // jeśli jest nulem zwwraca zero
                            team1.lose_goal += value.score.GetValueOrDefault();
                            team1.match++;
                            team1.points += 1;
                            team1.draw++;
                    }
                    if (value.score > value.score1)
                    {
                       
                            team.score_goal += value.score.GetValueOrDefault(); // jeśli jest nulem zwwraca zero
                            team.lose_goal += value.score1.GetValueOrDefault();
                            team.match++;
                            team.points += 3;
                            team.win++;     
                            team1.score_goal += value.score1.GetValueOrDefault(); // jeśli jest nulem zwwraca zero
                            team1.lose_goal += value.score.GetValueOrDefault();
                            team1.match++;
                            team1.lose++;
  
                    }
                    if (value.score < value.score1)
                    {
                            team1.score_goal += value.score.GetValueOrDefault(); // jeśli jest nulem zwwraca zero
                            team1.lose_goal += value.score1.GetValueOrDefault();
                            team1.match++;
                            team1.points += 3;
                            team1.win++;
                            team.score_goal += value.score1.GetValueOrDefault(); // jeśli jest nulem zwwraca zero
                            team.lose_goal += value.score.GetValueOrDefault();
                            team.match++;
                            team.lose++;
                    }

                }

            }
            List<Statistics> SortedList = statystyka.OrderByDescending(o => o.points).ThenByDescending(o=> o.match).ThenByDescending(o=>o.score_goal-o.lose_goal).ToList();
            ViewBag.List = SortedList;
            return View(lista);
        }


       
        public ActionResult Details(int? id, int? ids)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            MatchPlayerViewModel match = new MatchPlayerViewModel();
            match.Match = db.Match.Find(id);

            if (match.Match == null)
            {
                return HttpNotFound();
            }

            if (match.Match.LocationID != null)
            {
                string lat = match.Match.Google_Map.lat;
                string lng = match.Match.Google_Map.lng;

                ViewBag.lat = lat;
                ViewBag.lng = lng;

            }
            else
            {
                ViewBag.brak = "brak";
            }

            if (match.Match.isBreak == false)
            {

                if (match.Match.isPlayed == true)
                {
                    //lita zawodników biorących udzial 
                    match.listPlayerMatch = db.PlayerMatch.Where(s => s.Player.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID)).Where(s => s.MatchID == match.Match.MatchID).OrderBy(o => o.reserve ? 1 : 0).ToList();
                    match.listPlayerMatch1 = db.PlayerMatch.Where(s => s.Player.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID1)).Where(s => s.MatchID == match.Match.MatchID).OrderBy(o => o.reserve ? 1 : 0).ToList();

                    //kto bramke strzelił
                    match.goalList = db.PlayerMatch.Where(s => s.Player.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID)).Where(s => s.MatchID == match.Match.MatchID).Where(s => s.goal > 0);
                    match.goalList1 = db.PlayerMatch.Where(s => s.Player.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID1)).Where(s => s.MatchID == match.Match.MatchID).Where(s => s.goal > 0);

                }
                else
                {
                    var player = db.Player.Where(s => s.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID && c.TournamentID == ids)).OrderByDescending(o => o.number).ToList();
                    match.PlayerList = player;
                    var player1 = db.Player.Where(s => s.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID1 && c.TournamentID == ids)).OrderByDescending(o => o.number).ToList();
                    match.PlayerList1 = player1;
                }
            }else
            {
                return RedirectToAction("Index", "Matches", new { id = match.Match.TournamentID });
            }
            if (match == null)
            {
                return HttpNotFound();
            }

            return View(match);
        }


        [AuthorizeMatchesAttribute]
        public ActionResult Time(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
           Match match = db.Match.Find(id);

                if (match == null)
            {
                return HttpNotFound();
            }

            if (match.LocationID != null)
            {
                string lat = match.Google_Map.lat;
                string lng = match.Google_Map.lng;

                ViewBag.lat = lat;
                ViewBag.lng = lng;

            }
            else
            {
                ViewBag.lat = "51.919438";
                ViewBag.lng = "19.145136";
            }

            if (match.isBreak == true)
            {
                return RedirectToAction("Index", "Matches", new { id = match.TournamentID });
            }
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Time(Match match)
        {
            
                if (ModelState.IsValid)
            {
                var result = db.Match.SingleOrDefault(s => s.MatchID == match.MatchID);

                if(result.LocationID != null)
                {
                    result.Google_Map.lat = match.Google_Map.lat;
                    result.Google_Map.lng = match.Google_Map.lng;

                }
                else
                {
                    Google_map map = new Google_map
                    {
                        lat = match.Google_Map.lat,
                        lng = match.Google_Map.lng
                    };
                    db.Google_map.Add(map);
                    
                    result.LocationID = map.LocationID;
                }
                result.date = match.date;
                db.SaveChanges();

                return RedirectToAction("Index", "Matches", new { id = match.TournamentID });
            }
            ViewBag.lat = "51.919438";
            ViewBag.lng = "19.145136";
            return View(match);
        }

        [AuthorizeMatchesAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Match match = db.Match.Find(id);

                if (match == null)
            {
                return HttpNotFound();
            }

            if (match.isBreak == true)
            {
                return RedirectToAction("Index", "Matches", new { id = match.TournamentID });
            }
         
            return View(match);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Match match)

        {
           
                if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index", "Matches", new { id = match.TournamentID });
            }
            
             
            return View(match);
        }

        [AuthorizeMatchesAttribute]
        public ActionResult Create(int? id, int? ids)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            MatchPlayerViewModel match = new MatchPlayerViewModel();
            match.Match = db.Match.Find(id);

            if (match.Match.isBreak == true)
            {

                return RedirectToAction("Index", "Matches", new { id = match.Match.TournamentID });

            }

            if (match.Match == null)
            {
                return HttpNotFound();
            }

            if (match.Match.isPlayed == true)
            {

                return RedirectToAction("Details", "Matches", new { id = match.Match.MatchID, ids = match.Match.TournamentID });

            }
            
                var player = db.Player.Where(s => s.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID && c.TournamentID == ids && c.delete == false)).ToList();
                match.PlayerList = player;
                var player1 = db.Player.Where(s => s.PlayerTeam.Any(c => c.TeamID == match.Match.TeamID1 && c.TournamentID == ids && c.delete == false)).ToList();
                match.PlayerList1 = player1;

          
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchPlayerViewModel mv)
        {
            if (ModelState.IsValid)
            {
              if(mv.playerSelect != null)
                {
                    //pobranie wszystkich zawodników drużyn które biorą udział w turnieju
                    var player1 = db.Player.Where(s => s.PlayerTeam.Any(c => (c.TeamID == mv.Match.TeamID1 || c.TeamID == mv.Match.TeamID) &&
                    (c.TournamentID == mv.Match.TournamentID && c.TournamentID != null))).ToList();

                    if (player1 != null)
                    {
                        foreach (Player value in player1)
                        {
                            if (mv.playerSelect.Contains(value.PlayerID))
                            {
                                db.PlayerMatch.Add(new PlayerMatch()
                                {
                                    MatchID = mv.Match.MatchID,
                                    PlayerID = value.PlayerID,
                                });
                            }
                            else
                            {
                                db.PlayerMatch.Add(new PlayerMatch()
                                {
                                    MatchID = mv.Match.MatchID,
                                    PlayerID = value.PlayerID,
                                    reserve = true
                                });
                            }
                        }
                    }

                }
                mv.Match.isPlayed = true;
                db.Entry(mv.Match).State = EntityState.Modified;
                db.SaveChanges();

               return RedirectToAction("Details", "Matches", new { id = mv.Match.MatchID, ids = mv.Match.TournamentID });
            }
                return View(mv);
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

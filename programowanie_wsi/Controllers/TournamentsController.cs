using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using programowanie_wsi.Models;
using System.Collections.ObjectModel;
using programowanie_wsi.Authorize;
using PagedList;

namespace programowanie_wsi.Controllers
{
    public class TournamentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       [Authorize]
        public ActionResult Index(int? page)
        {
            string user = User.Identity.GetUserId();

            var lista = db.Tournament.Where(s => s.TournamentTeam.Any(c => c.Team.UserId == user)).ToList().OrderByDescending(o => o.date).ToPagedList(page ?? 1,5); 

            return View(lista);
        }

        [Authorize]
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();

            TournamentTeamViewModel obj = new TournamentTeamViewModel
            {
                Team = db.Team.Where(s => s.UserId == user && s.delete == false).ToList()
            };

            return View(obj);
            
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TournamentTeamViewModel obj)
        {
            if (obj.Team?.Count > 0 || obj.teamList?.Count() > 0)
            {
                int liczba = 0;
                int liczba1 = 0;
                if (obj.Team?.Count > 0)
                {
                    liczba = obj.Team.Count();
                }
                if(obj.teamList?.Count() > 0)
                {
                     liczba1= obj.teamList.Count();
                }
                if (liczba + liczba1 >= 3 && liczba + liczba1 <= 16)
                {

                    if (ModelState.IsValid)
                    {
                        string userID = User.Identity.GetUserId();
                        obj.Tournament.date = DateTime.Now;
                        obj.Tournament.name = obj.Tournament.name.Trim();
                        //dodanie turnieju i nazwy
                        db.Tournament.Add(obj.Tournament);

                        //dodanie drużyn do bazy danych
                        if (obj.Team?.Count > 0)
                        {
                            foreach (Team value in obj.Team)
                            {

                                value.UserId = userID;
                                value.FileID = 1;
                                db.Team.Add(value);
                            }

                            db.SaveChanges();
                        }
                        else obj.Team = new List<Team>();

                        //wyszukanie druzyn które brały udział w turnieju
                        if (obj.teamList?.Count() > 0)
                        {
                            var team = db.Team.Where(t => obj.teamList.Contains(t.TeamID)).ToList();
                            foreach (Team value in team)
                            {
                                var z = db.PlayerTeam.Where(c => c.TeamID == value.TeamID && !c.TournamentID.HasValue && c.delete == false);

                                foreach (PlayerTeam value1 in z)
                                {
                                    db.PlayerTeam.Add(new PlayerTeam()
                                    {
                                        PlayerID = value1.PlayerID,
                                        TeamID = value.TeamID,
                                        TournamentID = obj.Tournament.TournamentID

                                    });
                                }
                            }
                            //dodanie drużyn do jednej listy
                            obj.Team.AddRange(team);
                        }

                        foreach (Team value in obj.Team)
                        {
                            db.TournamentTeam.Add(new TournamentTeam()
                            {
                                TournamentID = obj.Tournament.TournamentID,
                                TeamID = value.TeamID
                            });

                        }

                        db.SaveChanges();

                        if (obj.Team.Count % 2 != 0)
                        {
                            obj.Team.Add(new Team()
                            {
                                TeamID = 1
                            });
                        }

                        obj.Team = obj.Team.OrderBy(a => Guid.NewGuid()).ToList();

                        int numTeams = obj.Team.Count();
                        int numDays = (numTeams - 1);
                        int halfSize = numTeams / 2;

                        List<Team> teams = new List<Team>();
                        teams.AddRange(obj.Team);
                        teams.RemoveAt(0);
                        int teamsSize = teams.Count;

                        for (int day = 0; day < numDays; day++)
                        {
                            int teamIdx = day % teamsSize;

                            Match match = new Match();

                            match.date = null;
                            match.isPlayed = false;
                            match.queue = day + 1;
                            match.TeamID = obj.Team.ElementAt(0).TeamID;
                            match.TeamID1 = teams.ElementAt(teamIdx).TeamID;
                            if (match.TeamID == 1)
                            {
                                match.isBreak = true;
                            }
                            if (match.TeamID1 == 1)
                            {
                                match.isBreak = true;
                            }
                            match.TournamentID = obj.Tournament.TournamentID;
                            db.Match.Add(match);
                            db.SaveChanges();

                            for (int idx = 1; idx < halfSize; idx++)
                            {
                                int firstTeam = (day + idx) % teamsSize;
                                int secondTeam = (day + teamsSize - idx) % teamsSize;

                                Match match1 = new Match();
                                match1.date = null;
                                match.isPlayed = false;
                                match1.TeamID = teams.ElementAt(firstTeam).TeamID;
                                match1.TeamID1 = teams.ElementAt(secondTeam).TeamID;
                                match1.queue = day + 1;
                                match1.TournamentID = obj.Tournament.TournamentID;
                                if (match1.TeamID == 1)
                                {
                                    match1.isBreak = true;
                                }
                                if (match1.TeamID1 == 1)
                                {
                                    match1.isBreak = true;
                                }
                                db.Match.Add(match1);
                                db.SaveChanges();

                            }
                        }

                        return RedirectToAction("Index", "Matches", new { id = obj.Tournament.TournamentID });
                    }
                    return View();
                }
                TempData["Error"] = "Nie można stworzyć turnieju drużyn musi być miedzy 3-16";
                return RedirectToAction("Create", "Tournaments");
            }
            TempData["Error"] = "Nie można stworzyć turnieju bez podania drużyn";
            return RedirectToAction("Create", "Tournaments");
        }
        
      

        [AuthorizeAuthorAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            Tournament tournament = db.Tournament.Find(id);

            if (tournament == null)
            {
                return HttpNotFound();
            }
            
            return View(tournament);
        
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
               // tournament.UserId = User.Identity.GetUserId();
                db.Entry(tournament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        [AuthorizeAuthorAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }

            return View(//tournament);
            */
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {/*
            Tournament tournament = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournament);
            db.SaveChanges();
            */
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

using programowanie_wsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
     
        public ActionResult Index(string searchBy,string search)
        {
            SearchViewModel sr = new SearchViewModel();
            
            if (search != null)
            {
               
                
                if (!String.IsNullOrEmpty(search))
                {
                    search =search.Trim();
                    
                    if (searchBy == "Tournamnet")
                    {
                        ViewBag.Category = "Tournament";
                        var tournamnet = db.Tournament.Where(s => s.name.Contains(search)).ToList();
                        sr.Tournament = tournamnet;
                        return View(sr);
                    }
                    if (searchBy == "Player")
                    {
                        ViewBag.Category = "Player";
                        var player = db.Player.Where(s => s.name.Contains(search) || s.surname.Contains(search)).ToList();
                        sr.Player = player;
                        return View(sr);
                    }
                    if (searchBy == "Team")
                    {
                        ViewBag.Category = "Team";
                        var team = db.Team.Where(s => s.team_name.Contains(search)).ToList();
                        sr.Team = team;
                        return View(sr);
                    }
                }
                else
                {
                    if (searchBy == "Tournamnet")
                    {
                        var tournament = db.Tournament.ToList();
                        sr.Tournament = tournament;
                        ViewBag.Category = "Tournament";
                        return View(sr);
                       
                    }
                    if (searchBy == "Player")
                    {
                        ViewBag.Category = "Player";
                        var player = db.Player.ToList();
                        sr.Player = player;
                        return View(sr);

                    }
                    if (searchBy == "Team")
                    {
                        ViewBag.Category = "Team";
                        var team = db.Team.ToList();
                        sr.Team = team;
                        return View(sr);
                    }

                }
               
            }
            ViewBag.Brak = "Wpisz dane do wyszukiwania";
            return View(sr);

        }

      
    }
}

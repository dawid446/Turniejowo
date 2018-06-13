using programowanie_wsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
           
           var showPiece = db.Tournament.OrderByDescending(p => p.TournamentID).Take(5);
           ViewBag.Turniej = showPiece;
         
            var liczba_druzyn = db.Team.Count();
            var liczba_turniej = db.Tournament.Count();

            var liczba_bramek = db.Match.Sum(s => s.score + s.score1);
            ViewBag.LiczbaTurniej = liczba_turniej;
            ViewBag.LiczbaDruzyn = liczba_druzyn;
            ViewBag.LiczbaBramek = liczba_bramek;
           
            return View();
        }

        public ActionResult About()
        {
           
            return View();
        }

        public ActionResult Contact()
        {
            
            return View();
        }
    }
}
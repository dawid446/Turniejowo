
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using programowanie_wsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return View();
        }

        public ActionResult Tournament()
        {
            var lista = db.Tournament.ToList();
            return View(lista); 
        }
        public ActionResult Team()
        {
            var lista = db.Team.ToList();
            return View(lista);
        }
        public ActionResult Player()
        {
            var lista = db.Player.ToList();
          

            return View(lista);
        }
      
    }
}
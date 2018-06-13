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
using programowanie_wsi.Authorize;

namespace programowanie_wsi.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var team = db.Team.Where(s => s.UserId == user && s.delete ==false).ToList();
            return View(team);
        }

        public ActionResult Details(int? id)
        {
             if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Include(s => s.File).SingleOrDefault(s => s.TeamID== id);

            var list = db.TournamentTeam.Include(p => p.Tournament).Where(s => s.TeamID == id);
            if (list != null)
            {
                ViewBag.List = list;
            }

            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [Authorize]
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team team, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),

                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    team.File = avatar;
                }
                else
                {
                    team.FileID = 1;

                }
                team.UserId = User.Identity.GetUserId();
                db.Team.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index", "Players", new { id = team.TeamID });
            }

            return View(team);
        }

        [AuthorizeTeamsAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Include(s => s.File).SingleOrDefault(s => s.TeamID == id);
            if (team == null)
            {
                return HttpNotFound();
            }
           
            return View(team);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Team team, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {


                var teamtoUpdate = db.Team.Find(id);

                teamtoUpdate.team_name = team.team_name;

                if (upload != null && upload.ContentLength > 0)
                {
                   
                   
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),

                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                    if (teamtoUpdate.FileID == 1)
                    {
                        team.File = avatar;
                        db.File.Add(team.File);
                        db.SaveChanges();
                        teamtoUpdate.FileID = avatar.FileID;
                    }
                    else
                    {
                        int i = teamtoUpdate.FileID;
                        team.File = avatar;
                        db.File.Add(team.File);
                        db.SaveChanges();
                        teamtoUpdate.FileID = avatar.FileID;
                        var itemtoremove = db.File.SingleOrDefault(x => x.FileID == i);
                        if (itemtoremove != null)
                        {
                            db.File.Remove(itemtoremove);
                           
                        }
                    }

                }

                db.SaveChanges();

                return RedirectToAction("Details",new { id= team.TeamID });
            }
        
            return View(team);
        }


        [AuthorizeTeamsAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var team = db.Team.Find(id);
            team.delete = true;
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

using Microsoft.AspNet.Identity;
using programowanie_wsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace programowanie_wsi.Authorize
{
    public class AuthorizePlayerAttribute : AuthorizeAttribute
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
                 var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                // brak autoryzacji - bazowe sprawdzenie
                return false;
            }
            bool admin = false;
            if (httpContext.User.IsInRole("Admin"))
            {
                admin = true;
            }


            // nazwa zalogowanego użytkownika
            var username = httpContext.User.Identity.GetUserId();

            // id zadania
            var id = httpContext.Request.RequestContext.RouteData.Values["id"] as string;
            var ids = httpContext.Request.RequestContext.RouteData.Values["ids"] as string;
            return IsUserOwnerOfArticle(username, id, admin);
        }

        private bool IsUserOwnerOfArticle(string username, string todoId, bool admin)
        {

            if (admin == true)
            {
                return true;
            }
            var test = db.PlayerTeam.Where(s => s.Team.UserId == username).ToList();
            if (!String.IsNullOrEmpty(todoId))
            {
                foreach (PlayerTeam result in test)

                {
                    if (result.PlayerID == Int32.Parse(todoId))
                    {
                        return true;
                    }

                }
                return false;

            }
            return false;
        }


    }
}
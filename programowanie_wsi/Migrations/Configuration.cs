namespace programowanie_wsi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using programowanie_wsi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<programowanie_wsi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(programowanie_wsi.Models.ApplicationDbContext context)
        {

            //context.Position.AddOrUpdate(x => x.PostionID,
            //    new Position { position = "Bramkarz" },
            //    new Position { position = "Obroñca" },
            //     new Position { position = "Pomocnik" },
            //      new Position { position = "Napastnik" }
                //);

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach(var roleName in roleNames)
            {

                if(!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));
                }
            }

            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //UserManager.AddToRole("e84e2f67-1df3-427e-85ae-e936f52ec975","Admin");

        }
    }
}

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services.Description;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System;

namespace programowanie_wsi.Models
{
    
    public class ApplicationUser : IdentityUser
    {
     

        public virtual ICollection<Team> Team { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
           
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
       
            return userIdentity;
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("myDatabaseConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
     

        public System.Data.Entity.DbSet<programowanie_wsi.Models.Tournament> Tournament { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.Team> Team { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.Match> Match { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.Player> Player { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.PlayerMatch> PlayerMatch { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.TournamentTeam> TournamentTeam { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.PlayerTeam> PlayerTeam { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.File> File { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.Position> Position { get; set; }
        public System.Data.Entity.DbSet<programowanie_wsi.Models.Google_map> Google_map { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                    .HasRequired(m => m.Team)
                    .WithMany(t => t.Match)
                    .HasForeignKey(m => m.TeamID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                        .HasRequired(m => m.Team1)
                        .WithMany(t => t.Match1)
                        .HasForeignKey(m => m.TeamID1)
                        .WillCascadeOnDelete(false);

            
                

        }


        // public System.Data.Entity.DbSet<programowanie_wsi.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}
namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.FileID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        team_name = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Files", t => t.FileID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FileID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        score = c.Int(),
                        score1 = c.Int(),
                        queue = c.Int(nullable: false),
                        date = c.DateTime(),
                        isPlayed = c.Boolean(nullable: false),
                        isBreak = c.Boolean(nullable: false),
                        TeamID = c.Int(nullable: false),
                        TeamID1 = c.Int(nullable: false),
                        TournamentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID)
                .ForeignKey("dbo.Tournaments", t => t.TournamentID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID)
                .ForeignKey("dbo.Teams", t => t.TeamID1)
                .Index(t => t.TeamID)
                .Index(t => t.TeamID1)
                .Index(t => t.TournamentID);
            
            CreateTable(
                "dbo.PlayerMatches",
                c => new
                    {
                        PlayerMatchID = c.Int(nullable: false, identity: true),
                        goal = c.Int(),
                        reserve = c.Boolean(nullable: false),
                        MatchID = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerMatchID)
                .ForeignKey("dbo.Matches", t => t.MatchID, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.MatchID)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        surname = c.String(),
                        number = c.Int(nullable: false),
                        height = c.Int(nullable: false),
                        weight = c.Int(nullable: false),
                        PositionID = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.Positions", t => t.PositionID)
                .Index(t => t.PositionID);
            
            CreateTable(
                "dbo.PlayerTeams",
                c => new
                    {
                        PlayerTeamID = c.Int(nullable: false, identity: true),
                        delete = c.Boolean(nullable: false),
                        PlayerID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                        TournamentID = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerTeamID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentID)
                .Index(t => t.PlayerID)
                .Index(t => t.TeamID)
                .Index(t => t.TournamentID);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        TournamentID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TournamentID);
            
            CreateTable(
                "dbo.TournamentTeams",
                c => new
                    {
                        TournamentTeamID = c.Int(nullable: false, identity: true),
                        TournamentID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TournamentTeamID)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentID, cascadeDelete: true)
                .Index(t => t.TournamentID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PostionID = c.Int(nullable: false, identity: true),
                        position = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PostionID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Teams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matches", "TeamID1", "dbo.Teams");
            DropForeignKey("dbo.Matches", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Players", "PositionID", "dbo.Positions");
            DropForeignKey("dbo.TournamentTeams", "TournamentID", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentTeams", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.PlayerTeams", "TournamentID", "dbo.Tournaments");
            DropForeignKey("dbo.Matches", "TournamentID", "dbo.Tournaments");
            DropForeignKey("dbo.PlayerTeams", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.PlayerTeams", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.PlayerMatches", "PlayerID", "dbo.Players");
            DropForeignKey("dbo.PlayerMatches", "MatchID", "dbo.Matches");
            DropForeignKey("dbo.Teams", "FileID", "dbo.Files");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TournamentTeams", new[] { "TeamID" });
            DropIndex("dbo.TournamentTeams", new[] { "TournamentID" });
            DropIndex("dbo.PlayerTeams", new[] { "TournamentID" });
            DropIndex("dbo.PlayerTeams", new[] { "TeamID" });
            DropIndex("dbo.PlayerTeams", new[] { "PlayerID" });
            DropIndex("dbo.Players", new[] { "PositionID" });
            DropIndex("dbo.PlayerMatches", new[] { "PlayerID" });
            DropIndex("dbo.PlayerMatches", new[] { "MatchID" });
            DropIndex("dbo.Matches", new[] { "TournamentID" });
            DropIndex("dbo.Matches", new[] { "TeamID1" });
            DropIndex("dbo.Matches", new[] { "TeamID" });
            DropIndex("dbo.Teams", new[] { "FileID" });
            DropIndex("dbo.Teams", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Positions");
            DropTable("dbo.TournamentTeams");
            DropTable("dbo.Tournaments");
            DropTable("dbo.PlayerTeams");
            DropTable("dbo.Players");
            DropTable("dbo.PlayerMatches");
            DropTable("dbo.Matches");
            DropTable("dbo.Teams");
            DropTable("dbo.Files");
        }
    }
}

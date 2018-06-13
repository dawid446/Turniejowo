namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TournamentTeams", "delete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TournamentTeams", "delete", c => c.Boolean(nullable: false));
        }
    }
}

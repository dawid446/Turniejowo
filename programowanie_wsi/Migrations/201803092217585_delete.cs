namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TournamentTeams", "delete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TournamentTeams", "delete");
        }
    }
}

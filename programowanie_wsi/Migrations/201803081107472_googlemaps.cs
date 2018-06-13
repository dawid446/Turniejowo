namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class googlemaps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Google_map",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        lat = c.Single(nullable: false),
                        lng = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            AddColumn("dbo.Matches", "LocationID", c => c.Int());
            CreateIndex("dbo.Matches", "LocationID");
            AddForeignKey("dbo.Matches", "LocationID", "dbo.Google_map", "LocationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "LocationID", "dbo.Google_map");
            DropIndex("dbo.Matches", new[] { "LocationID" });
            DropColumn("dbo.Matches", "LocationID");
            DropTable("dbo.Google_map");
        }
    }
}

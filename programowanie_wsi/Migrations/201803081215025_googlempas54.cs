namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class googlempas54 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Google_map", "lat", c => c.Double(nullable: false));
            AlterColumn("dbo.Google_map", "lng", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Google_map", "lng", c => c.Single(nullable: false));
            AlterColumn("dbo.Google_map", "lat", c => c.Single(nullable: false));
        }
    }
}

namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamdelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "delete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "delete");
        }
    }
}

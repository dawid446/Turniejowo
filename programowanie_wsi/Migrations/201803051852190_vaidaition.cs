namespace programowanie_wsi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vaidaition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Players", "surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "surname", c => c.String());
            AlterColumn("dbo.Players", "name", c => c.String());
        }
    }
}

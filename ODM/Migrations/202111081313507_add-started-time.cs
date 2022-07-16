namespace ODM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstartedtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "started", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "started");
        }
    }
}

namespace ODM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterequestattr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "state", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "state");
        }
    }
}

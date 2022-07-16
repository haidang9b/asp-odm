namespace ODM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "installDay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "installDay");
        }
    }
}

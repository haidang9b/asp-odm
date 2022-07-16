namespace ODM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "birthday", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.User", "email", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.User", "avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "avatar");
            DropColumn("dbo.User", "email");
            DropColumn("dbo.User", "birthday");
            DropColumn("dbo.User", "gender");
        }
    }
}

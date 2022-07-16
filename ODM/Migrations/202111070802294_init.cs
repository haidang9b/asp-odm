namespace ODM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Borrow",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        timeReceive = c.String(),
                        timeReturn = c.String(),
                        isReturn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Product", t => t.product_id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 300),
                        description = c.String(maxLength: 500),
                        image = c.String(maxLength: 300),
                        status = c.Boolean(nullable: false),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Category", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 100),
                        fullName = c.String(nullable: false, maxLength: 150),
                        password = c.String(nullable: false, maxLength: 150),
                        role_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Role", t => t.role_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        timeRequest = c.String(),
                        timeCompletion = c.String(),
                        isReturn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Product", t => t.product_id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "user_id", "dbo.User");
            DropForeignKey("dbo.Request", "product_id", "dbo.Product");
            DropForeignKey("dbo.Borrow", "user_id", "dbo.User");
            DropForeignKey("dbo.User", "role_id", "dbo.Role");
            DropForeignKey("dbo.Borrow", "product_id", "dbo.Product");
            DropForeignKey("dbo.Product", "category_id", "dbo.Category");
            DropIndex("dbo.Request", new[] { "product_id" });
            DropIndex("dbo.Request", new[] { "user_id" });
            DropIndex("dbo.User", new[] { "role_id" });
            DropIndex("dbo.Product", new[] { "category_id" });
            DropIndex("dbo.Borrow", new[] { "product_id" });
            DropIndex("dbo.Borrow", new[] { "user_id" });
            DropTable("dbo.Request");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Category");
            DropTable("dbo.Product");
            DropTable("dbo.Borrow");
        }
    }
}

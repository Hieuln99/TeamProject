namespace Project.EF.SystemMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FPTsystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        description = c.String(nullable: false),
                        categoryid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Category", t => t.categoryid, cascadeDelete: true)
                .Index(t => t.categoryid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "categoryid", "dbo.Category");
            DropIndex("dbo.Course", new[] { "categoryid" });
            DropTable("dbo.Course");
            DropTable("dbo.Category");
        }
    }
}

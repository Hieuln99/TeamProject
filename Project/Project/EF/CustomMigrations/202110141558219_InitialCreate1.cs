namespace Project.EF.CustomMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        CourseCategory_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CourseCategories", t => t.CourseCategory_id)
                .Index(t => t.CourseCategory_id);
            
            AddColumn("dbo.AspNetUsers", "Role", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Course_id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Course_id");
            AddForeignKey("dbo.AspNetUsers", "Course_id", "dbo.Courses", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CourseCategory_id", "dbo.CourseCategories");
            DropForeignKey("dbo.AspNetUsers", "Course_id", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "Course_id" });
            DropIndex("dbo.Courses", new[] { "CourseCategory_id" });
            DropColumn("dbo.AspNetUsers", "Course_id");
            DropColumn("dbo.AspNetUsers", "Role");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseCategories");
        }
    }
}

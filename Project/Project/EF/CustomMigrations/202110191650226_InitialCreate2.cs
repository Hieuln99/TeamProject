namespace Project.EF.CustomMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Course_id", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "Course_id" });
            CreateTable(
                "dbo.CustomUserCourses",
                c => new
                    {
                        CustomUser_Id = c.String(nullable: false, maxLength: 128),
                        Course_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomUser_Id, t.Course_id })
                .ForeignKey("dbo.AspNetUsers", t => t.CustomUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_id, cascadeDelete: true)
                .Index(t => t.CustomUser_Id)
                .Index(t => t.Course_id);
            
            DropColumn("dbo.AspNetUsers", "Course_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Course_id", c => c.Int());
            DropForeignKey("dbo.CustomUserCourses", "Course_id", "dbo.Courses");
            DropForeignKey("dbo.CustomUserCourses", "CustomUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CustomUserCourses", new[] { "Course_id" });
            DropIndex("dbo.CustomUserCourses", new[] { "CustomUser_Id" });
            DropTable("dbo.CustomUserCourses");
            CreateIndex("dbo.AspNetUsers", "Course_id");
            AddForeignKey("dbo.AspNetUsers", "Course_id", "dbo.Courses", "id");
        }
    }
}

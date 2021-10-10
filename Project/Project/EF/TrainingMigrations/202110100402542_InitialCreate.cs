namespace Project.EF.TrainingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
            
            CreateTable(
                "dbo.Trainee",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        username = c.String(nullable: false, maxLength: 50, unicode: false),
                        password = c.String(nullable: false),
                        age = c.Int(nullable: false),
                        dob = c.DateTime(nullable: false),
                        edu = c.String(nullable: false, maxLength: 50, unicode: false),
                        language = c.String(nullable: false, maxLength: 30, unicode: false),
                        toeic = c.Int(nullable: false),
                        exp = c.String(nullable: false),
                        department = c.String(nullable: false, maxLength: 50, unicode: false),
                        location = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Trainer",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        type = c.Boolean(nullable: false),
                        workplace = c.String(nullable: false, maxLength: 15, unicode: false),
                        email = c.String(nullable: false, maxLength: 30, unicode: false),
                        phonenumber = c.String(nullable: false, maxLength: 12, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        Trainee_id = c.Int(nullable: false),
                        Course_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trainee_id, t.Course_id })
                .ForeignKey("dbo.Trainee", t => t.Trainee_id, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_id, cascadeDelete: true)
                .Index(t => t.Trainee_id)
                .Index(t => t.Course_id);
            
            CreateTable(
                "dbo.TrainerCourses",
                c => new
                    {
                        Trainer_id = c.Int(nullable: false),
                        Course_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trainer_id, t.Course_id })
                .ForeignKey("dbo.Trainer", t => t.Trainer_id, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_id, cascadeDelete: true)
                .Index(t => t.Trainer_id)
                .Index(t => t.Course_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerCourses", "Course_id", "dbo.Course");
            DropForeignKey("dbo.TrainerCourses", "Trainer_id", "dbo.Trainer");
            DropForeignKey("dbo.TraineeCourses", "Course_id", "dbo.Course");
            DropForeignKey("dbo.TraineeCourses", "Trainee_id", "dbo.Trainee");
            DropForeignKey("dbo.Course", "categoryid", "dbo.Category");
            DropIndex("dbo.TrainerCourses", new[] { "Course_id" });
            DropIndex("dbo.TrainerCourses", new[] { "Trainer_id" });
            DropIndex("dbo.TraineeCourses", new[] { "Course_id" });
            DropIndex("dbo.TraineeCourses", new[] { "Trainee_id" });
            DropIndex("dbo.Course", new[] { "categoryid" });
            DropTable("dbo.TrainerCourses");
            DropTable("dbo.TraineeCourses");
            DropTable("dbo.Trainer");
            DropTable("dbo.Trainee");
            DropTable("dbo.Course");
            DropTable("dbo.Category");
        }
    }
}

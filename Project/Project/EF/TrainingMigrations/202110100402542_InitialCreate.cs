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

            Sql("INSERT INTO dbo.Category(name,description) VALUES" +
             "('Computing', 'Simply put, cloud computing is the delivery of computing services—including servers, storage, databases, networking, software, analytics, and intelligence—over the Internet (“the cloud”) to offer faster innovation, flexible resources, and economies of scale. You typically pay only for cloud services you use, helping you lower your operating costs, run your infrastructure more efficiently, and scale as your business needs change.')," +
             "('Business', 'Those looking for professional development into new areas will find an array of topics to choose from, including social media and entrepreneurship. Whether you’re looking for online management courses that last a couple of weeks or a business course that spans several months, you’re sure to find one that’s right for you.')," +
             "('Academic English', 'academic English is about expressing the relationship between ideas. Although the language may be more complex than in everyday English, good academic writers aim to be as clear, precise and simple as possible. They think about what their readers know already, and aim to guide them towards less familiar areas and topics.')," +
              "('Foundation', 'foundation subjects are not explored as thoroughly, they are still important because they introduce trainees to a wide variety of skills and knowledge. Foundation subjects also give a taster to trainees on what they enjoy and excel at doing to give them a clear idea on what to progress further in their education. ')," +
             "('Graphic Design', 'Graphic design is a craft where professionals create visual content to communicate messages. By applying visual hierarchy and page layout techniques, designers use typography and pictures to meet users’ specific needs and focus on the logic of displaying elements in interactive designs, to optimize the user experience.')");
            
            Sql("INSERT INTO dbo.Course(name, description, categoryid) VALUES" +
                "('Computing','Cloud computing is the on - demand delivery of IT resources over the Internet with a pay -as- you - go policy.Instead of buying, owning, and maintaining physical servers and data centers, you can access technology services, like compute power, storage, and databases, as needed, from cloud service providers such as Amazon Web Services(AWS)',1)," +
                "('Database','database is an organized collection of structured information, or data, typically stored electronically in a computer system. A database is usually controlled by a database management system (DBMS). Together, the data and the DBMS, along with the applications that are associated with them, are referred to as a database system, often shortened to just database',1)," +
                "('Internet of Thing','The internet of things, or IoT, is a system of interrelated computing devices, mechanical and digital machines, objects, animals or people that are provided with unique identifiers (UIDs) and the ability to transfer data over a network without requiring human-to-human or human-to-computer interaction.',3)," +
                "('Internet','The internet of things, or IoT, is a system of interrelated computing devices, mechanical and digital machines, objects, animals or people that are provided with unique identifiers (UIDs) and the ability to transfer data over a network without requiring human-to-human or human-to-computer interaction.',2)," +
                "('C#','C# is a strongly typed object-oriented programming language. C# is open source, simple, modern, flexible, and versatile. In this article, let’s learn what C# is, what C# can do, and how C# is different than C++ and other programming languages.', 5)");

            Sql("INSERT INTO dbo.Trainee(name, username, password, age, dob, edu, language, toeic, exp, department, location) VALUES" +
                "(1, 2)," +
                "(2, 1)," +
                "(1, 1)," +
                "(2, 2)");

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

namespace Project.EF.CustomMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "CourseCategory_id", newName: "categories_id");
            RenameIndex(table: "dbo.Courses", name: "IX_CourseCategory_id", newName: "IX_categories_id");
            AddColumn("dbo.Courses", "CatId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CatId");
            RenameIndex(table: "dbo.Courses", name: "IX_categories_id", newName: "IX_CourseCategory_id");
            RenameColumn(table: "dbo.Courses", name: "categories_id", newName: "CourseCategory_id");
        }
    }
}

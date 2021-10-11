namespace Project.EF.TrainingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainer", "username", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Trainer", "password", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainer", "password");
            DropColumn("dbo.Trainer", "username");
        }
    }
}

namespace Project.EF.TrainingMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        username = c.String(nullable: false, maxLength: 50, unicode: false),
                        password = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);



            Sql("INSERT INTO dbo.Staff( name, username, password) VALUES" +
               "('Mary','JoiMaguire', '0919919999')," +
                "('Mary', 'Maguire','0919919999')," +
                "('Mary', 'Maguire','0919919999')," +
                "('Mary', 'Maguire','0919919999')," +
                "('Mary', 'Maguire','0919919999')," +
                "('Mary', 'Maguire','0919919999')," +
                "('Tobey Maguire','Tobey','0919919999')");
        }
        
        public override void Down()
        {
            DropTable("dbo.Staff");
        }
    }
}

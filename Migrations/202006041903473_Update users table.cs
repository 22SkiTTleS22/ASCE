namespace ASCE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateuserstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "firstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "secondName", c => c.String());
            AddColumn("dbo.AspNetUsers", "patronymic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "patronymic");
            DropColumn("dbo.AspNetUsers", "secondName");
            DropColumn("dbo.AspNetUsers", "firstName");
        }
    }
}

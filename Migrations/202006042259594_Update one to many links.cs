namespace ASCE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateonetomanylinks : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Counters", "PAID");
            DropColumn("dbo.PersonalAccounts", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalAccounts", "UserId", c => c.Int());
            AddColumn("dbo.Counters", "PAID", c => c.Int());
        }
    }
}

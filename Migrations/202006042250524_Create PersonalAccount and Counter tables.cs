namespace ASCE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePersonalAccountandCountertables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PAID = c.Int(),
                        SerialNumber = c.String(),
                        Model = c.String(),
                        Manufacturer = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateVerification = c.DateTime(nullable: false),
                        DateVerificationNext = c.DateTime(nullable: false),
                        DateInstall = c.DateTime(nullable: false),
                        Capacity = c.Int(nullable: false),
                        SealNumber = c.String(),
                        InstallPlase = c.String(),
                        PersonalAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalAccounts", t => t.PersonalAccount_Id)
                .Index(t => t.PersonalAccount_Id);
            
            CreateTable(
                "dbo.PersonalAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Address = c.String(),
                        DateOpen = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counters", "PersonalAccount_Id", "dbo.PersonalAccounts");
            DropForeignKey("dbo.PersonalAccounts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PersonalAccounts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Counters", new[] { "PersonalAccount_Id" });
            DropTable("dbo.PersonalAccounts");
            DropTable("dbo.Counters");
        }
    }
}

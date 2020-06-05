namespace ASCE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        Model = c.String(),
                        Manufacturer = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DateVerification = c.DateTime(nullable: false),
                        DateVerificationNext = c.DateTime(nullable: false),
                        DateInstall = c.DateTime(nullable: false),
                        Capacity = c.Int(nullable: false),
                        SealNumber = c.String(),
                        InstallPlace = c.String(),
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
                        AccountNumber = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        DateOpen = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false),
                        Category = c.String(),
                        DateOpen = c.DateTime(nullable: false),
                        DateClose = c.DateTime(nullable: false),
                        Description = c.String(),
                        PersonalAccount_Id = c.Int(),
                        Service_Id = c.Int(),
                        Worker_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalAccounts", t => t.PersonalAccount_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .ForeignKey("dbo.Workers", t => t.Worker_Id)
                .Index(t => t.PersonalAccount_Id)
                .Index(t => t.Service_Id)
                .Index(t => t.Worker_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Verifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtoclImg = c.Binary(),
                        СertificateImg = c.Binary(),
                        Counter_Id = c.Int(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Counters", t => t.Counter_Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.Counter_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 25),
                        SecondName = c.String(maxLength: 25),
                        Patronymic = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Verifications", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Verifications", "Counter_Id", "dbo.Counters");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Requests", "Worker_Id", "dbo.Workers");
            DropForeignKey("dbo.Requests", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Requests", "PersonalAccount_Id", "dbo.PersonalAccounts");
            DropForeignKey("dbo.Counters", "PersonalAccount_Id", "dbo.PersonalAccounts");
            DropForeignKey("dbo.PersonalAccounts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.Verifications", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Verifications", new[] { "Counter_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Requests", new[] { "Worker_Id" });
            DropIndex("dbo.Requests", new[] { "Service_Id" });
            DropIndex("dbo.Requests", new[] { "PersonalAccount_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUsers", "UserNameIndex");
            DropIndex("dbo.PersonalAccounts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Counters", new[] { "PersonalAccount_Id" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Verifications");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workers");
            DropTable("dbo.Services");
            DropTable("dbo.Requests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.PersonalAccounts");
            DropTable("dbo.Counters");
        }
    }
}

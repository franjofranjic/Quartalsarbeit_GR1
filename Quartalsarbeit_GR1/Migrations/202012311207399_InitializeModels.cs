namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anlasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                        Ort = c.String(),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Anlass_ID = c.Int(),
                        Kategorie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Anlasses", t => t.Anlass_ID)
                .ForeignKey("dbo.Kategories", t => t.Kategorie_ID)
                .Index(t => t.Anlass_ID)
                .Index(t => t.Kategorie_ID);
            
            CreateTable(
                "dbo.Disziplins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                        Abkuerzung = c.String(),
                        Formel = c.String(),
                        Config_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configs", t => t.Config_ID)
                .Index(t => t.Config_ID);
            
            CreateTable(
                "dbo.Kategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bezeichnung = c.String(),
                        MinAlter = c.Int(nullable: false),
                        MaxAlter = c.Int(nullable: false),
                        Geschlecht = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Athlets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Vorname = c.String(),
                        Nachname = c.String(),
                        Geburtstag = c.DateTime(nullable: false),
                        Geschlecht = c.Int(nullable: false),
                        Gewicht = c.Int(nullable: false),
                        Groesse = c.Int(nullable: false),
                        Verein_ID = c.Int(),
                        Anlass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Vereins", t => t.Verein_ID)
                .ForeignKey("dbo.Anlasses", t => t.Anlass_ID)
                .Index(t => t.Verein_ID)
                .Index(t => t.Anlass_ID);
            
            CreateTable(
                "dbo.Vereins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Vereinsname = c.String(),
                        Ort = c.String(),
                        PLZ = c.Int(nullable: false),
                        Strasse = c.String(),
                        Vereinsverantwortlicher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Vereinsverantwortlicher_Id)
                .Index(t => t.Vereinsverantwortlicher_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Vorname = c.String(nullable: false, maxLength: 255),
                        Nachname = c.String(nullable: false, maxLength: 255),
                        TelPrivat = c.Int(nullable: false),
                        TelGeschaeft = c.Int(nullable: false),
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
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Athlets", "Anlass_ID", "dbo.Anlasses");
            DropForeignKey("dbo.Vereins", "Vereinsverantwortlicher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Athlets", "Verein_ID", "dbo.Vereins");
            DropForeignKey("dbo.Configs", "Kategorie_ID", "dbo.Kategories");
            DropForeignKey("dbo.Disziplins", "Config_ID", "dbo.Configs");
            DropForeignKey("dbo.Configs", "Anlass_ID", "dbo.Anlasses");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Vereins", new[] { "Vereinsverantwortlicher_Id" });
            DropIndex("dbo.Athlets", new[] { "Anlass_ID" });
            DropIndex("dbo.Athlets", new[] { "Verein_ID" });
            DropIndex("dbo.Disziplins", new[] { "Config_ID" });
            DropIndex("dbo.Configs", new[] { "Kategorie_ID" });
            DropIndex("dbo.Configs", new[] { "Anlass_ID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Vereins");
            DropTable("dbo.Athlets");
            DropTable("dbo.Kategories");
            DropTable("dbo.Disziplins");
            DropTable("dbo.Configs");
            DropTable("dbo.Anlasses");
        }
    }
}

namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teilnehmers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Startnummer = c.Int(nullable: false),
                        Anlass_ID = c.Int(),
                        Athlet_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Anlasses", t => t.Anlass_ID)
                .ForeignKey("dbo.Teilnehmers", t => t.Athlet_ID)
                .Index(t => t.Anlass_ID)
                .Index(t => t.Athlet_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teilnehmers", "Athlet_ID", "dbo.Teilnehmers");
            DropForeignKey("dbo.Teilnehmers", "Anlass_ID", "dbo.Anlasses");
            DropIndex("dbo.Teilnehmers", new[] { "Athlet_ID" });
            DropIndex("dbo.Teilnehmers", new[] { "Anlass_ID" });
            DropTable("dbo.Teilnehmers");
        }
    }
}

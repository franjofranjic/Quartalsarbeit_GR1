namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Startnummernblock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Startnummernblocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        minStartnummer = c.Int(nullable: false),
                        maxStartnummer = c.Int(nullable: false),
                        gruppierung = c.String(),
                        differenz = c.Int(nullable: false),
                        anlass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Anlasses", t => t.anlass_ID)
                .Index(t => t.anlass_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Startnummernblocks", "anlass_ID", "dbo.Anlasses");
            DropIndex("dbo.Startnummernblocks", new[] { "anlass_ID" });
            DropTable("dbo.Startnummernblocks");
        }
    }
}

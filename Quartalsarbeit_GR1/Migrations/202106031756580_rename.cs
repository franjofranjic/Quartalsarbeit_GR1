namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Anlasses", newName: "Events");
            RenameTable(name: "dbo.Configs", newName: "Configurations");
            RenameTable(name: "dbo.Disziplins", newName: "Disciplines");
            RenameTable(name: "dbo.Kategories", newName: "Categories");
            RenameTable(name: "dbo.Athlets", newName: "Athletes");
            RenameTable(name: "dbo.Vereins", newName: "Clubs");
            RenameTable(name: "dbo.Startnummernblocks", newName: "StartNumberConfigurations");
            RenameTable(name: "dbo.Teilnehmers", newName: "Participants");
            RenameColumn(table: "dbo.Athletes", name: "Anlass_ID", newName: "Event_ID");
            RenameIndex(table: "dbo.Athletes", name: "IX_Anlass_ID", newName: "IX_Event_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Athletes", name: "IX_Event_ID", newName: "IX_Anlass_ID");
            RenameColumn(table: "dbo.Athletes", name: "Event_ID", newName: "Anlass_ID");
            RenameTable(name: "dbo.Participants", newName: "Teilnehmers");
            RenameTable(name: "dbo.StartNumberConfigurations", newName: "Startnummernblocks");
            RenameTable(name: "dbo.Clubs", newName: "Vereins");
            RenameTable(name: "dbo.Athletes", newName: "Athlets");
            RenameTable(name: "dbo.Categories", newName: "Kategories");
            RenameTable(name: "dbo.Disciplines", newName: "Disziplins");
            RenameTable(name: "dbo.Configurations", newName: "Configs");
            RenameTable(name: "dbo.Events", newName: "Anlasses");
        }
    }
}

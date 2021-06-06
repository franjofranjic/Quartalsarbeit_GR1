namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParticipantUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Participants", name: "Anlass_ID", newName: "Event_ID");
            RenameColumn(table: "dbo.Participants", name: "Athlet_ID", newName: "Athlete_ID");
            RenameIndex(table: "dbo.Participants", name: "IX_Athlet_ID", newName: "IX_Athlete_ID");
            RenameIndex(table: "dbo.Participants", name: "IX_Anlass_ID", newName: "IX_Event_ID");
            AddColumn("dbo.Participants", "StartNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Participants", "Startnummer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Startnummer", c => c.Int(nullable: false));
            DropColumn("dbo.Participants", "StartNumber");
            RenameIndex(table: "dbo.Participants", name: "IX_Event_ID", newName: "IX_Anlass_ID");
            RenameIndex(table: "dbo.Participants", name: "IX_Athlete_ID", newName: "IX_Athlet_ID");
            RenameColumn(table: "dbo.Participants", name: "Athlete_ID", newName: "Athlet_ID");
            RenameColumn(table: "dbo.Participants", name: "Event_ID", newName: "Anlass_ID");
        }
    }
}

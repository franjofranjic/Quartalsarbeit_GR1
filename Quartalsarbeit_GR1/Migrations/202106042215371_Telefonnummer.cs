namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Telefonnummer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "TelPrivat", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "TelGeschaeft", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "TelGeschaeft", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "TelPrivat", c => c.Int(nullable: false));
        }
    }
}

namespace Quartalsarbeit_GR1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Disziplins", "Config_ID", "dbo.Configs");
            DropIndex("dbo.Disziplins", new[] { "Config_ID" });
            AddColumn("dbo.Configs", "Disziplin_ID", c => c.Int());
            CreateIndex("dbo.Configs", "Disziplin_ID");
            AddForeignKey("dbo.Configs", "Disziplin_ID", "dbo.Disziplins", "ID");
            //DropColumn("dbo.Disziplins", "Config_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Disziplins", "Config_ID", c => c.Int());
            DropForeignKey("dbo.Configs", "Disziplin_ID", "dbo.Disziplins");
            DropIndex("dbo.Configs", new[] { "Disziplin_ID" });
            DropColumn("dbo.Configs", "Disziplin_ID");
            CreateIndex("dbo.Disziplins", "Config_ID");
            AddForeignKey("dbo.Disziplins", "Config_ID", "dbo.Configs", "ID");
        }
    }
}

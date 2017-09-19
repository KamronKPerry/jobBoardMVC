namespace FPJobBoard.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResumeFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ResumeFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ResumeFileName");
        }
    }
}

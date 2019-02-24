namespace IssueLog_Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addissuetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CriticalLevel = c.Boolean(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "ProjectId" });
            DropTable("dbo.Issues");
        }
    }
}

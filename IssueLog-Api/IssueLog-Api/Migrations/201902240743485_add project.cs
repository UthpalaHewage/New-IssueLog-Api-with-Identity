namespace IssueLog_Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ClientId = c.String(nullable: false, maxLength: 128),
                        ProjectManagerId = c.String(nullable: false, maxLength: 128),
                        ProjectLeaderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ClientId)
                .ForeignKey("dbo.User", t => t.ProjectLeaderId)
                .ForeignKey("dbo.User", t => t.ProjectManagerId)
                .Index(t => t.ClientId)
                .Index(t => t.ProjectManagerId)
                .Index(t => t.ProjectLeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProjectManagerId", "dbo.User");
            DropForeignKey("dbo.Projects", "ProjectLeaderId", "dbo.User");
            DropForeignKey("dbo.Projects", "ClientId", "dbo.User");
            DropIndex("dbo.Projects", new[] { "ProjectLeaderId" });
            DropIndex("dbo.Projects", new[] { "ProjectManagerId" });
            DropIndex("dbo.Projects", new[] { "ClientId" });
            DropTable("dbo.Projects");
        }
    }
}

namespace Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.WorkLogs");
            DropTable("dbo.Users");
            DropTable("dbo.SharedTasks");
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskComments");
            DropTable("dbo.SharedProjects");
            DropTable("dbo.Projects");

            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ownerID = c.Int(nullable: false),
                        title = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SharedProjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TaskComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TaskID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        taskOwnerID = c.Int(nullable: false),
                        parentID = c.Int(nullable: false),
                        title = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SharedTasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        TaskID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WorkLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TaskID = c.Int(nullable: false),
                        LoggedHours = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkLogs");
            DropTable("dbo.Users");
            DropTable("dbo.SharedTasks");
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskComments");
            DropTable("dbo.SharedProjects");
            DropTable("dbo.Projects");
        }
    }
}

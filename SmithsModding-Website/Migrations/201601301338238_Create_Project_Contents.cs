namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Project_Contents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Message = c.String(nullable: false, maxLength: 128),
                        LogoPath = c.String(nullable: false),
                        PageContent = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentationGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        DisplayContents = c.String(nullable: false),
                        LastEditOn = c.DateTime(nullable: false),
                        LastEditor_Id = c.String(nullable: false, maxLength: 128),
                        Project_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LastEditor_Id, cascadeDelete: false)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: false)
                .Index(t => t.LastEditor_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.DocumentationItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        DisplayContents = c.String(nullable: false),
                        LastEditOn = c.DateTime(nullable: false),
                        DocumentationGroup_Id = c.String(nullable: false, maxLength: 128),
                        LastEditor_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocumentationGroups", t => t.DocumentationGroup_Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.LastEditor_Id, cascadeDelete: false)
                .Index(t => t.DocumentationGroup_Id)
                .Index(t => t.LastEditor_Id);
            
            DropTable("dbo.ProjectItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Message = c.String(nullable: false, maxLength: 128),
                        LogoPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.DocumentationGroups", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.DocumentationGroups", "LastEditor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocumentationItems", "LastEditor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DocumentationItems", "DocumentationGroup_Id", "dbo.DocumentationGroups");
            DropIndex("dbo.DocumentationItems", new[] { "LastEditor_Id" });
            DropIndex("dbo.DocumentationItems", new[] { "DocumentationGroup_Id" });
            DropIndex("dbo.DocumentationGroups", new[] { "Project_Id" });
            DropIndex("dbo.DocumentationGroups", new[] { "LastEditor_Id" });
            DropTable("dbo.DocumentationItems");
            DropTable("dbo.DocumentationGroups");
            DropTable("dbo.Projects");
        }
    }
}

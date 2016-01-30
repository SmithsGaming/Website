namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUserSystemtoallowPermissions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRolesPermissions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AddedOn = c.DateTime(nullable: false),
                        AddedBy_Id = c.String(nullable: false, maxLength: 128),
                        Permission_Id = c.String(nullable: false, maxLength: 128),
                        Role_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedBy_Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetPermissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.Permission_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.AspNetPermissions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClaimID = c.String(nullable: false),
                        LastEditedOn = c.DateTime(nullable: false),
                        LastEditor_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LastEditor_Id, cascadeDelete: false)
                .Index(t => t.LastEditor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetRolesPermissions", "Role_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRolesPermissions", "Permission_Id", "dbo.AspNetPermissions");
            DropForeignKey("dbo.AspNetPermissions", "LastEditor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetRolesPermissions", "AddedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetPermissions", new[] { "LastEditor_Id" });
            DropIndex("dbo.AspNetRolesPermissions", new[] { "Role_Id" });
            DropIndex("dbo.AspNetRolesPermissions", new[] { "Permission_Id" });
            DropIndex("dbo.AspNetRolesPermissions", new[] { "AddedBy_Id" });
            DropTable("dbo.AspNetPermissions");
            DropTable("dbo.AspNetRolesPermissions");
        }
    }
}

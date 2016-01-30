namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Projects : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProjectItems");
        }
    }
}

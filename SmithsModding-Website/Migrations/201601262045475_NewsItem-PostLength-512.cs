namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsItemPostLength512 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsItems", "Post", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsItems", "Post", c => c.String(nullable: false, maxLength: 1024));
        }
    }
}

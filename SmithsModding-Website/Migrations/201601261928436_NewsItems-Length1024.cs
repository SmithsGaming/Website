namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsItemsLength1024 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsItems", "Post", c => c.String(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsItems", "Post", c => c.String(nullable: false, maxLength: 256));
        }
    }
}

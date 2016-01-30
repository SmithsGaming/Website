namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Required_From_PageContents_On_Projects : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "PageContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "PageContent", c => c.String(nullable: false));
        }
    }
}

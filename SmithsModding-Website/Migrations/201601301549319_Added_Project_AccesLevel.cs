namespace SmithsModding_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Project_AccesLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "AccesLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "AccesLevel");
        }
    }
}

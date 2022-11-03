namespace LocalPrep.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCookingStyle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CookingStyle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CookingStyle");
        }
    }
}

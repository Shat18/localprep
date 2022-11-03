namespace LocalPrep.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDietsCuisinesWebsite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Website", c => c.String());
            AddColumn("dbo.AspNetUsers", "CuisinesSelected", c => c.String());
            AddColumn("dbo.AspNetUsers", "DietsSelected", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DietsSelected");
            DropColumn("dbo.AspNetUsers", "CuisinesSelected");
            DropColumn("dbo.AspNetUsers", "Website");
        }
    }
}

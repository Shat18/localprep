namespace LocalPrep.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfilePic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePic", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePic");
        }
    }
}

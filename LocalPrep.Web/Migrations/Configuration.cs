namespace LocalPrep.Web.Migrations
{
    using LocalPrep.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocalPrep.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LocalPrep.Web.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Prepper"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Prepper" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "TheMediaCaptain"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "TheMediaCaptain", Email = "mike@themediacaptain.com", Address1 = "294 E. Long St.", Address2 = "Suite 300", City = "Columbus", StateId = 35, Zip = "43215" };
                manager.Create(user, "FrankRizzo1234*");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "AlexClose"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "AlexClose", Email = "ac382305@gmail.com", Address1 = "", City = "Columbus", StateId = 35, Zip = "43215" };
                manager.Create(user, "LocalPrep1234*");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "BretMette"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "BretMette", Email = "bret@themediacaptain.com", Address1 = "", City = "Columbus", StateId = 35, Zip = "43215" };
                manager.Create(user, "BretMette1234*");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}

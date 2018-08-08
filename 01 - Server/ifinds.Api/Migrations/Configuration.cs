namespace ifinds.Api.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private const string _UserRole = "User";
        private const string _AdminRole = "Administrator";
        private const string _DefaultPassword = "123456";

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ifinds.Api.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == _AdminRole))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = _AdminRole };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == _UserRole))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = _UserRole };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "ducnguyen"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "ducnguyen", Email = "ducnguyen@admin.com" };

                manager.Create(user, _DefaultPassword);
                manager.AddToRole(user.Id, _AdminRole);
            }

            if (!context.Users.Any(u => u.UserName == "namlt"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "namlt", Email = "namlt@namlt.com" };

                manager.Create(user, _DefaultPassword);
                manager.AddToRole(user.Id, _AdminRole);
            }
        }
    }
}
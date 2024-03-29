namespace Blog_JCF.Migrations
{
    using Blog_JCF.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "JCFields@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "JCFields@mailinator.com",
                    Email = "JCFields@mailinator.com",
                    FirstName = "Caleb",
                    LastName = "Fields",
                    DisplayName = "JCFields",
                }, "Abc&123!");
          
            if (!context.Users.Any(u => u.Email == "JTwichell@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "JTwichell@mailinator.com",
                    Email = "JTwichell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich",
                }, "Abc&123!");

            var userId = userManager.FindByEmail("JCFields@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("JTwichell@mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");
        }
    }
}

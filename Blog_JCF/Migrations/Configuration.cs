namespace Blog_JCF.Migrations
{
    using Blog_JCF.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog_JCF.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Blog_JCF.Models.ApplicationDbContext context)
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
          
            if (!context.Users.Any(u => u.Email == "JTwitchell@mailinator.com"))
                userManager.Create(new ApplicationUser
                {
                    UserName = "JTwitchell@mailinator.com",
                    Email = "JTwitchell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Twich",
                }, "Abc&123!");

            var userId = userManager.FindByEmail("Jcfields@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("JTwichell@Mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
namespace ASCE.Models
{
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var roleAdmin = new IdentityRole { Name = "admin" };
            var roleUser = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@admin.ru", UserName = "admin@admin.ru" };
            string password = "admin";

            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, "admin");
            }

            base.Seed(context);
        }
    }
}
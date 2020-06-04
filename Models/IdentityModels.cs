using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASCE.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        public ICollection<PersonalAccount> PersonalAccounts { get; set; }
        public ApplicationUser()
        {
            PersonalAccounts = new List<PersonalAccount>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class PersonalAccount
    {
        public int Id { get; set; } //Номер лицевого счета

        public ApplicationUser ApplicationUser { get; set; }

        public string Address { get; set; }

        public DateTime DateOpen { get; set; }

        public ICollection<Counter> Counters { get; set; }
        public PersonalAccount()
        {
            Counters = new List<Counter>();
        }

    }

    public class Counter
    {
        public int Id { get; set; }
     
        public PersonalAccount PersonalAccount { get; set; }
        
        public string SerialNumber { get; set; }
        
        public string Model { get; set; }
        
        public string Manufacturer { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateVerification { get; set; }

        public DateTime DateVerificationNext { get; set; } //TODO: Сделать автоматический расчет в зависимости от прошлой поверки

        public DateTime DateInstall { get; set; }
        
        public int Capacity { get; set; }

        public string SealNumber { get; set; }

        public string InstallPlase { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        DbSet<PersonalAccount> PersonalAccounts { get; set; }
        DbSet<Counter> Counters { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASCE.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(25)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [StringLength(25)]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        
        [Display(Name = "Отчество")]
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
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Номер лицевого счета")]
        public int AccountNumber { get; set; }

        [HiddenInput (DisplayValue = false)]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Адреc")]
        public string Address { get; set; }

        [Required]
        [Display (Name = "Дата открытия")]
        [DataType(DataType.Date)]
        public DateTime DateOpen { get; set; }

        public ICollection<Counter> Counters { get; set; }
        public PersonalAccount()
        {
            Counters = new List<Counter>();
        }

    }

    public class Counter
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
     
        [HiddenInput(DisplayValue = false)]
        public PersonalAccount PersonalAccount { get; set; }

        [Display(Name = "Серийный номер")]
        public string SerialNumber { get; set; }
        
        [Display(Name = "Модель")]
        public string Model { get; set; }
        
        [Display(Name = "Производитель")]
        public string Manufacturer { get; set; }

        [Display(Name = "Дата изготовления")]
        [DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }

        [Display(Name = "Дата поверки")]
        [DataType(DataType.Date)]
        public DateTime DateVerification { get; set; }

        [Display(Name = "Дата следующей поверки")]
        [DataType(DataType.Date)]
        public DateTime DateVerificationNext { get; set; } //TODO: Сделать автоматический расчет в зависимости от прошлой поверки

        [Display(Name = "Дата установки")]
        [DataType(DataType.Date)]
        public DateTime DateInstall { get; set; }
        
        [Display(Name = "Разрядность")]
        public int Capacity { get; set; }

        [Display(Name = "Номер пломбы")]
        public string SealNumber { get; set; }

        [Display(Name = "Место установки")]
        public string InstallPlace { get; set; }
    }

    public class Service
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя услуги")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

    }

    public class Verification
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Counter Counter { get; set; }

        [HiddenInput(DisplayValue = false)]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Протокол о поверки")]
        public byte[] ProtoclImg { get; set; } //Сканкопия протокола о поверки

        [Display(Name = "Свидетельство о поверки")]
        public byte[] СertificateImg { get; set; } //Сканкопия свидетельства о поверки
    }

    public class Worker
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Телефон")]
        public int PhoneNumber { get; set; }
    }

    public class Request
    {
        [Display(Name = "Номер заявки")]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Worker Worker { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Service Service { get; set; }

        [HiddenInput(DisplayValue = false)]
        public PersonalAccount PersonalAccount { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Дата открытия")]
        [DataType(DataType.Date)]
        public DateTime DateOpen { get; set; }
        
        [Display(Name = "Дата закрытия")]
        [DataType(DataType.Date)]
        public DateTime DateClose { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<PersonalAccount> PersonalAccounts { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Verification> Verifications { get; set; }

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
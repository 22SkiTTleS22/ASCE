﻿using System;
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
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Строка должна содержать от 5 до 25 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Строка должна содержать от 5 до 25 символов")]
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

        [Required(ErrorMessage = "Лицевой счет не может быть пустым")]
        [Display(Name = "Номер лицевого счета")]
        public int AccountNumber { get; set; }

        [HiddenInput (DisplayValue = false)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage = "Адрес не может быть пустым")]
        [Display(Name = "Адреc")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Дата открытия лицевого счета")]
        [DataType(DataType.Date)]
        public DateTime DateOpen { get; set; } = DateTime.Now;

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
        public int? PersonalAccountId { get; set; }
        public PersonalAccount PersonalAccount { get; set; }

        [Required(ErrorMessage = "Серийный номер не может быть пустым")]
        [Display(Name = "Серийный номер")]
        public string SerialNumber { get; set; }
        
        [Required(ErrorMessage = "Модель не может быть пустой")]
        [Display(Name = "Модель")]
        public string Model { get; set; }
        
        [Required(ErrorMessage = "Обязательно введите производителя")]
        [Display(Name = "Производитель")]
        public string Manufacturer { get; set; }

        [Display(Name = "Дата изготовления")]
        [DataType(DataType.Date)]
        public DateTime DateCreate { get; set; }

        [Display(Name = "Дата поверки")]
        [DataType(DataType.Date)]
        public DateTime DateVerification { get; set; }

        [Display(Name = "Дата установки")]
        [DataType(DataType.Date)]
        public DateTime DateInstall { get; set; }

        [Display(Name = "Номер пломбы")]
        public string SealNumber { get; set; }

        [Display(Name = "Место установки")]
        public string InstallPlace { get; set; }

        public ICollection<CounterHistory> CounterHistories { get; set; }
        public Counter()
        {
            CounterHistories = new List<CounterHistory>();
        }
    }

    public class CounterHistory
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? CounterId { get; set; }
        public Counter Counter { get; set; }

        [Required(ErrorMessage = "Данные не могут быть пустыми")]
        [Display(Name = "Показания")]
        public float Value { get; set; }

        [Required(ErrorMessage = "Дата должна быть не пустой")]
        [Display(Name = "Дата сдачи")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
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
        public int? CounterId { get; set; }
        public Counter Counter { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ApplicationUserId { get; set; }
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
        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? PersonalAccountId { get; set; }
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
        public DbSet<CounterHistory> CounterHistories { get; set; }
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
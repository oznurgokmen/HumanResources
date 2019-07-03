namespace InsanKaynaklari.Migrations
{
    using InsanKaynaklari.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InsanKaynaklari.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InsanKaynaklari.Models.ApplicationDbContext context)
        {
            if (!context.Departmanlar.Any())
            {
                context.Departmanlar.Add(new Models.Departman { DepartmanAd = "Bilgi Ýþlem" });
                context.Departmanlar.Add(new Models.Departman { DepartmanAd = "Muhasebe" });
                context.Departmanlar.Add(new Models.Departman { DepartmanAd = "Kalite Kontrol" });
                context.Departmanlar.Add(new Models.Departman { DepartmanAd = "Satýn Alma" });
            }

            if (!context.Roles.Any(r => r.Name == "yonetici"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "yonetici" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@example.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };

                manager.Create(user, "Ankara1.");

                manager.AddToRole(user.Id, "yonetici");
            }

            if (false)
            {
                List<int> departmanIdler = context
                    .Departmanlar
                    .Select(x => x.Id)
                    .ToList();

                Random rnd = new Random();
                var personelSayisi = context.Personeller.Count();
                var adet = 60 - personelSayisi;

                for (int i = 0; i < adet; i++)
                {
                    context.Personeller.Add(new Personel
                    {
                        PersonelAd = "Personel Ad " + i,
                        PersonelSoyad = "Personel Soyad " + i,
                        PersonelEmail = "12@as.com",
                        TelefonNo = "1234567890",
                        IseBaslamaTarihi = DateTime.Now,
                        Maas = rnd.Next(1000, 10000),
                        DepartmanId = departmanIdler[rnd.Next(0, departmanIdler.Count)]
                    });
                }
            }
        }
    }
}

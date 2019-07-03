using PagedList;
using InsanKaynaklari.Models;
using InsanKaynaklari.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace InsanKaynaklari.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? cid, int? page,string ara, string sirala = "az")
        {
            HomeIndexVM vm = new HomeIndexVM();

            IQueryable<Personel> personellerSorgu = db.Personeller;

            if (cid != null && cid != -1)
            {
                Departman seciliDepartman = db.Departmanlar.Find(cid);

                if (seciliDepartman == null)
                {
                    return HttpNotFound();
                }

                vm.SeciliDepartmanId = cid.Value;
                vm.SeciliDepartmanAd = seciliDepartman.DepartmanAd;


                personellerSorgu = personellerSorgu
                    .Where(x => x.DepartmanId == cid);
            }

            if (!string.IsNullOrEmpty(ara))
            {
                personellerSorgu = personellerSorgu.Where(x => x.PersonelAd.Contains(ara));
                ViewBag.ara = ara;
            }
            else
            {
                ViewBag.ara = "";
            }

            vm.Departmanlar = db.Departmanlar.ToList();

            switch (sirala)
            {
                case "az":
                    personellerSorgu = personellerSorgu.OrderBy(x => x.PersonelAd);
                    break;
                default:
                    personellerSorgu = personellerSorgu.OrderBy(x => x.PersonelAd);
                    sirala = "az";
                    break;
            }

            ViewBag.siralaSecenek = sirala;

            int pageSize = 100;
            int pageNumber = page ?? 1;
            vm.Personeller = personellerSorgu.ToPagedList(pageNumber, pageSize);

            return View(vm);
        }

        public ActionResult Iletisim()
        {
            return View();
        }
    }
}
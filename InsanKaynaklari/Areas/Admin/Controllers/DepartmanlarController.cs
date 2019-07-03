using InsanKaynaklari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsanKaynaklari.Areas.Admin.Controllers
{
    public class DepartmanlarController : AdminBaseController
    {
        public ActionResult Index()
        {
            return View(db.Departmanlar.ToList());
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Ekle(string departmanAd)
        {
            db.Departmanlar.Add(new Departman { DepartmanAd = departmanAd });
            db.SaveChanges();

            TempData["mesaj"] = "\"" + departmanAd + "\" adlı departman başarıyla eklendi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Duzenle(int id, string departmanAd)
        {
            Departman departman = db.Departmanlar.Find(id);
            departman.DepartmanAd = departmanAd;

            db.SaveChanges();

            TempData["mesaj"] = "\"" + departman.DepartmanAd + "\" adlı departman başarıyla düzenlendi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sil(int id)
        {
            Departman departman = db.Departmanlar.Find(id);

            if (departman.Personeller.Count > 0)
            {
                TempData["hata"] = "\"" + departman.DepartmanAd + "\" adlı departman altında personeller bulunduğundan silinemiyor.";

                return RedirectToAction("Index");
            }

            db.Departmanlar.Remove(departman);

            db.SaveChanges();

            TempData["mesaj"] = "\"" + departman.DepartmanAd + "\" adlı departman başarıyla silindi.";

            return RedirectToAction("Index");
        }


    }
}
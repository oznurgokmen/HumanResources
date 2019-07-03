using InsanKaynaklari.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsanKaynaklari.Areas.Admin.Controllers
{
    public class PersonellerController : AdminBaseController
    {
        // GET: Admin/Personeller
        public ActionResult Index()
        {
            return View(db.Personeller.ToList());
        }

        public ActionResult Ekle()
        {
            ViewBag.DepartmanId = new SelectList(db.Departmanlar.ToList(), "Id", "DepartmanAd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Ekle(Personel personel, HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                if (dosya != null && dosya.ContentLength > 0 && dosya.ContentLength < 2 * 1024 * 1024)
                {
                    var ext = Path.GetExtension(dosya.FileName);

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (dosya.ContentType.StartsWith("image/") && allowedExtensions.Contains(ext))
                    {
                        string yeniDosyaAd = Guid.NewGuid().ToString() + ext;

                        string kaydetmeYolu = Server.MapPath("~/img/" + yeniDosyaAd);

                        dosya.SaveAs(kaydetmeYolu);

                        personel.FotoAd = yeniDosyaAd;
                    }
                }

                db.Personeller.Add(personel);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DepartmanId = new SelectList(db.Departmanlar.ToList(), "Id", "DepartmanAd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sil(int id)
        {
            Personel personel = db.Personeller.Find(id);

            if (personel != null)
            {
                db.Personeller.Remove(personel);
                db.SaveChanges();

                if (!string.IsNullOrEmpty(personel.FotoAd))
                {
                    var resimYolu = Server.MapPath("~/img/" + personel.FotoAd);

                    if (System.IO.File.Exists(resimYolu))
                    {
                        System.IO.File.Delete(resimYolu);
                    }
                }
            }

            return RedirectToAction("Index", new { sonuc = "silindi" });
        }

        public ActionResult Duzenle(int id)
        {
            Personel personel = db.Personeller.Find(id);

            if (personel == null)
            {
                return HttpNotFound();
            }

            ViewBag.DepartmanId = new SelectList(db.Departmanlar.ToList(), "Id", "DepartmanAd");

            return View(personel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Duzenle(Personel personel, HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                Personel dbdekiEskiPersonel = db.Personeller.Find(personel.Id);

                if (dosya != null && dosya.ContentLength > 0 && dosya.ContentLength < 2 * 1024 * 1024)
                {
                    var ext = Path.GetExtension(dosya.FileName);

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (!dosya.ContentType.StartsWith("image/") || !allowedExtensions.Contains(ext))
                    {
                        ModelState.AddModelError("FotoAd", "Geçersiz bir resim dosyası yüklediniz.");
                    }

                    if (ModelState.IsValid)
                    {
                        if (!string.IsNullOrEmpty(dbdekiEskiPersonel.FotoAd))
                        {
                            var dbdekiEskiPersonelResimYol = Server.MapPath("~/img/" + dbdekiEskiPersonel.FotoAd);

                            if (System.IO.File.Exists(dbdekiEskiPersonelResimYol))
                            {
                                System.IO.File.Delete(dbdekiEskiPersonelResimYol);
                            }
                        }

                        string yeniDosyaAd = Guid.NewGuid().ToString() + ext;

                        string kaydetmeYolu = Server.MapPath("~/img/" + yeniDosyaAd);

                        dosya.SaveAs(kaydetmeYolu);

                        dbdekiEskiPersonel.FotoAd = yeniDosyaAd;
                    }
                }

                if (ModelState.IsValid)
                {
                    dbdekiEskiPersonel.DepartmanId = personel.DepartmanId;
                    dbdekiEskiPersonel.PersonelAd = personel.PersonelAd;
                    dbdekiEskiPersonel.PersonelSoyad = personel.PersonelSoyad;
                    dbdekiEskiPersonel.PersonelEmail = personel.PersonelEmail;
                    dbdekiEskiPersonel.TelefonNo = personel.TelefonNo;
                    dbdekiEskiPersonel.IseBaslamaTarihi = personel.IseBaslamaTarihi;
                    dbdekiEskiPersonel.Maas = personel.Maas;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.DepartmanId = new SelectList(db.Departmanlar.ToList(), "Id", "DepartmanAd");

            return View(personel);
        }
    }
}
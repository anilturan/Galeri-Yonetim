using GaleriUygulamasi.Extension;
using GaleriUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaleriUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        GaleriYonetimContext context;
        public HomeController()
        {
            context = new GaleriYonetimContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult fileupload()
        {
            //Upload View'ından gelen datalarımın tipi HttpPostedFileBase, gelen datayı file değişkeninde saklıyoruz.
            HttpPostedFileBase file = HttpContext.Request.Files[0];

            //Disposable
            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                byte[] value = reader.ReadBytes((int)file.ContentLength);

                if (Session["value"] == null)
                {
                    Session["value"] = value;
                }
                else
                {
                    Session["value"] = UtilityManager.ByteBirlestir((byte[])Session["value"], value);
                }
                if (10000 > file.ContentLength)
                {
                    context.Dosya.Add(new Dosya
                    {
                        Deger = ((byte[])Session["value"]),
                        DosyaAdi = file.FileName,
                        DosyaBoyutu = (((byte[])Session["value"]).Length),
                        DosyaTipi = file.ContentType,
                        Ikon = UtilityManager.setIcon(file.ContentType),
                        BoyutKisaltma = UtilityManager.BytesToString(((byte[])Session["value"]).Length),
                        KayitTarihi = DateTime.Now
                    });
                    context.SaveChanges();
                    Session.Remove("value");
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Galeri()
        {
            return View();
        }

        //[HttpPost]
        public FileContentResult fileView(int id)
        {
            var List = context.Dosya.Where(x => x.ID == id).SingleOrDefault();
            return new FileContentResult(List.Deger, List.DosyaTipi);
            //return new FileContentResult(List.Deger, List.DosyaTipi, List.DosyaAdi);
        }

        [HttpGet]
        public ActionResult GetFileDetailByID(int ID)
        {
            var file = (from p in context.Dosya
                        where p.ID == ID
                        select new
                        {
                            p.BoyutKisaltma,
                            p.DosyaAdi,
                            p.DosyaTipi,
                            p.Baslik,
                            p.Aciklama,
                            UrlYolu = "/Home/fileView/" + p.ID 
                        }).FirstOrDefault();
            return Json(file, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateFile(UpdateFileProperties items)
        {
            try
            {
                var entity = (from p in context.Dosya where p.ID == items.ID select p).SingleOrDefault();
                entity.Aciklama = items.Aciklama;
                entity.Baslik = items.Baslik;
                context.SaveChanges();

                return Json("E", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("H", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
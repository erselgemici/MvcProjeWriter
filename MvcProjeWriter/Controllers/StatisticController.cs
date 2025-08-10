using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        Context c = new Context();
        public ActionResult Index()
        {
            // 1. Toplam kategori sayısı
            ViewBag.KategoriSayisi = c.Categories.Count();

            // 2. "Yazılım" kategorisine ait başlık sayısı
            var yazilimKategoriId = c.Categories
                .Where(x => x.CategoryName.ToLower() == "Tiyatro")
                .Select(x => x.CategoryID)
                .FirstOrDefault();
            ViewBag.YazilimBaslikSayisi = c.Headings
                .Count(x => x.CategoryID == yazilimKategoriId);

            // 3. Adında 'a' harfi geçen yazar sayısı
            ViewBag.AIcerenYazarSayisi = c.Writers
                .Count(x => x.WriterName.ToLower().Contains("a"));

            // 4. En fazla başlığa sahip kategori adı
            var populerKategoriId = c.Headings
                .GroupBy(x => x.CategoryID)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
            ViewBag.EnPopulerKategoriAdi = c.Categories
                .Where(x => x.CategoryID == populerKategoriId)
                .Select(x => x.CategoryName)
                .FirstOrDefault();

            // 5. Aktif ve pasif kategori sayısı farkı
            var aktif = c.Categories.Count(x => x.CategoryStatus == true);
            var pasif = c.Categories.Count(x => x.CategoryStatus == false);
            ViewBag.KategoriDurumFarki = aktif - pasif;

            return View();
        }
    }
}

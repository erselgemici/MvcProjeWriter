using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class TalentController : Controller
    {
        // GET: Talent
        TalentManager tm = new TalentManager(new EfTalentDal());
        public ActionResult Index()
        {
            var card = tm.GetList().FirstOrDefault();

            if (card == null)
            {
                // Eğer hiç kayıt yoksa edit sayfasına yönlendir
                return RedirectToAction("Edit");
            }

            return View(card); // View: Views/Talent/Index.cshtml
        }
        [HttpGet]
        public ActionResult Edit()
        {
            var card = tm.GetList().FirstOrDefault();

            if (card == null)
            {
                // Yeni kayıt eklemek için boş model
                return View(new Talent());
            }

            // Mevcut kaydı düzenlemek için
            return View(card); // View: Views/Talent/Edit.cshtml
        }

        // POST: /Talent/Edit
        [HttpPost]
        public ActionResult Edit(Talent model)
        {
            if (model.TalentID == 0)
            {
                tm.TalentAdd(model);     // Yeni kayıt
            }
            else
            {
                tm.TalentUpdate(model);  // Mevcut kaydı güncelle
            }

            return RedirectToAction("Index");
        }
    }
}

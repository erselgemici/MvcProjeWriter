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
    [Authorize(Roles = "E")]
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        AdminManager am = new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            var admimvalues = am.GetList();
            return View(admimvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            var roles = am.GetList()
                  .Select(x => x.AdminRole)
                  .Distinct()
                  .ToList();

            ViewBag.RoleList = roles.Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
          
            p.AdminStatus = true;
            am.AdminAddBL(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminvalues = am.GetByID(id);
            return View(adminvalues);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            
            am.AdminUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult ChangeStatus(int id)
        {
            var admin = am.GetByID(id);
            admin.AdminStatus = !admin.AdminStatus; // true ise false yap, false ise true yap
            am.AdminUpdate(admin);
            return RedirectToAction("Index");
        }

    }
}

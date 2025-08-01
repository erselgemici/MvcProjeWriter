using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using MvcProjeKampi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Context c = new Context();  

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            //// Kullanıcının girdiği şifreyi hashle
            //string hashedPassword = Hash(p.AdminPassword);
            //// Giriş yapılan şifreyi hash'e çeviriyoruz
            //string hashedPassword2 = PasswordHashHelpers.ComputeSha256Hash(p.AdminPassword);

            var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;
                return RedirectToAction("Index", "Admincategory");
            }
            else
            {
                ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

        }
        //public string Hash(string text)
        //{
        //    using (var sha = System.Security.Cryptography.SHA256.Create())
        //    {
        //        return string.Concat(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text))
        //                             .Select(x => x.ToString("x2")));
        //    }
        //}
        //public ActionResult HashAllAdminPasswords()
        //{
        //    Context c = new Context();
        //    var admins = c.Admins.ToList();

        //    foreach (var admin in admins)
        //    {
        //        // Şifre daha önce hash'lenmediyse hashle
        //        if (admin.AdminPassword.Length < 64) // SHA256 64 karakterlidir
        //        {
        //            admin.AdminPassword = PasswordHashHelpers.ComputeSha256Hash(admin.AdminPassword);
        //        }
        //    }

        //    c.SaveChanges();

        //    return Content("Tüm admin şifreleri SHA256 ile hashlenmiştir.");
        //}

    }
}

using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKampi.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous] //Proje bazında oluşturulan kurallardan muaf tutar.
    public class LoginController : Controller
    {
        WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());
        //public class ReCaptchaResponse
        //{
        //    public bool success { get; set; }
        //    public List<string> error_codes { get; set; }
        //}
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
                Session["AdminRole"] = adminuserinfo.AdminRole;
                return RedirectToAction("Index", "Admincategory");
            }
            else
            {
                ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {

            //var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            var writeruserinfo = wm.GetWriter(p.WriterMail, p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
                return RedirectToAction("WriterLogin");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings","Default");
        }



        //[HttpPost]
        //public ActionResult WriterLogin(Writer p)
        //{
        //    // 1. Recaptcha kontrolü
        //    var recaptchaResponse = Request["g-recaptcha-response"];
        //    var secret = "6LeoXZgrAAAAAF7kGmiRGkiOaYzWjj2obKLW6l0z";
        //    using (var client = new System.Net.WebClient())
        //    {
        //        var googleReply = client.DownloadString(
        //            $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={recaptchaResponse}");
        //        // Burada googleReply'ı debugda veya ekranda gör!
        //        //System.Diagnostics.Debug.WriteLine("reCAPTCHA Yanıtı: " + googleReply);
        //        //return Content("reCAPTCHA Yanıtı: " + googleReply);
        //        var captchaResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaResponse>(googleReply);
        //        if (!captchaResult.success)
        //        {
        //            ViewBag.LoginError = "Lütfen 'Ben robot değilim' kutusunu işaretleyin!";
        //            return View("WriterLogin");
        //        }
        //    }

        //    // 2. Kullanıcı adı ve şifre kontrolü
        //    var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
        //    if (writeruserinfo != null)
        //    {
        //        FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
        //        Session["WriterMail"] = writeruserinfo.WriterMail;
        //        return RedirectToAction("MyContent", "WriterPanelContent");
        //    }
        //    else
        //    {
        //        ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
        //        return View("WriterLogin");
        //    }
        //}


        //}
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

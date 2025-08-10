using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        // GET: Message
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        public ActionResult Inbox(string p)
        {
            //string currentUser = (string)Session["WriterMail"];
            //var messages = mm.GetListInbox(currentUser);
            //ViewBag.UnreadCount = mm.GetUnreadMessageCount(currentUser);
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult Sendbox(string p)
        {
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);

            if (!values.IsRead)
            {
                values.IsRead = true;
                mm.MessageUpdate(values);
            }

            return View(values);
        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult result = messagevalidator.Validate(p);
            if (result.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAddBL(p);
                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}

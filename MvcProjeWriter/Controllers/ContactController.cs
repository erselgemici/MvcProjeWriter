using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }
        public PartialViewResult ContactPartial()
        {
            Context c = new Context();
            var inboxCount = c.Messages.Count(x => x.ReceiverMail == "admin@gmail.com");
            var draftCount = c.Messages.Count(x => x.IsDraft == true);
            var sentCount = c.Messages.Count(x => x.SenderMail == "admin@gmail.com" && x.IsDraft == false);
            var spamCount = c.Messages.Count(x => x.IsSpam == true);
            var trashCount = c.Messages.Count(x => x.IsDeleted == true);
            var contactCount = c.Contacts.Count();

            ViewBag.InboxCount = inboxCount;
            ViewBag.DraftCount = draftCount;
            ViewBag.SentCount = sentCount;
            ViewBag.SpamCount = spamCount;
            ViewBag.TrashCount = trashCount;
            ViewBag.ContactCount = contactCount;
            return PartialView();
        }
    }
}

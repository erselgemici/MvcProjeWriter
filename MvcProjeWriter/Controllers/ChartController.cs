using DataAccessLayer.Concrete;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        Context c = new Context();
        public ActionResult CategoryChart()
        {
            return Json(CategoryContentCount(), JsonRequestBehavior.AllowGet);
        }
        public List<CategoryClass> CategoryContentCount()
        {           
                var values = c.Contents
                    .Where(x => x.ContentStatus == true)
                    .GroupBy(x => x.Heading.Category.CategoryName)
                    .Select(g => new CategoryClass
                    {
                        CategoryName = g.Key,
                        CategoryCount = g.Count()
                    }).ToList();
                return values;          
        }
        public ActionResult HeadingCategory()
        {
            return View();
        }

        public ActionResult HeadingCategoryChart()
        {
            return Json(HeadingCategoryList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> HeadingCategoryList()
        {
            using (Context c = new Context())
            {
                var result = c.Headings
                    .Where(x => x.HeadingStatus == true)
                    .GroupBy(x => x.Category.CategoryName)
                    .Select(g => new CategoryClass
                    {
                        CategoryName = g.Key,
                        CategoryCount = g.Count()
                    }).ToList();

                return result;
            }
        }
        public ActionResult WriterContent()
        {
            return View();
        }

        public ActionResult WriterContentChart()
        {
            return Json(WriterContentList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> WriterContentList()
        {
            using (Context c = new Context())
            {
                var result = c.Contents
                    .Where(x => x.ContentStatus == true)
                    .GroupBy(x => x.Writer.WriterName)
                    .Select(g => new CategoryClass
                    {
                        CategoryName = g.Key,
                        CategoryCount = g.Count()
                    }).ToList();

                return result;
            }
        }

    }
}

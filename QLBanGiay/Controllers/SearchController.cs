using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanGiay.Models;
using PagedList.Mvc;
using PagedList;

namespace QLBanGiay.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        [HttpGet]
        public ActionResult ResultSearch(string sTuKhoa, int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var lstSP = db.GIAYs.Where(n => n.TenGiay.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSP.OrderBy(n => n.TenGiay).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult GetValueKey(string sTuKhoa)
        {

            return RedirectToAction("ResultSearch", new { @sTuKhoa = sTuKhoa });
        }
    }
}
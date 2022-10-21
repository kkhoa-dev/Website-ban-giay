using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanGiay.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: Admin/Home
        public int countTH()
        {
            return db.THUONGHIEUx.Count();
        }
        public int CountSP()
        {
            return db.GIAYs.Count();
        }
        public int countDH()
        {
            return db.DONDATHANGs.Count();
        }
        public int CountKH()
        {
            return db.KHACHHANGs.Count();
        }
        public double countPrice()
        { 
            return (double)db.CHITIETDATHANGs.Sum(x => x.DonGia);
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var sTenDN = f["Username"];
            var sMatKhau = f["Password"];
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult NavtopPartial()
        {
            return PartialView();
        }
        public ActionResult QuanLiPartial()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            ViewBag.brand = countTH();
            ViewBag.total = countPrice();
            ViewBag.countDH = countDH();
            ViewBag.CountKH = CountKH();
            ViewBag.CountSP = CountSP();
            return View();
        }
    }
}
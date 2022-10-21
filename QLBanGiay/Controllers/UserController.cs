using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanGiay.Controllers
{
    public class UserController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            //Gan gia tri nguoi dung nhap du lieu
            var sHoTen = collection["HoTen"];
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err1"] = "Họ tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err5"] = "Email không được rỗng";
            }
            else if (String.IsNullOrEmpty(sDienThoai))
            {
                ViewData["err6"] = "Số điện thoại không được rỗng";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";
            }
            else
            {
                //Gan gia tỉ cho doi tuong dươc tao moi
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTenDN;
                kh.MatKhau = sMatKhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    if (Session["visitedpage"] != null)
                    {                       
                        return RedirectToAction(Session["visitedpage"].ToString(), "GioHang");
                    }
                    else { 
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChangePass(int? id)
        {
            KHACHHANG kh = db.KHACHHANGs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }
        [HttpPost]
        public ActionResult ChangePass(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(kh);
        }
        [HttpGet]
        public ActionResult InfoUser(int? id)
        {
            KHACHHANG kh = db.KHACHHANGs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }       
        public JsonResult VerifyEmail(string email)
        {          
            return Json(!db.KHACHHANGs.Any(x=>x.Email == email), JsonRequestBehavior.AllowGet);            
        }
        [HttpPost]
        public ActionResult InfoUser(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(kh).State = EntityState.Modified;               
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(kh);
        }
    }
}
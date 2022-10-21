using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanGiay.Models;

namespace QLBanGiay.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG khachhang)
        {
        
                if (ModelState.IsValid)
                {
                    db.KHACHHANGs.Add(khachhang);
                    db.SaveChanges();
                }
                return RedirectToAction("Index"); 
            
        }
        public ActionResult Details(int? id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            KHACHHANG kh = db.KHACHHANGs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }        

            return View(kh);
        }
        [HttpPost]
        public ActionResult Edit(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            return View(kh);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }            
            db.KHACHHANGs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
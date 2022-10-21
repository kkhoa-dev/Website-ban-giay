using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanGiay.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }
       
        public ActionResult Details(int? id)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            DONDATHANG dh = db.DONDATHANGs.Find(id);
            if (dh == null)
            {
                return HttpNotFound();
            }

            return View(dh);
        }
        [HttpPost]
        public ActionResult Edit(DONDATHANG dh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dh);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DONDATHANGs.Remove(dh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
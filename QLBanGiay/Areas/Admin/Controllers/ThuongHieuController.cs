using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanGiay.Areas.Admin.Controllers
{
    public class ThuongHieuController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: Admin/ThuongHieu
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;
            return View(db.THUONGHIEUx.ToList().OrderBy(n => n.MaTH).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(THUONGHIEU thuonghieu)
        {

            if (ModelState.IsValid)
            {
                db.THUONGHIEUx.Add(thuonghieu);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Details(int? id)
        {
            var th = db.THUONGHIEUx.SingleOrDefault(n => n.MaTH == id);
            if (th == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(th);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            THUONGHIEU th = db.THUONGHIEUx.Find(id);
            if (th == null)
            {
                return HttpNotFound();
            }

            return View(th);
        }
        [HttpPost]
        public ActionResult Edit(THUONGHIEU th)
        {
            if (ModelState.IsValid)
            {
                db.Entry(th).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(th);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var kh = db.THUONGHIEUx.SingleOrDefault(n => n.MaTH == id);
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
            var kh = db.THUONGHIEUx.SingleOrDefault(n => n.MaTH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.THUONGHIEUx.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
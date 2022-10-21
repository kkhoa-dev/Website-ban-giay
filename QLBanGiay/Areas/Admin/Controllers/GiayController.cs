using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanGiay.Areas.Admin.Controllers
{
    public class GiayController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: Admin/Giay
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<GIAY>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = db.GIAYs.Where(n => n.TenGiay.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = db.GIAYs.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            lstProduct = lstProduct.OrderByDescending(n => n.MaGiay).ToList();
            return View(lstProduct.ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Create()
        {
            ViewBag.MaTH = new SelectList(db.THUONGHIEUx.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu");
            ViewBag.MaLoaiGiay = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoaiGiay), "MaLoaiGiay", "TenLoaiGiay");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GIAY giay, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            //Dua du lieu vao DropDown
            ViewBag.MaTH = new SelectList(db.THUONGHIEUx.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu");
            ViewBag.MaLoaiGiay = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoaiGiay), "MaLoaiGiay", "TenLoaiGiay");
            if (fFileUpload == null)
            {
                //Noi dung thong bao yeu cau chon anh giay
                ViewBag.ThongBao = "Hãy chọn ảnh giày.";
                //Luu thong tin khi load
                ViewBag.TenGiay = f["sTenGiay"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.Size = int.Parse(f["iSize"]);
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaTH = new SelectList(db.THUONGHIEUx.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu", int.Parse(f["MaTH"]));
                ViewBag.MaLoaiGiay = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoaiGiay), "MaLoaiGiay", "TenLoaiGiay", int.Parse(f["MaLoaiGiay"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Lay ten file
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    //Lay duong dan luu file
                    var path = Path.Combine(Server.MapPath("/Images"), sFileName);
                    //Kiem tra anh giay da ton tai chua de luu len thu muc
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    //Luu giay vao CSDL
                    giay.TenGiay = f["sTenGiay"];
                    giay.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    giay.AnhGiay = sFileName;
                    giay.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    giay.Size = int.Parse(f["iSize"]);
                    giay.SoLuongBan = int.Parse(f["iSoLuong"]);
                    giay.GiaBan = decimal.Parse(f["mGiaBan"]);
                    giay.MaTH = int.Parse(f["MaTH"]);
                    giay.MaLoaiGiay = int.Parse(f["MaLoaiGiay"]);
                    db.GIAYs.Add(giay);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaGiay == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sách này đang có trong bảng chi tiết đặt hàng <br>" +
                    "Nếu muốn xoá thì phải xoá hết mã giày này trong bảng chi tiết đặt hàng";
                return View(giay);
            }
            db.GIAYs.Remove(giay);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            GIAY giay = db.GIAYs.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTH = new SelectList(db.THUONGHIEUx.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu", giay.MaTH);
            ViewBag.MaLoaiGiay = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.MaLoaiGiay), "MaLoaiGiay", "TenLoaiGiay", giay.MaLoaiGiay);

            return View(giay);
        }
        [HttpPost]
        public ActionResult Edit(GIAY giay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTH = new SelectList(db.THUONGHIEUx, "MaTH", "TenThuongHieu", giay.MaTH);
            ViewBag.MaLoaiGiay = new SelectList(db.LOAIGIAYs, "MaLoaiGiay", "TenLoaiGiay", giay.MaLoaiGiay);
            return View(giay);
        } 
    }
}
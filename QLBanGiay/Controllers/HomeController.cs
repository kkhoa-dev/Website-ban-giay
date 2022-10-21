using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace QLBanGiay.Controllers
{
    public class HomeController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        
        public ActionResult ChiTietGiay(int id)
        {
            var giay = from a in db.GIAYs where a.MaGiay == id select a;
            return View(giay.Single());
        }
       
        public ActionResult SanPhamMoiPartial()
        {
            var giaymoi = laySanPhamMoi(8);
            return PartialView(giaymoi);
        }
        public List<GIAY> laySanPhamMoi(int count)
        {
            return db.GIAYs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult SanPhamBanChayPartial()
        {
            var giaybanchay =  (from p in db.GIAYs
           let totalQuantity = (from op in db.CHITIETDATHANGs
                         join o in db.DONDATHANGs on op.MaDonHang equals o.MaDonHang
                         where op.MaGiay == p.MaGiay
                         select op.SoLuong).Sum()
            where totalQuantity > 0
            orderby totalQuantity descending
            select p).Take(8);
            return PartialView(giaybanchay);
        }
        public ActionResult LoaiGiayPartial()
        {
            var loaigiay = from a in db.LOAIGIAYs select a;
            return PartialView(loaigiay);
        }
        public ActionResult NavbarPartial()
        {
            var th = from a in db.THUONGHIEUx select a;
            return PartialView(th);
        }
        public ActionResult ThuongHieuPartial()
        {
            var thuonghieu = from a in db.THUONGHIEUx select a;
            return PartialView(thuonghieu);
        }
        public ActionResult GiayTheoThuongHieu(int iMaTH, int? page)
        {
            ViewBag.MaTH = iMaTH;
            //Tạo biến quy đinh số sản phẩm trên mỗi trang
            int pageSize = 6;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);
            var theothuonghieu = db.GIAYs.Where(x => x.MaTH == iMaTH).ToList();
            return View(theothuonghieu.ToPagedList(iPageNum, pageSize));
        }
        public ActionResult LoginLogoutPartial()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GiayTheoLoaiGiay(int iMaLoaiGiay, int? page)
        {
            ViewBag.MaLoaiGiay = iMaLoaiGiay;
            int pageSize = 6;
            int iPageNum = (page ?? 1);
            var theoloaigiay = db.GIAYs.Where(x => x.MaLoaiGiay == iMaLoaiGiay).ToList();
            return View(theoloaigiay.ToPagedList(iPageNum, pageSize));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
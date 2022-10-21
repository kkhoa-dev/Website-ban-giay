using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanGiay.Models
{
    public class GioHang
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        public int iMaGiay { get; set; }
        public string sTenGiay { get; set; }
        public int sSizeGiay { get; set; }
        public string sAnhGiay { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang(int mg)
        {
            iMaGiay = mg;
            GIAY g = db.GIAYs.Single(n => n.MaGiay == iMaGiay);
            sTenGiay = g.TenGiay;
            sSizeGiay = int.Parse(g.Size.ToString());
            sAnhGiay = g.AnhGiay;
            dDonGia = double.Parse(g.GiaBan.ToString());
            iSoLuong = 1;

        }
    }
}
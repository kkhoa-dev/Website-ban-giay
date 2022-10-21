using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanGiay.Models;
using PayPal.Api;

namespace QLBanGiay.Controllers
{
    public class GioHangController : Controller
    {
        QLBanGiayEntities1 db = new QLBanGiayEntities1();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Cap nhat gio hang
        public ActionResult CapNhatGioHang(int iMaGiay, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaGiay);
            //Neu ton tai thi cho sua so luong
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap chua
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                Session["visitedpage"] = "GioHang";
                return RedirectToAction("DangNhap", "User");

            }
            //Kiem tra co hang trong gio hang chua
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lay hang tu Session
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            //them don hang
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> lstGioHang = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            ddh.NgayGiao = DateTime.Parse(NgayGiao);
            ddh.TinhTrangGiaoHang = 1;
            ddh.DaThanhToan = false;
            db.DONDATHANGs.Add(ddh);
            db.SaveChanges();
            //them chi tiet don hang
            foreach (var item in lstGioHang)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaGiay = item.iMaGiay;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                db.CHITIETDATHANGs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
        //Xoa gio hang
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Xoa san pham khoi gio hang
        public ActionResult XoaSPKhoiGioHang(int iMaGiay)
        {
            List<GioHang> lstGioHang = LayGioHang();
            //Kiem tra xem sach co trong Session["GioHang"]
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaGiay);
            //Xoa sp khoi gio hang
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMaGiay == iMaGiay);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("GioHang");
        }
        //Them san pham vao gio hang
        public ActionResult ThemGioHang(int mg, string url)
        {
            //lay gio hang hien tai
            List<GioHang> lstGioHang = LayGioHang();
            //Kiem tra chu co san pham thi them vao con co roi thi tang len
            GioHang sp = lstGioHang.Find(n => n.iMaGiay == mg);
            if (sp == null)
            {
                sp = new GioHang(mg);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        //Tinh tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        //Action tra ve view GioHang
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("GioHangNone");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangNone()
        {
            return View();
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult Index()
        {
            return View();
        }

        // Work with Paypal Payment
        private Payment payment;

        // Create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirecUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };

            List<GioHang> listCarts = LayGioHang();
            foreach (var cart in listCarts)
            {
                listItems.items.Add(new Item() 
                {
                    name = cart.sTenGiay,
                    currency = "VND",
                    price = cart.dDonGia.ToString(),
                    quantity = cart.iSoLuong.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Do the configuration RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirecUrl,
                return_url = redirecUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = listCarts.Sum(n => n.dThanhTien).ToString()
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "VND",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),                
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction() 
            {
                description = "Blue Pudding transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }

        // Create Execute Payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExcution = new PaymentExecution() 
            { 
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExcution);
        }

        // Create PaymentWithPaypal method
        public ActionResult PaymentWithPaypal()
        {
            // Gettings context from the paypal bases on clientId and clientServer for payment
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Create a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/GioHang/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //Get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var excutePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (excutePayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");
            }

            return View("Success");
            //Remove shopping cart session
          
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;

namespace ShopVongTay.Controllers
{
    public class GioHangController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        public List<SanPhamGH> LayGioHang()
        {
            Session["NgayGiaoHang"]=DateTime.Parse(DateTime.Now.ToString());
            Session["NgayNhanHang"] = DateTime.Parse(DateTime.Now.AddDays(+5).ToString("dd-MM-yyyy"));
            List<SanPhamGH> lstSP = Session["GioHang"] as List<SanPhamGH>;
            if (lstSP == null)
            {
                lstSP = new List<SanPhamGH>();
                Session["GioHang"] = lstSP;
            }
            return lstSP;
        }
        public ActionResult GioHang()
        {
            if (Session["username"] == null && Session["Email"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
                //List<KHACHHANG> lstKH
                List<SanPhamGH> listSP = LayGioHang();
                int TongSL = 0;
                double TongTien = 0;

                foreach (var item in listSP)
                {
                    TongSL += item.SoLuongMua;
                    TongTien += item.TongTien;
                    var tongtien1 = String.Format("{0:N0}", TongTien);
                    ViewBag.tongtien = tongtien1;
                }
                if (Session["MaGiam"] != null)
                {
                    int vat = (int)Session["MaGiam"];
                    TongTien = TongTien - TongTien * vat / 100;
                    var tongtien = String.Format("{0:N0}", TongTien);
                    Session["TongSL"] = TongSL.ToString();
                    Session["TongTien"] = tongtien.ToString();
                }
                else
                {
                    var tongtien = String.Format("{0:N0}", TongTien);
                    Session["TongSL"] = TongSL.ToString();
                    Session["TongTien"] = tongtien.ToString();
                }

                return View(listSP);
            }
        }

        [HttpPost]
        public ActionResult ThemGioHang(int iMaSP, int? SL)
        {

            List<SanPhamGH> lstSP = LayGioHang();
            SanPhamGH SP = lstSP.Find(n => n.MaSP == iMaSP);
            if (SP == null)
            {
                SP = new SanPhamGH();
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                SP.MaSP = iMaSP;
                SP.TenSP = sp.TenSP;
                SP.HinhAnh = sp.HinhAnh;
                SP.SoLuong = int.Parse(sp.SoLuong.ToString());
                SP.DonGia = double.Parse(sp.DonGia.ToString());
                if (SL == null)
                {
                    SP.SoLuongMua = 1;
                }
                else
                {
                    SP.SoLuongMua = int.Parse(SL.ToString());
                }
                lstSP.Add(SP);
                Session["GioHang"] = lstSP;

                return Json(lstSP, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (SL > SP.SoLuong)
                {
                    return Json(1);
                }
                else
                {
                    if(SL== null)
                    {
                        SP.SoLuongMua++;

                    }
                    else
                    {
                        SP.SoLuongMua = int.Parse(SL.ToString());
                    }
                }
                    
                Session["GioHang"] = lstSP;
                return Json(lstSP, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult XoaSP(int iMaSP)
        {
            List<SanPhamGH> lstSP = LayGioHang();
            SanPhamGH SP = lstSP.Find(n => n.MaSP == iMaSP);
            lstSP.Remove(SP);
            Session["GioHang"] = lstSP;
            return Json(lstSP, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GioHang(FormCollection frm, DonHang donhang,SanPham sanPham)
        {
           
            Session["NgayGiaoHang"] = DateTime.Parse(DateTime.Now.ToString());
            Session["NgayNhanHang"] = DateTime.Parse(DateTime.Now.AddDays(+5).ToString("dd-MM-yyyy"));
            int x = 0;
            string danggiao = "Đang giao";
            string ngaygiao = "19/06/2020";
            if (Session["Email"] == null && Session["username"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {

                if (Session["username"] != null)
                {
                    x = int.Parse(Session["makh"].ToString());
                }
                else
                {
                    x = int.Parse(Session["MaKH"].ToString());
                }
                var user = db.KhachHangs.FirstOrDefault(n => n.MaKH == x);

                if (user.DiaChiKH == null || user.SoDienThoaiKH == null)
                {
                    user = db.KhachHangs.Find(x);
                    {
                        user.SoDienThoaiKH = frm["dienthoainhanhang"];
                        user.DiaChiKH = frm["diachinhanhang"];

                    };
                    if (Session["username"] != null)
                    {
                        db.Entry(user);
                        donhang.MaKH = int.Parse(Session["makh"].ToString());
                        donhang.NgayDatHang = DateTime.Parse(DateTime.Now.ToString());
                        donhang.NgayGiaoHang =DateTime.Parse(DateTime.Now.AddDays(+5).ToString("dd-MM-yyyy"));
                        donhang.TongTien = decimal.Parse(Session["TongTien"].ToString());
                        donhang.TrangThai = danggiao;
                        donhang.TenNguoiNhan = frm["tennguoinhan"];
                        donhang.DienThoaiKH = frm["dienthoainhanhang"];
                        donhang.DiaChiNhan = frm["diachinhanhang"];
                        donhang.EmailNhanHang = frm["email"];
                        db.DonHangs.Add(donhang);
                        db.SaveChanges();
                        List<SanPhamGH> listSP = LayGioHang();
                        foreach (var item in listSP)
                        {
                            SanPham sanPham1 = db.SanPhams.Where(n => n.MaSP == item.MaSP).SingleOrDefault();
                            ChiTietDH ctdh = new ChiTietDH();
                            ctdh.SoDH = donhang.SoHD;
                            ctdh.MaSP = item.MaSP;
                            ctdh.SoLuong = item.SoLuongMua;
                            ctdh.DonGia = (decimal)item.DonGia;
                            sanPham1.SoLuong = sanPham1.SoLuong - item.SoLuongMua;
                            db.ChiTietDHs.Add(ctdh);
                            db.SaveChanges();
                        }
                        Session["GioHang"] = null;
                        return RedirectToAction("ThanhToanThanhCong", "GioHang");
                    }
                    else
                    {
                        db.Entry(user);
                        donhang.MaKH = int.Parse(Session["MaKH"].ToString());
                        donhang.NgayDatHang = DateTime.Parse(DateTime.Now.ToString());
                        donhang.NgayGiaoHang =  DateTime.Parse(DateTime.Now.AddDays(+5).ToString("dd-MM-yyyy"));
                        donhang.TongTien = decimal.Parse(Session["TongTien"].ToString());
                        donhang.TrangThai = danggiao;
                        donhang.TenNguoiNhan = frm["tennguoinhan"];
                        donhang.DienThoaiKH = frm["dienthoainhanhang"];
                        donhang.DiaChiNhan = frm["diachinhanhang"];
                        donhang.EmailNhanHang = frm["email"];
                        db.DonHangs.Add(donhang);
                        db.SaveChanges();
                        List<SanPhamGH> listSP = LayGioHang();
                        foreach (var item in listSP)
                        {
                            SanPham sanPham1 = db.SanPhams.Where(n => n.MaSP == item.MaSP).SingleOrDefault();
                            ChiTietDH ctdh = new ChiTietDH();
                            ctdh.SoDH = donhang.SoHD;
                            ctdh.MaSP = item.MaSP;
                            ctdh.SoLuong = item.SoLuongMua;
                            ctdh.DonGia = (decimal)item.DonGia;
                            sanPham1.SoLuong = sanPham1.SoLuong - item.SoLuongMua;
                            db.ChiTietDHs.Add(ctdh);
                            db.SaveChanges();

                        }

                        Session["GioHang"] = null;
                        return RedirectToAction("ThanhToanThanhCong", "GioHang");
                    }
                }
                else
                {
                    if (Session["username"] != null)
                    {
                        donhang.MaKH = int.Parse(Session["makh"].ToString());
                        donhang.NgayDatHang = DateTime.Parse(DateTime.Now.ToString());
                        donhang.NgayGiaoHang =  DateTime.Parse(DateTime.Now.AddDays(+5).ToString("dd-MM-yyyy"));
                        donhang.TongTien = decimal.Parse(Session["TongTien"].ToString());
                        donhang.TrangThai = danggiao;
                        donhang.TenNguoiNhan = frm["tennguoinhan"];
                        donhang.DienThoaiKH = frm["dienthoainhanhang"];
                        donhang.DiaChiNhan = frm["diachinhanhang"];
                        donhang.EmailNhanHang = frm["email"];
                        db.DonHangs.Add(donhang);
                        db.SaveChanges();
                        List<SanPhamGH> listSP = LayGioHang();
                        foreach (var item in listSP)
                        {
                            SanPham sanPham1 = db.SanPhams.Where(n => n.MaSP == item.MaSP).SingleOrDefault();
                            ChiTietDH ctdh = new ChiTietDH();
                            ctdh.SoDH = donhang.SoHD;
                            ctdh.MaSP = item.MaSP;
                            ctdh.SoLuong = item.SoLuongMua;
                            ctdh.DonGia = (decimal)item.DonGia;
                            sanPham1.SoLuong = sanPham1.SoLuong - item.SoLuongMua;
                            db.ChiTietDHs.Add(ctdh);
                            db.SaveChanges();

                        }

                        Session["Madh"] = donhang.SoHD;
                        return RedirectToAction("ThanhToanThanhCong", "GioHang");
                    }
                    else
                    {
                        donhang.MaKH = int.Parse(Session["MaKH"].ToString());
                        donhang.NgayDatHang = DateTime.Parse(DateTime.Now.ToString());
                        donhang.NgayGiaoHang = DateTime.Parse(DateTime.Now.AddDays(+5).ToString("dd-MM-yyyy"));
                        donhang.TongTien = decimal.Parse(Session["TongTien"].ToString());
                        donhang.TrangThai = danggiao;
                        donhang.TenNguoiNhan = frm["tennguoinhan"];
                        donhang.DienThoaiKH = frm["dienthoainhanhang"];
                        donhang.DiaChiNhan = frm["diachinhanhang"];
                        donhang.EmailNhanHang = frm["email"];
                        db.DonHangs.Add(donhang);
                        db.SaveChanges();
                        List<SanPhamGH> listSP = LayGioHang();
                        foreach (var item in listSP)
                        {
                            SanPham sanPham1 = db.SanPhams.Where(n => n.MaSP == item.MaSP).SingleOrDefault();
                            ChiTietDH ctdh = new ChiTietDH();
                            ctdh.SoDH = donhang.SoHD;
                            ctdh.MaSP = item.MaSP;
                            ctdh.SoLuong = item.SoLuongMua;
                            ctdh.DonGia = (decimal)item.DonGia;
                            sanPham1.SoLuong = sanPham1.SoLuong - item.SoLuongMua;
                            db.ChiTietDHs.Add(ctdh);
                            db.SaveChanges();
                        }

                        Session["Madh"] = donhang.SoHD;
                        return RedirectToAction("ThanhToanThanhCong", "GioHang");
                    }

                }

            }

        }
        public ActionResult ThanhToanThanhCong()
        {
            @Session["TongTien"] = null;
            @Session["TongSL"] = null;

            int x = int.Parse(Session["Madh"].ToString());
            List<SanPhamGH> listSP = LayGioHang();
            List<DonHang> dh = db.DonHangs.Where(n => n.SoHD == x).ToList();
            List<ChiTietDH> ten = db.ChiTietDHs.Where(n => n.SoDH == x).ToList();
            ViewData["tensp"] = ten;
            ViewData["dondathang"] = dh;
            return View(listSP);
        }



        [HttpPost]
        public ActionResult ThanhToanThanhCong(ShopVongTay.Models.MailModel model)
        {
            if (ModelState.IsValid)
            {
                string to = model.To;
                string subject = model.Subject;
                string body = model.Body;

                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.From = new MailAddress("phamvantrunggg@gmail.com");
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("phamvantrunggg@gmail.com", "buonvideptrai");
                smtp.Send(mail);
                Session["MaGiam"] = null;
                Session["soluongma"] = null;
                //ViewBag.message = "giui r";
                return RedirectToAction("TrangChu", "Home");

            }
            return View();
        }




        public ActionResult ThanhToan()
        {
            int x = int.Parse(Session["makh"].ToString());
            List<KhachHang> KH = db.KhachHangs.Where(n => n.MaKH == x).ToList();
            return View(KH);
        }

        public ActionResult giohangg()
        {
            
            return View();
        }
        public ActionResult MaGiam(string magiam)
        {
            Session["loi"] = null;
            var Magiam = db.MaGiams.Where(x => x.MaGiamGia == magiam).FirstOrDefault();

            if (db.MaGiams.Where(x => x.MaGiamGia == magiam).SingleOrDefault() != null)
            {
                if(Magiam.SoLuong>0)
                {
                    DateTime dt = DateTime.Parse(DateTime.Now.ToString());
                    if (DateTime.Parse(Magiam.NgayHetHan.ToString())>=dt)
                    {
                        Session["loi"] = 1;
                        MaGiam dh = db.MaGiams.Find(Magiam.ID);
                        Session["MaGiam"] = Magiam.GiaTri;
                        Session["soluongma"] = null;
                        dh.SoLuong--;
                        db.Entry(dh).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("GioHang");
                    }
                    else
                    {
                        Session["MaGiam"] = null;
                        Session["loi"] = "Code hết hạn hoặc sai";
                        return RedirectToAction("GioHang");
                    }
                }
                else
                {
                    Session["MaGiam"] = null;
                    Session["loi"] = "Code hết số lượng";
                    return RedirectToAction("GioHang");
                }
                
            }
            else
            {
                Session["MaGiam"] = null;
                Session["loi"] = "Code hết hạn hoặc sai";
                return RedirectToAction("GioHang");
            }



        }
    }
}
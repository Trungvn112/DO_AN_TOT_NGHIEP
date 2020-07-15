using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;

namespace ShopVongTay.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        ShopVongTayEntities db = new ShopVongTayEntities();
        public ActionResult ThongKe(int? iyear, int? imonth)
        {
            List<int> ItemMonth = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                ItemMonth.Add(i);
            }
            List<int> ItemYear = new List<int>();
            for (int i = 2019; i <= 2030; i++)
            {
                ItemYear.Add(i);
            }
            SelectList itemMonth = new SelectList(ItemMonth);
            SelectList itemYear = new SelectList(ItemYear);
            // Set vào ViewBag
            ViewBag.ItemMonth = itemMonth;
            ViewBag.ItemYear = itemYear;

            //int month = int.Parse(frmTao["thang"]);
            //int year = int.Parse(frmTao["nam"]);


            List<DonHang> ddh = db.DonHangs.Where(n => n.NgayDatHang.Value.Month == imonth && n.NgayDatHang.Value.Year == iyear && n.TrangThai == "Đã giao").ToList();
            var x = db.DonHangs.Where(n => n.NgayDatHang.Value.Month == imonth && n.NgayDatHang.Value.Year == iyear && n.TrangThai == "Đã giao").Sum(n => n.TongTien);
            Session["TongDG"] = x;
            Session["month"] = imonth;
            Session["year"] = iyear;
            Session["data"] = ddh;

            return View(ddh);
        }

        //public PartialViewResult DSThongKePartial(int IdMonth = 10, int IdYear = 2019)
        //{
        //    List<int> ItemMonth = new List<int>();
        //    for (int i = 1; i < 13; i++)
        //    {
        //        ItemMonth.Add(i);
        //    }
        //    List<int> ItemYear = new List<int>();
        //    for (int i = 2019; i <= 2030; i++)
        //    {
        //        ItemYear.Add(i);
        //    }
        //    SelectList itemMonth = new SelectList(ItemMonth);
        //    SelectList itemYear = new SelectList(ItemYear);
        //    // Set vào ViewBag
        //    ViewBag.ItemMonth = itemMonth;
        //    ViewBag.ItemYear = itemYear;

        //    // SELECT* FROM KHACHHANG WHERE MONTH(NGAYSINH) = 2 AND YEAR(NGAYSINH)= 2000
        //    var DSThongKeThang = db.DonHangs.Where(n => n.NgayDatHang.Value.Month == IdMonth && n.NgayDatHang.Value.Year == IdYear).ToList();

        //    ViewBag.DS = DSThongKeThang;
        //    return PartialView(ViewBag.DS);

        //}
        public ActionResult HoaDon(int? year, int? month, int? day)
        {
            List<int> ItemDay = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                ItemDay.Add(i);
            }
            List<int> ItemMonth = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                ItemMonth.Add(i);
            }
            List<int> ItemYear = new List<int>();
            for (int i = 2019; i <= 2030; i++)
            {
                ItemYear.Add(i);
            }
            SelectList itemMonth = new SelectList(ItemMonth);
            SelectList itemday = new SelectList(ItemDay);
            SelectList itemYear = new SelectList(ItemYear);
            // Set vào ViewBag
            ViewBag.ItemMonth = itemMonth;
            ViewBag.ItemYear = itemYear;
            ViewBag.ItemDay = itemday;
            List<DonHang> donhang1 = db.DonHangs.ToList();
            List<DonHang> donhang = db.DonHangs.Where(n => n.NgayDatHang.Value.Month == month && n.NgayDatHang.Value.Year == year && n.NgayDatHang.Value.Day == day).ToList();

            Session["data"] = donhang;
            Session["year"] = year;
            Session["month"] = month;

            return View(donhang1);
        }

        public ActionResult HoaDonNgay(int? day, int? year, int? month)
        {
            List<int> ItemDay = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                ItemDay.Add(i);
            }
            List<int> ItemMonth = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                ItemMonth.Add(i);
            }
            List<int> ItemYear = new List<int>();
            for (int i = 2019; i <= 2030; i++)
            {
                ItemYear.Add(i);
            }
            SelectList itemMonth = new SelectList(ItemMonth);
            SelectList itemday = new SelectList(ItemDay);
            SelectList itemYear = new SelectList(ItemYear);
            // Set vào ViewBag
            ViewBag.ItemMonth = itemMonth;
            ViewBag.ItemYear = itemYear;
            ViewBag.ItemDay = itemday;
            Session["year"] = year;
            Session["month"] = month;
            Session["day"] = day;
            List<DonHang> donhang = db.DonHangs.Where(n => n.NgayDatHang.Value.Day == day && n.NgayDatHang.Value.Month == month && n.NgayDatHang.Value.Year == year).ToList();
            return View(donhang);
        }

        public ActionResult HoaDonThang(int? year, int? month)
        {
            List<int> ItemDay = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                ItemDay.Add(i);
            }
            List<int> ItemMonth = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                ItemMonth.Add(i);
            }
            List<int> ItemYear = new List<int>();
            for (int i = 2019; i <= 2030; i++)
            {
                ItemYear.Add(i);
            }
            SelectList itemMonth = new SelectList(ItemMonth);
            SelectList itemday = new SelectList(ItemDay);
            SelectList itemYear = new SelectList(ItemYear);
            // Set vào ViewBag
            ViewBag.ItemMonth = itemMonth;
            ViewBag.ItemYear = itemYear;
            ViewBag.ItemDay = itemday;
            Session["year"] = year;
            Session["month"] = month;
            List<DonHang> donhang = db.DonHangs.Where(n => n.NgayDatHang.Value.Month == month && n.NgayDatHang.Value.Year == year).ToList();
            return View(donhang);
        }

                public ActionResult HoaDonNam(int? year)
                {
                    List<int> ItemDay = new List<int>();
                    for (int i = 1; i < 31; i++)
                    {
                        ItemDay.Add(i);
                    }
                    List<int> ItemMonth = new List<int>();
                    for (int i = 1; i < 13; i++)
                    {
                        ItemMonth.Add(i);
                    }
                    List<int> ItemYear = new List<int>();
                    for (int i = 2019; i <= 2030; i++)
                    {
                        ItemYear.Add(i);
                    }
                    SelectList itemMonth = new SelectList(ItemMonth);
                    SelectList itemday = new SelectList(ItemDay);
                    SelectList itemYear = new SelectList(ItemYear);
                    // Set vào ViewBag
                    ViewBag.ItemMonth = itemMonth;
                    ViewBag.ItemYear = itemYear;
                    ViewBag.ItemDay = itemday;
                    Session["year"] = year;
                    List<DonHang> donhang = db.DonHangs.Where(n => n.NgayDatHang.Value.Year == year).ToList();
                    return View(donhang);
                }
                public ActionResult EditHoaDon(int? sohd)
                {

                    //CTDONHANG dh = db.CTDONHANGs.Find(sohd);
                    List<ChiTietDH> donhang = db.ChiTietDHs.Where(n => n.SoDH == sohd).ToList();
                    //ViewData["datadh"] = db.SANPHAMs.Where(n => n.MaSP == dh.MaSP );
                    ViewBag.trigia = db.DonHangs.SingleOrDefault(n => n.SoHD == sohd).TongTien;

                    return View(donhang);
                }


        public ActionResult Edit(int? id)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            // List<Category> lis = db.Categories.ToList();

           DonHang dh = db.DonHangs.Find(id);
            return View(dh);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoHD,MaKH,NgayDatHang,NgayGiaoHang,TongTien,TrangThai,TenNguoiNhan,DiaChiNhan,DienThoaiKH,MaHinhThucTT,MaHinhThucGD")] DonHang dh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HoaDon", "ThongKe");
            }
            return View(dh);
        }
        
    }
}
   
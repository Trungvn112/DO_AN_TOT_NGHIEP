using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;

namespace ShopVongTay.Controllers
{
    public class SanPhamController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        // GET: SanPham
        public ActionResult SanPhamNam()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 1|| x.MaLoai == 10 || x.MaLoai == 11) && x.SoLuong > 0).ToList());
        }
        //--------------------------Nữ-------------------//
        public ActionResult SanPhamNu()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 2|| x.MaLoai == 12|| x.MaLoai == 13) && x.SoLuong > 0).ToList());
        }
       
        //----------------------Vòng Tay 108 ------------------------------------//

        public PartialViewResult VongTay108()
        {

            return PartialView(db.SanPhams.Where(x => (x.MaLoai == 8|| x.MaLoai == 21|| x.MaLoai == 22) && x.SoLuong > 0).ToList());

        }
        
        //------------------------------ Vòng Tay Tỳ Hưu-------------------------///
        public ActionResult VongTayTyHuu()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 6 || x.MaLoai == 18|| x.MaLoai == 19) && x.SoLuong > 0).ToList());
        }
       

        //--------------------------Vòng Tay Bọc Vàng-------------------//
        public ActionResult VongTayBocVang()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 7 || x.MaLoai == 20 || x.MaLoai == 7) && x.SoLuong > 0).ToList());
        }
       
        //--------------------------Trầm Hương Chìm-------------------//

        public ActionResult TramHuongChim()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 9|| x.MaLoai == 23|| x.MaLoai == 24) && x.SoLuong > 0).ToList());
        }
       
        //--------------------------Phụ Kiện-------------------//


        public ActionResult PhuKien()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 4|| x.MaLoai == 16|| x.MaLoai == 17) && x.SoLuong > 0).ToList());
        }
        //--------------------------Nhang Trầm-------------------//
        public ActionResult NhangTram()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 3 || x.MaLoai == 14|| x.MaLoai == 15) && x.SoLuong > 0).ToList());
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using PagedList;
using System.Data.Entity;
using System.Threading;
using System.Globalization;
using System.IO;

namespace ShopVongTay.Areas.Admin.Controllers
{
    public class TinMoiController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        // GET: Admin/TinMoi
        public ActionResult BangTin(int? page)
        {
            
            if (page == null)
                    page = 1;
                return View(db.TinTucs.ToList().ToPagedList(page ?? 1, 8));

        }



        public ActionResult ThemBangTin(int? page)
        {
            ViewBag.MaLoaiTinTuc = new SelectList(db.LoaiTinTucs.Where(n => (n.MaLoaiTinTuc != 0)).ToList().OrderBy(n => n.TenLoaiTT), "MaLoaiTinTuc", "TenLoaiTT");
            return View();
        }

       
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemBangTin(FormCollection frm, TinTuc baiviet, HttpPostedFileBase AnhDaiDien)
        {
            //try
            //{
                ViewBag.MaLoaiTinTuc = new SelectList(db.LoaiTinTucs.Where(n => (n.MaLoaiTinTuc != 0)).ToList().OrderBy(n => n.TenLoaiTT), "MaLoaiTinTuc", "TenLoaiTT");
                string path = Server.MapPath("~/img/sanpham/");
                AnhDaiDien.SaveAs(path + Path.GetFileName(AnhDaiDien.FileName));
                baiviet.AnhDaiDien = AnhDaiDien.FileName;
                baiviet.MaLoaiTinTuc = int.Parse(frm["MaLoaiTinTuc"]);
                baiviet.TenBaiViet = frm["TenBaiViet"];
                baiviet.Keywords = frm["MetaKeywords"];
                baiviet.Description = frm["MetaDescription"];
                baiviet.MetaTitle = frm["MetaTitle"];
                baiviet.Title = frm["Title"];
                baiviet.CapNhat = DateTime.Parse(DateTime.Now.ToString());
                baiviet.NoiDung = frm["NoiDung"];
                db.TinTucs.Add(baiviet);
                db.SaveChanges();
                //return Content(@"<script language='javascript' type='text/javascript'>
                //         alert('Đăng nhập không thành công!');
                //         window.location.href='/Admin/TinTucAdmin/TinTuc'
                //         </script>
                //      ");
                return RedirectToAction("BangTin", "TinMoi");
            //}
            //catch (Exception ex)
            //{
            //    RedirectToAction("DangNhapAdmin", "DangNhapAdmin");
            //}
            //return View();
        }
        public ActionResult DeleteTinTuc(int? ID)
        {

            TinTuc sp = db.TinTucs.Find(ID);
          
            return View(sp);
        }

        [HttpPost, ActionName("DeleteTinTuc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTinTucc(int ID)
        {

            TinTuc sp = db.TinTucs.Find(ID);
            db.TinTucs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("BangTin");
        }
    }

}
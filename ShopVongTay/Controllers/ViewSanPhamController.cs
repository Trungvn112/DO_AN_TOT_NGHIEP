using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using ShopVongTay.Models;

namespace ShopVongTay.Controllers
{
    public class ViewSanPhamController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        // GET: ViewSanPham
        public ActionResult ChiTietSanPham(int? MaSP , int? Maloai)
        {
            if (MaSP == null)
            {
                MaSP = int.Parse(TempData["MaSP"].ToString());
                List<DanhGia> DG = db.DanhGias.Where(n => n.MaSP == MaSP).OrderByDescending(n => n.MaDanhGia).ToList();
                ViewData["listDG"] = DG;
                List<DanhGia> LDG = db.DanhGias.Where(n => n.MaSP == MaSP).ToList();
                double DGTB = 0;
                int DiemDG = 0;
                for (int i = 0; i < LDG.Count(); i++)
                {
                    DiemDG += int.Parse(LDG[i].DiemDG.ToString());
                }
                DGTB = DiemDG / LDG.Count();

                return View(db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP));
            }
            else
            {
                var countdg = db.DanhGias.Where(x => x.MaSP == MaSP).Count();
                List<DanhGia> DG = db.DanhGias.Where(n => n.MaSP == MaSP).OrderByDescending(n => n.MaDanhGia).ToList();
                List<SanPham> sp = db.SanPhams.Where(n => n.MaLoai == Maloai).Take(4).ToList();
                ViewData["listDG"] = DG;
                Session["CountDG"] = countdg;
                ViewData["lstSP"] = sp;
                return View(db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP));
            }
        }
        [HttpPost]
        public ActionResult ChiTietSanPham(int iMaSP, string iComment, string Ngay, int iRating, DanhGia DG)
        {
            if (Session["username"] == null && Session["Email"] == null )
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            if(Session["username"] != null)
            {
                DG.MaKH = int.Parse(Session["MaKH"].ToString());
                DG.MaSP = iMaSP;
                DG.cmtDanhGia = iComment;
                DG.DiemDG = iRating;
                DG.NgayCmt = DateTime.Parse(Ngay);
                TempData["MaSP"] = iMaSP;
                db.DanhGias.Add(DG);
                db.SaveChanges();
                List<DanhGia> LDG = db.DanhGias.Where(n => n.MaSP == iMaSP).ToList();
                double DGTB = 0;
                int DiemDG = 0;
                for (int i = 0; i < LDG.Count(); i++)
                {
                    DiemDG += int.Parse(DG.DiemDG.ToString());
                }
                DGTB = DiemDG / LDG.Count();

                return RedirectToAction("TrangChu", "Home");
            }
            else if (Session["Email"] != null)
            {
                DG.MaKH = int.Parse(Session["MaKH"].ToString());
                DG.MaSP = iMaSP;
                DG.cmtDanhGia = iComment;
                DG.DiemDG = iRating;
                DG.NgayCmt = DateTime.Parse(Ngay);
                TempData["MaSP"] = iMaSP;
                db.DanhGias.Add(DG);
                db.SaveChanges();
                List<DanhGia> LDG = db.DanhGias.Where(n => n.MaSP == iMaSP).ToList();
                double DGTB = 0;
                int DiemDG = 0;
                for (int i = 0; i < LDG.Count(); i++)
                {
                    DiemDG += int.Parse(DG.DiemDG.ToString());
                }
                DGTB = DiemDG / LDG.Count();

                return RedirectToAction("TrangChu", "Home");
            }
            return RedirectToAction("TrangChu", "Home");
        }
    }
}
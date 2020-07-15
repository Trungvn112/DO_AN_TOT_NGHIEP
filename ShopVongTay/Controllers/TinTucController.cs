using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;

namespace ShopVongTay.Controllers
{
    public class TinTucController : Controller
    {
        ShopVongTayEntities db = new  ShopVongTayEntities();
        // GET: TinTuc
        public ActionResult TatCaTinTuc()
        {
            return View(db.TinTucs.Where(x => x.MaLoaiTinTuc == 1 || x.MaLoaiTinTuc == 1 || x.MaLoaiTinTuc == 2 || x.MaLoaiTinTuc == 3 || x.MaLoaiTinTuc == 4).ToList());
        }
        public ActionResult ChiTietTinTuc(int? ID)
        {
            List<TinTuc> sp = db.TinTucs.Where(n => n.ID == ID).ToList();
            return View(db.TinTucs.SingleOrDefault(n => n.ID == ID));
        }
    }
}
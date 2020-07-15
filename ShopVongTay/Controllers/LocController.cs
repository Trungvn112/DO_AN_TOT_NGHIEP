using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;
using System.Reflection;
using System.Linq.Dynamic;
using PagedList;
using System.Data.Entity;
using System.Threading;
using System.Globalization;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Security.Claims;

namespace ShopVongTay.Controllers
{
    public class LocController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        // GET: Loc
        public ActionResult Duoi1Trieu()
        {
            return View(db.SanPhams.Where(x => x.DonGia < 1000000 ).ToList());
        }

        public ActionResult Tu1Toi4()
        {
            return View(db.SanPhams.Where(x => x.DonGia > 1000000 && x.DonGia < 4000000 ).ToList());
        }
        public ActionResult Tu4Den7()
        {
            return View(db.SanPhams.Where(x => x.DonGia > 4000000 && x.DonGia < 7000000).ToList());
        }
        public ActionResult Tu7Den13()
        {
            return View(db.SanPhams.Where(x => x.DonGia > 7000000 && x.DonGia < 13000000).ToList());
        }
        public ActionResult Tren13trieu()
        {
            return View(db.SanPhams.Where(x => x.DonGia > 13000000 ).ToList());
        }
    }
}
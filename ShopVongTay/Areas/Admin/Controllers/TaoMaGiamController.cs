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


namespace ShopVongTay.Areas.Admin.Controllers
{
    public class TaoMaGiamController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        // GET: Admin/TaoMaGiam
        public class HttpParamActionAttribute : ActionNameSelectorAttribute
        {
            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                    return true;

                var request = controllerContext.RequestContext.HttpContext.Request;
                return request[methodInfo.Name] != null;
            }
        }
        public ActionResult TaoMa()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TaoMa(int? size, int? page, string sortProperty, string sortOrder, string searchString)
        {
            if (Session["MaAD"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "DangNhapAdmin");
            }

            ViewBag.searchValue = searchString;
            ViewBag.sortProperty = sortProperty;
            ViewBag.page = page;

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });

            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;

            var links = from l in db.MaGiams select l;
            // 5. T?o thu?c tính s?p x?p m?c d?nh là "LinkID"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "BookId";

            // 5. S?p x?p tang/gi?m b?ng phuong th?c OrderBy s? d?ng trong thu vi?n Dynamic LINQ
            if (sortOrder == "desc") links = links.OrderBy(sortProperty + " desc");
            else if (sortOrder == "asc") links = links.OrderBy(sortProperty);
            else links = links.OrderBy("MaGiamGia");

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.MaGiamGia.Contains(searchString));
            }

            page = page ?? 1;


            int pageSize = (size ?? 5);

            ViewBag.pageSize = pageSize;

            // 6. Toán t? ?? trong C# mô t? n?u page khác null thì l?y giá tr? page, còn
            // n?u page = null thì l?y giá tr? 1 cho bi?n pageNumber. --- dammio.com
            int pageNumber = (page ?? 1);

            // 6.2 L?y t?ng s? record chia cho kích thu?c d? bi?t bao nhiêu trang
            int checkTotal = (int)(links.ToList().Count / pageSize) + 1;
            // N?u trang vu?t qua t?ng s? trang thì thi?t l?p là 1 ho?c t?ng s? trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            return View(links.ToPagedList(pageNumber, pageSize));

        }
        [HttpPost, HttpParamAction]
        public ActionResult Reset()
        {
            ViewBag.searchValue = "";
            //IndexAdmin(null, null, "", "", "");
            return View();

        }
        public ActionResult DeleteMa(int? id)
        {

            MaGiam sp = db.MaGiams.Find(id);
            Session["itemid"] = id;
            return View(sp);
        }

        [HttpPost, ActionName("DeleteMa")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            MaGiam sp = db.MaGiams.Find(id);
            db.MaGiams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("TaoMa");
        }
        public ActionResult EditMa(int? id)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            //ViewBag.imgurl = db.SanPhams.SingleOrDefault(n => n.MaSP == id).HinhAnh;
            // List<Category> lis = db.Categories.ToList();

            MaGiam sp = db.MaGiams.Find(id);
            return View(sp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMa([Bind(Include = "ID,MaGiamGia,GiaTri,NgayHetHan,SoLuong")]  MaGiam sp, FormCollection frmTao)
        {
            if (ModelState.IsValid)
            {
                sp.NgayHetHan = DateTime.Parse(frmTao["NgayHetHan"]);
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TaoMa");
            }
            return View(sp);
        }
        public ActionResult ThemMaGiam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMaGiam(FormCollection frmTao, MaGiam sp)
        {

            sp.MaGiamGia = frmTao["name"];
            sp.GiaTri = Convert.ToInt32(frmTao["mota"]);
            sp.NgayHetHan = DateTime.Parse(frmTao["Loai"]); /*Convert.ToInt32(frmTao["Loai"]);*/
            //Book.AuthorId = Convert.ToInt32(frmTao["author"]);
            //Book.PubId = Convert.ToInt32(frmTao["pub"]);
            
            sp.SoLuong = Convert.ToInt32(frmTao["SL"]);
            //sp.TrangThai = frmTao["trangthai"];
            ////sp.NgayTao = DateTime.Now;
            //sp.HinhAnh = frmTao["fileUpload"];
            ////sp.Donvitinh = frmTao["dvtinh"];
            db.MaGiams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("TaoMa", "TaoMaGiam");
        }


    }
}
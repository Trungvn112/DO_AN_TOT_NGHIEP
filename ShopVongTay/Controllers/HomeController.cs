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
using System.Security.Cryptography;

namespace ShopVongTay.Controllers
{
    public class HomeController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
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
        public ActionResult TrangChu(int? soluong)
        {
            //var SanPham = db.SanPhams.Where(x => x.SoLuong == soluong).FirstOrDefault();
            //SanPham dh = db.SanPhams.Find(SanPham.MaSP);
            //Session["SanPham"] = SanPham.SoLuong;
           
            return View(db.SanPhams.Where(x => x.MaLoai == 1 ).ToList());
        }
       
        [HttpGet]
        public ActionResult Popup(int? iMaSP)
        {
            SanPham sp = (from p in db.SanPhams where p.MaSP == iMaSP select p).ToArray()[0];
            return Json(new { name = sp.TenSP, price = sp.DonGia, hinhminhhoa = sp.HinhAnh, mota = sp.MoTa, trangthai = sp.TrangThai, soluong = sp.SoLuong, masp = sp.MaSP, gioitinh=sp.GioiTinh }, JsonRequestBehavior.AllowGet);

        }
        public PartialViewResult spnu()
        {

            return PartialView(db.SanPhams.Take(20).Where(x => (x.MaLoai == 2 || x.MaLoai == 3 || x.MaLoai == 3 || x.MaLoai == 4) && x.SoLuong>0).ToList());

        }
        public PartialViewResult spnam()
        {

            return PartialView(db.SanPhams.Where(x => (x.MaLoai == 1) && x.SoLuong > 0).ToList());

        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        




        //------------------------------Sản Phẩm --------------------------------//
        public ActionResult SanPham()
        {
            return View(db.SanPhams.Where(x => (x.MaLoai == 1 || x.MaLoai == 2 || x.MaLoai == 3 || x.MaLoai == 4 || x.MaLoai == 5 || x.MaLoai == 6 || x.MaLoai == 7 || x.MaLoai == 8 || x.MaLoai == 9 || x.MaLoai == 10 || x.MaLoai == 11 || x.MaLoai == 12 || x.MaLoai == 13) && x.SoLuong > 0).ToList());
        }
        [HttpGet]
        public ActionResult SanPham(int? size, int? page, string sortProperty, string sortOrder, string searchString)
        {
            

            ViewBag.searchValue = searchString;
            ViewBag.sortProperty = sortProperty;
            ViewBag.page = page;

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "9", Value = "9" });
            items.Add(new SelectListItem { Text = "18", Value = "18" });
            items.Add(new SelectListItem { Text = "27", Value = "27" });
            items.Add(new SelectListItem { Text = "36", Value = "36" });

            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;

            var links = from l in db.SanPhams select l;
            // 5. T?o thu?c tính s?p x?p m?c d?nh là "LinkID"
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "BookId";

            // 5. S?p x?p tang/gi?m b?ng phuong th?c OrderBy s? d?ng trong thu vi?n Dynamic LINQ
            if (sortOrder == "desc") links = links.OrderBy(sortProperty + " desc");
            else if (sortOrder == "asc") links = links.OrderBy(sortProperty);
            else links = links.OrderBy("TenSP");

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.TenSP.Contains(searchString));
            }

            page = page ?? 1;


            int pageSize = (size ?? 9);

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

        //-----------------------------------------End Sản Phẩm---------------------- //
        //-----------------------------------------Đăng Nhập---------------------- //
        public ActionResult DangXuat()
        {
            Session["GioHang"] = null;
            Session["MaKH"] = null;
            Session["Email"] = null;
            Session["Password"] = null;
            return RedirectToAction("TrangChu", "Home");
        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection frmDN)
        {
                string sTaiKhoan = frmDN["TenDN"];
                string sMatKhau = GetMD5(frmDN["MatKhau"]);
                //Lấy username và password ở bản ghi đầu tiên
                var user = db.KhachHangs.Where(x => x.TenDN == sTaiKhoan && x.MatKhau == sMatKhau).FirstOrDefault();
                if (user == null)
                {

                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu sai";
                return Content(@"<script language='javascript' type='text/javascript'>
                         alert('Tên đăng nhập hoặc mật khẩu sai !');
                         window.location.href='/Home/DangNhap'
                         </script>
                      ");
            }
                else
                {
                    //ViewBag.avatar = user.Avatar;
                    //ViewBag.Online = user.IsActive;
                    //Session["Online"] = user.IsActive;
                    //Session["Avatar"] = user.Avatar;
                    Session["MaKH"] = user.MaKH;
                    Session["Email"] = user.TenDN;
                    Session["Password"] = user.MatKhau;
                //return View(user)

                return Content(@"<script language='javascript' type='text/javascript'>
                         alert('Đăng nhập thành công!');
                         window.location.href='/Home/TrangChu'
                         </script>
                      ");
            }
        }
        //-----------------------------------------Đăng Nhập---------------------- //
        public ActionResult TimKiem(string name)
        {
            List<SanPham> sp = db.SanPhams.Where(n => n.TenSP.Contains(name) && n.SoLuong > 0).ToList();
            Session["nametimkiem"] = name;
            return View(sp);
        }
        //-----------------------------------------Đăng Ký---------------------- //
        public ActionResult DangKy()
        {
            return View();
        }

     
        

        public ActionResult TinTuc()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangKy(FormCollection frmDK, KhachHang KH)
        {
            KH.HoTenKH = frmDK["hoten"];
            KH.DiaChiKH = frmDK["diachi"];
            KH.TenDN = frmDK["tendn"];
            KH.MatKhau =GetMD5(frmDK["matkhau"]);
            KH.Email = frmDK["email"];
            db.KhachHangs.Add(KH);
            db.SaveChanges();
            return Content(@"<script language='javascript' type='text/javascript'>
                         alert('Đăng ký thành công!');
                         window.location.href='/Home/TrangChu'
                         </script>
                      ");
        }
        public string GetMD5(string MD5)
        {
            string str = "";
            byte[] A = System.Text.Encoding.UTF8.GetBytes(MD5);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            A = md5.ComputeHash(A);
            foreach (Byte b in A)
            {
                str += b.ToString("X2");
            }
            return str;
        }

       //Google//
        public void SignIn(string ReturnUrl = "/", string type = "")
        {
            if (!Request.IsAuthenticated)
            {
                if (type == "Google")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "SSO/GoogleLoginCallback" }, "Google");
                }
            }
        }
        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Redirect("~/");
        }

        [AllowAnonymous]
        public ActionResult GoogleLoginCallback()
        {
            var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;

            var loginInfo = SSO.GetLoginInfo(claimsPrincipal);
            if (loginInfo == null)
            {
                return RedirectToAction("DangNhap");
            }
            ShopVongTayEntities db = new ShopVongTayEntities(); //DbContext
            var user = db.KhachHangs.FirstOrDefault(x => x.Email == loginInfo.emailaddress);

            if (user == null)
            {
                user = new KhachHang
                {

                    Email = loginInfo.emailaddress,
                    HoTenKH = loginInfo.name,
                    DiaChiKH = loginInfo.nameidentifier,

                };
                db.KhachHangs.Add(user);
                db.SaveChanges();
            }
            Session["username"] = loginInfo.name;
            Session["makh"] = user.MaKH;

            var ident = new ClaimsIdentity(
                    new[] { 
									// adding following 2 claim just for supporting default antiforgery provider
									new Claim(ClaimTypes.NameIdentifier, user.Email),
                                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                                    new Claim(ClaimTypes.Name, user.HoTenKH),
                                    new Claim(ClaimTypes.Email, user.Email),
									// optionally you could add roles if any
									new Claim(ClaimTypes.Role, "User")
                    },
                    CookieAuthenticationDefaults.AuthenticationType);


            HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
            return Redirect("~/");
        }
        public ActionResult LienHe()
        {
            return View();
        }
    }
}
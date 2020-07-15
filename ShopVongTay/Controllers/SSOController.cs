using ShopVongTay.Models;
using Microsoft.Owin.Security.Cookies;
using System.Security.Claims;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using Microsoft.Owin;


namespace ShopVongTay.Controllers
{
    public class SSOController : Controller
    {
        
        public ActionResult DangNhapSSO()
        {
            return View();
        }
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
    }
}
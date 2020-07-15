using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVongTay.Models;

namespace ShopVongTay.Areas.Admin.Controllers
{
    public class DangNhapAdminController : Controller
    {
        ShopVongTayEntities db = new ShopVongTayEntities();

        // GET: Admin/DangNhapAdmin
        public ActionResult DangNhapAdmin()
        {
            Session["MaAD"] = null;
            Session["TenDNAD"] = null;
            
            Session["EmailAD"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult DangNhapAdmin(ADMIN model)
        {
            using (db)
            {

                //Lấy username và password ở bản ghi đầu tiên
                var user = db.ADMINs.Where(x => x.TenDNAD == model.TenDNAD && x.MatKhauAD == model.MatKhauAD).FirstOrDefault();
                if (user == null)
                {

                    ViewBag.error = "Email or Password is fail";
                    return View("DangNhapAdmin", model);
                }
                else
                {
                  
                    Session["MaAD"] = user.MaAD;
                    Session["TenDND"] = user.TenDNAD;
                   
                    Session["EmailAD"] = user.EmailAD;
                    

                    return RedirectToAction("Index", "AdminCRUD");
                }
            }
        }
    }
}
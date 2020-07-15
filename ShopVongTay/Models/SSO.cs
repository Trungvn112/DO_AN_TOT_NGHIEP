using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace ShopVongTay.Models
{
    public class SSO
    {
        ShopVongTayEntities db = new ShopVongTayEntities();
        public string emailaddress { get; set; }
        public string givenname { get; set; }
        public string name { get; set; }
        public string nameidentifier { get; set; }




        internal static SSO GetLoginInfo(ClaimsIdentity identity)
        {
            if (identity.Claims.Count() == 0 || identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email) == null)
            {
                return null;
            }

            return new SSO
            {
                emailaddress = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                givenname = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value,
                name = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value,
                nameidentifier = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
            };
        }

    }
}
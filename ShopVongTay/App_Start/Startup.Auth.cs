using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;


namespace ShopVongTay
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/SSO/DangNhapSSO"),
                SlidingExpiration = true
            });
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "134741838966-0rejrhhgndsh74d0nb1bhj0vs0r9hn0h.apps.googleusercontent.com",
                ClientSecret = "a615LUwz_LAtmDX_dMUXDiEZ",
                CallbackPath = new PathString("/GoogleLoginCallback")
            });
           
            
        }
    }
}


using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace OktaSamlSample.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return new Saml2ChallengeResult(Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("LoginError");
            }

            var identity = new ClaimsIdentity(loginInfo.ExternalIdentity.Claims,
                DefaultAuthenticationTypes.ApplicationCookie);
            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
            };            
            HttpContext.GetOwinContext().Authentication.SignIn(authProps, identity);

            return RedirectToLocal(returnUrl);
        }

        [AllowAnonymous]
        public ActionResult LoginError()
        {
            return Content("Error Logging in!");
        }

        private IAuthenticationManager AuthenticationManager =>
            HttpContext.GetOwinContext().Authentication;

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class Saml2ChallengeResult : HttpUnauthorizedResult
        {
            public string RedirectUri { get; set; }

            public Saml2ChallengeResult(string redirectUri)
            {
                RedirectUri = redirectUri;
            }
            
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, "Saml2");
            }
        }
    }
}
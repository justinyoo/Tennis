using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Tennis.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for account.
    /// </summary>
    [Route("account")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Signs into the application.
        /// </summary>
        /// <returns>Returns the <see cref="ChallengeResult"/>.</returns>
        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Signs out of the application.
        /// </summary>
        /// <returns>Returns the <see cref="SignOutResult"/>.</returns>
        [Route("sign-out")]
        public IActionResult SignOut()
        {
            var callbackUrl = Url.Action("SignedOut", "Account", values: null, protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Gets the /account/signed-out page.
        /// </summary>
        /// <returns>Returns the /account/signed-out page.</returns>
        [Route("signed-out")]
        public IActionResult SignedOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
        }
    }
}
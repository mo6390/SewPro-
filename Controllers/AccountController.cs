using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SewPro.Models;
using SewPro.Data;
using Microsoft.Extensions.Logging;

namespace SewPro.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // صفحة تسجيل الدخول
     public IActionResult Login(string returnUrl = null)
{
    // إعادة التوجيه إلى صفحة تسجيل الدخول الخاصة بـ Identity
    return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl });
}


        // معالجة عملية تسجيل الدخول
[HttpPost]
public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
{
    returnUrl ??= Url.Content("~/");

    var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

    if (result.Succeeded)
    {
        _logger.LogInformation("User {username} logged in successfully.", username);
        return Redirect(returnUrl);
    }

    _logger.LogWarning("Invalid login attempt for user {username}.", username);
    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

    // التوجيه إلى صفحة تسجيل الدخول في الـ Area
    return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl });
}


[HttpGet]
public IActionResult AccessDenied()
{
    return Redirect("/Identity/Account/AccessDenied");
}

        // معالجة عملية التسجيل
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home"); // التوجيه إلى الصفحة الرئيسية بعد التسجيل
            }

            // إضافة الأخطاء في حالة فشل التسجيل
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        // تسجيل الخروج
       public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Login", "Account", new { area = "Identity" }); // إعادة التوجيه إلى صفحة تسجيل الدخول
}

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewPro.Controllers
{
    [Authorize(Roles = "Admin")] // السماح فقط للمسؤولين بالوصول
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityController(UserManager<IdentityUser> userManager, 
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // ✅ عرض قائمة المستخدمين
        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // ✅ عرض قائمة الأدوار
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // ✅ عرض نموذج إنشاء مستخدم جديد
        public IActionResult CreateUser()
        {
            return View();
        }

        // ✅ إنشاء مستخدم جديد
        [HttpPost]
        public async Task<IActionResult> CreateUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "يجب إدخال اسم المستخدم وكلمة المرور.");
                return View();
            }

            var user = new IdentityUser { UserName = userName };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return RedirectToAction("Users");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        // ✅ تعديل بيانات مستخدم
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string userName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(userName))
            {
                user.UserName = userName;
                var result = await _userManager.UpdateAsync(user);
                
                if (result.Succeeded)
                    return RedirectToAction("Users");
                
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }

        // ✅ حذف مستخدم
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users");
        }

        // ✅ عرض نموذج إنشاء دور جديد
        public IActionResult CreateRole()
        {
            return View();
        }

        // ✅ إنشاء دور جديد
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "يجب إدخال اسم الدور.");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
                return RedirectToAction("Roles");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        // ✅ حذف دور
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Roles");
        }

        // ✅ تعيين دور للمستخدم
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _roleManager.RoleExistsAsync(roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return RedirectToAction("Users");
        }

        // ✅ إزالة دور من المستخدم
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            return RedirectToAction("Users");
        }

        // ✅ تسجيل خروج المستخدم
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

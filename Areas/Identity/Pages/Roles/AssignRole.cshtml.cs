using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SewPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SewPro.Areas.Identity.Pages.Roles
{
    public class AssignRoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AssignRoleModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string RoleName { get; set; }

        public List<string> Roles { get; set; }

        public async Task OnGetAsync(string userId)
        {
            UserId = userId;

            // الحصول على قائمة الأدوار المتاحة
            Roles = new List<string>();
            foreach (var role in _roleManager.Roles)
            {
                Roles.Add(role.Name);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(RoleName))
            {
                return Page(); // إذا كانت البيانات غير مكتملة، أعد تحميل الصفحة
            }

            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound($"المستخدم الذي يحمل معرف {UserId} غير موجود.");
            }

            // إضافة الدور للمستخدم
            var result = await _userManager.AddToRoleAsync(user, RoleName);
            if (result.Succeeded)
            {
                TempData["Message"] = "تم تعيين الدور بنجاح.";
                return RedirectToPage("/Users/Index"); // إعادة التوجيه إلى صفحة المستخدمين
            }
            else
            {
                TempData["ErrorMessage"] = "فشل في تعيين الدور.";
                return Page();
            }
        }
    }
}

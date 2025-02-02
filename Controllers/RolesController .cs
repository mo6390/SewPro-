using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SewPro.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SewPro.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // عرض الأدوار
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        // إضافة دور جديد
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateRoleViewModel(); // تهيئة النموذج
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.RoleName))
                {
                    ModelState.AddModelError("", "اسم الدور لا يمكن أن يكون فارغاً");
                    return View(model);
                }

                var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
                if (roleExist)
                {
                    ModelState.AddModelError("", "الدور موجود بالفعل");
                    return View(model);
                }

                var role = new IdentityRole(model.RoleName);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // حذف دور
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }

        // تعيين دور للمستخدم
        [HttpGet]
        public async Task<IActionResult> AssignRoleToUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var model = new AssignRoleViewModel
            {
                UserId = userId,
                Roles = roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }).ToList(),
                SelectedRoles = new List<string>() // إضافة قيمة فارغة لتمريرها إلى الفيو
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.SelectedRoles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(model.SelectedRoles);

            // إضافة الأدوار للمستخدم
            foreach (var role in rolesToAdd)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            // إزالة الأدوار من المستخدم
            foreach (var role in rolesToRemove)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            return RedirectToAction("Index", "Home");  // التوجيه إلى الصفحة الرئيسية بعد تعيين الأدوار
        }
    }
}

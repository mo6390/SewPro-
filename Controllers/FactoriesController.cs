using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SewPro.Controllers
{
    public class FactoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FactoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 عرض جميع المصانع
        public async Task<IActionResult> Index()
        {
            return View(await _context.Factories.ToListAsync());
        }

        // 🔹 عرض تفاصيل المصنع
        public async Task<IActionResult> Details(int id)
        {
            var factory = await _context.Factories.FindAsync(id);
            if (factory == null) return NotFound();
            return View(factory);
        }

        // 🔹 إضافة مصنع جديد (عرض النموذج)
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 إضافة مصنع جديد (حفظ البيانات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factory factory)
        {
            if (ModelState.IsValid)
            {
                _context.Factories.Add(factory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(factory);
        }

        // 🔹 تعديل مصنع (عرض النموذج)
        public async Task<IActionResult> Edit(int id)
        {
            var factory = await _context.Factories.FindAsync(id);
            if (factory == null) return NotFound();
            return View(factory);
        }

        // 🔹 تعديل مصنع (حفظ التعديلات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Factory factory)
        {
            if (id != factory.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(factory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(factory);
        }

        // 🔹 حذف مصنع (عرض التأكيد)
        public async Task<IActionResult> Delete(int id)
        {
            var factory = await _context.Factories.FindAsync(id);
            if (factory == null) return NotFound();
            return View(factory);
        }

        // 🔹 حذف مصنع (تنفيذ الحذف)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factory = await _context.Factories.FindAsync(id);
            if (factory != null)
            {
                _context.Factories.Remove(factory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

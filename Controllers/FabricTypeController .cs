using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SewPro.Controllers
{
    public class FabricTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FabricTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 عرض جميع أنواع الأقمشة
        public async Task<IActionResult> Index()
        {
            return View(await _context.FabricTypes.ToListAsync());
        }

        // 🔹 عرض تفاصيل نوع القماش
        public async Task<IActionResult> Details(int id)
        {
            var fabricType = await _context.FabricTypes.FindAsync(id);
            if (fabricType == null) return NotFound();
            return View(fabricType);
        }

        // 🔹 إضافة نوع جديد (عرض النموذج)
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 إضافة نوع جديد (حفظ البيانات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FabricType fabricType)
        {
            if (ModelState.IsValid)
            {
                _context.FabricTypes.Add(fabricType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fabricType);
        }

        // 🔹 تعديل نوع قماش (عرض النموذج)
        public async Task<IActionResult> Edit(int id)
        {
            var fabricType = await _context.FabricTypes.FindAsync(id);
            if (fabricType == null) return NotFound();
            return View(fabricType);
        }

        // 🔹 تعديل نوع قماش (حفظ التعديلات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FabricType fabricType)
        {
            if (id != fabricType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(fabricType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fabricType);
        }

        // 🔹 حذف نوع قماش (عرض التأكيد)
        public async Task<IActionResult> Delete(int id)
        {
            var fabricType = await _context.FabricTypes.FindAsync(id);
            if (fabricType == null) return NotFound();
            return View(fabricType);
        }

        // 🔹 حذف نوع قماش (تنفيذ الحذف)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fabricType = await _context.FabricTypes.FindAsync(id);
            if (fabricType != null)
            {
                _context.FabricTypes.Remove(fabricType);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

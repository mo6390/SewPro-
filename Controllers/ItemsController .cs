using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SewPro.Data;
using SewPro.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace SewPro.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ApplicationDbContext context, ILogger<ItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Items/Index
        public async Task<IActionResult> Index()
        {
            var items = _context.Items.Include(i => i.ItemGroup);
            return View(await items.ToListAsync());
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            // جلب البيانات من جدول ItemGroups
            var itemGroups = await _context.ItemGroups.ToListAsync();
            ViewBag.ItemGroups = new SelectList(itemGroups, "Id", "ArabicName");

            return View();
        }

        // POST: Items/Create
   [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Item item)
{
    if (ModelState.IsValid)
    {
        try
        {
            // إضافة العنصر إلى قاعدة البيانات
            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // توجيه إلى قائمة العناصر
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "فشل حفظ العنصر");
            ModelState.AddModelError(string.Empty, "حدث خطأ أثناء حفظ البيانات.");
        }
    }
    else
    {
        _logger.LogWarning("النموذج غير صالح، يرجى التحقق من الحقول.");
    }

    // إعادة تحميل القيم
    var itemGroups = await _context.ItemGroups.ToListAsync();
    ViewBag.ItemGroups = new SelectList(itemGroups, "Id", "ArabicName");
    return View(item);  // إعادة عرض النموذج في حالة وجود أخطاء
}

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // تحميل مجموعة العناصر للـ DropDownList
            ViewBag.ItemGroups = new SelectList(await _context.ItemGroups.ToListAsync(), "Id", "ArabicName");
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item, IFormFile imageFile)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // التحقق من وجود ملف صورة
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        item.ImagePath = "/images/" + fileName;
                    }

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));  // توجيه إلى قائمة العناصر
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // إعادة تحميل القيم في حال حدوث خطأ
            ViewBag.ItemGroups = new SelectList(await _context.ItemGroups.ToListAsync(), "Id", "ArabicName");
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                // حذف الصورة من المجلد
                if (!string.IsNullOrEmpty(item.ImagePath))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));  // توجيه إلى قائمة العناصر
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}

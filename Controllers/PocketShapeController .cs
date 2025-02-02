using Microsoft.AspNetCore.Mvc;
using SewPro.Models;
using SewPro.Data;

namespace SewPro.Controllers
{
    public class PocketShapeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PocketShapeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PocketShape/Index
        public IActionResult Index()
        {
            var pocketShapes = _context.PocketShapes.ToList();  // استعلام للحصول على كل الأشكال من قاعدة البيانات
            return View(pocketShapes);  // إرجاع الأشكال إلى الفيو
        }

        // GET: PocketShape/Create
        public IActionResult Create()
        {
            return View();  // عرض الفيو لإنشاء شكل جيب جديد
        }

        // POST: PocketShape/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PocketShape model)
        {
            if (ModelState.IsValid)
            {
              
                _context.PocketShapes.Add(model);  // إضافة شكل الجيب إلى قاعدة البيانات
                _context.SaveChanges();  // حفظ التغييرات في قاعدة البيانات

                return RedirectToAction(nameof(Index));  // إعادة التوجيه إلى صفحة الفهرس
            }
            return View(model);  // إرجاع النموذج إذا كان هناك أخطاء
        }

        // GET: PocketShape/Edit/5
        public IActionResult Edit(int id)
        {
            var pocketShape = _context.PocketShapes.Find(id);  // العثور على شكل الجيب بالـ ID
            if (pocketShape == null)
            {
                return NotFound();  // إذا لم يتم العثور عليه، إرجاع "لم يتم العثور"
            }
            return View(pocketShape);  // إرجاع الفيو لتحرير شكل الجيب
        }

        // POST: PocketShape/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PocketShape model)
        {
            if (id != model.Id)
            {
                return NotFound();  // إذا لم يتطابق المعرف
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(model);  // تحديث البيانات في قاعدة البيانات
                    _context.SaveChanges();  // حفظ التغييرات
                }
                catch
                {
                    return View(model);  // إرجاع النموذج إذا حدث خطأ
                }

                return RedirectToAction(nameof(Index));  // العودة إلى صفحة الفهرس بعد التحديث
            }
            return View(model);  // إرجاع النموذج في حالة وجود أخطاء
        }

        // GET: PocketShape/Delete/5
        public IActionResult Delete(int id)
        {
            var pocketShape = _context.PocketShapes.Find(id);  // العثور على شكل الجيب بالـ ID
            if (pocketShape == null)
            {
                return NotFound();
            }

            return View(pocketShape);  // عرض نموذج الحذف
        }

        // POST: PocketShape/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pocketShape = _context.PocketShapes.Find(id);  // العثور على شكل الجيب
            if (pocketShape != null)
            {
                _context.PocketShapes.Remove(pocketShape);  // حذف شكل الجيب من قاعدة البيانات
                _context.SaveChanges();  // حفظ التغييرات
            }

            return RedirectToAction(nameof(Index));  // العودة إلى صفحة الفهرس بعد الحذف
        }
    }
}

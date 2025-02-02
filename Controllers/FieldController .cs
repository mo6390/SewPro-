using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SewPro.Controllers
{
    public class FieldController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FieldController> _logger;

        public FieldController(ApplicationDbContext context, ILogger<FieldController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // عرض قائمة الحقول
        public IActionResult Index()
        {
            var fields = _context.Fields.ToList();
            return View(fields);
        }

        // عرض صفحة إنشاء حقل جديد
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // إنشاء حقل جديد
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Field field)
{
    if (ModelState.IsValid)
    {
        try
        {
            // إضافة الحقل إلى قاعدة البيانات
            _context.Fields.Add(field);
            _context.SaveChanges(); // حفظ التغييرات
            return RedirectToAction(nameof(Index)); // إعادة التوجيه إلى صفحة القائمة
        }
        catch (Exception ex)
        {
            // تسجيل الخطأ في الـ Logger
            _logger.LogError(ex, "Error occurred while saving the field.");
            ModelState.AddModelError("", "An error occurred while saving the field.");
        }
    }
    else
    {
        // إذا كانت البيانات غير صالحة
        _logger.LogWarning("Model state is invalid.");
    }

    // إعادة عرض النموذج مع الأخطاء
    return View(field);
}

        // عرض تفاصيل حقل معين
        public IActionResult Details(int id)
        {
            var field = _context.Fields.FirstOrDefault(f => f.Id == id);
            if (field == null)
            {
                return NotFound(); // إذا لم يتم العثور على الحقل
            }
            return View(field);
        }

        // عرض صفحة تعديل حقل
        public IActionResult Edit(int id)
        {
            var field = _context.Fields.FirstOrDefault(f => f.Id == id);
            if (field == null)
            {
                return NotFound(); // إذا لم يتم العثور على الحقل
            }

            // إعداد قائمة أنواع الحقول
            var fieldTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Text", Text = "مربع نص" },
                new SelectListItem { Value = "Textarea", Text = "نص متعدد" },
                new SelectListItem { Value = "Image", Text = "رفع صورة" },
                new SelectListItem { Value = "File", Text = "تحميل ملف" },
                new SelectListItem { Value = "Dropdown", Text = "قائمة نصية" },
                new SelectListItem { Value = "DropdownImage", Text = "قائمة مصورة" },
                new SelectListItem { Value = "Checkbox", Text = "مربع اختيار" }
            };

            ViewBag.FieldTypes = fieldTypes;

            return View(field);
        }

        // تعديل حقل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Field field)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Fields.Update(field);
                    _context.SaveChanges(); // حفظ التغييرات
                    return RedirectToAction(nameof(Index)); // إعادة التوجيه إلى صفحة القائمة
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating the field.");
                    ModelState.AddModelError("", "An error occurred while updating the field.");
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
            }

            return View(field); // إعادة عرض صفحة التعديل مع الأخطاء
        }

        // حذف حقل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var field = _context.Fields.FirstOrDefault(f => f.Id == id);
                if (field != null)
                {
                    _context.Fields.Remove(field);
                    _context.SaveChanges(); // حفظ التغييرات
                }
                return RedirectToAction(nameof(Index)); // إعادة التوجيه إلى صفحة القائمة
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the field.");
                return RedirectToAction(nameof(Index)); // إعادة التوجيه إلى صفحة القائمة في حالة حدوث خطأ
            }
        }
    }
}

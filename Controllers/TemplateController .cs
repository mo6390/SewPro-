using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using SewPro.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace SewPro.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض صفحة إنشاء القالب واختيار الحقول
        public IActionResult Create()
        {
            var availableFields = _context.Fields.ToList(); // الحصول على الحقول المتاحة
            var viewModel = new TemplateViewModel
            {
                AvailableFields = availableFields,
                SelectedFields = new List<int>() // تحديد الحقول المختارة كبداية
            };
            return View(viewModel);
        }

        // حفظ القالب
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTemplate(TemplateViewModel model)
        {
            if (model.SelectedFields == null || !model.SelectedFields.Any())
            {
                ModelState.AddModelError("", "يجب اختيار الحقول");
                var availableFields = _context.Fields.ToList();
                model.AvailableFields = availableFields;
                return View("Create", model); // إذا لم يتم اختيار الحقول، نعرض رسالة خطأ
            }

            // إنشاء قالب جديد وربط الحقول المختارة
            var template = new Template
            {
                ArabicName = model.ArabicName, // استخدام ArabicName
                EnglishName = model.EnglishName, // استخدام EnglishName
                Description = model.Description, // استخدام الوصف
                TemplateType = model.TemplateType, // نوع القالب
                IsActive = model.IsActive, // حالة القالب
                ShowNameWithValue = model.ShowNameWithValue, // إظهار الاسم مع القيمة
                ShowBorderAroundField = model.ShowBorderAroundField, // إظهار الإطار حول الحقل
                DesignType = model.DesignType, // نوع التصميم إذا كان مطلوبة
                Fields = _context.Fields
                    .Where(f => model.SelectedFields.Contains(f.Id)) // اختيار الحقول
                    .AsEnumerable() // تحويل النتائج إلى ذاكرة
                    .OrderBy(f => model.SelectedFields.IndexOf(f.Id)) // ترتيب الحقول باستخدام IndexOf
                    .ToList() // تحويل النتيجة إلى قائمة
            };

            _context.Templates.Add(template); // إضافة القالب إلى قاعدة البيانات
            _context.SaveChanges(); // حفظ التغييرات

            return RedirectToAction(nameof(Index)); // إعادة التوجيه إلى صفحة القوالب
        }

        // عرض جميع القوالب
public IActionResult Index()
{
    var templates = _context.Templates
        .Select(t => new TemplateViewModel
        {
            ArabicName = t.ArabicName ?? string.Empty,
            EnglishName = t.EnglishName ?? string.Empty,
            Description = t.Description ?? string.Empty,
            TemplateType = t.TemplateType ?? string.Empty,
            IsActive = t.IsActive,
            ShowNameWithValue = t.ShowNameWithValue,
            ShowBorderAroundField = t.ShowBorderAroundField,
            DesignType = t.DesignType ?? string.Empty
        })
        .ToList();

    // تمرير البيانات إلى الـ View
    return View(templates);
}


    }
}

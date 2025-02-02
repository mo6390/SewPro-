using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;  // إضافة الـ using
using Microsoft.AspNetCore.Mvc.Rendering;  // إضافة الـ using المناسب


namespace SewPro.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض نموذج إضافة طريقة الدفع
        public IActionResult Create()
        {
            return View();
        }

        // معالجة بيانات النموذج بعد تقديمه
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                // إضافة طريقة الدفع إلى قاعدة البيانات
                _context.PaymentMethods.Add(paymentMethod);
                _context.SaveChanges();

                // إعادة التوجيه إلى صفحة عرض جميع طرق الدفع أو صفحة أخرى حسب الحاجة
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod); // إعادة عرض الصفحة في حالة وجود أخطاء في التحقق
        }

        // عرض جميع طرق الدفع (يمكنك إضافة هذا للحصول على طرق الدفع في التطبيق)
        public IActionResult Index()
        {
            var paymentMethods = _context.PaymentMethods.ToList();
            return View(paymentMethods);
        }
   
   // دالة عرض تأكيد الحذف
public IActionResult Delete(int id)
{
    var paymentMethod = _context.PaymentMethods.Find(id);
    if (paymentMethod == null)
    {
        return NotFound(); // في حال عدم العثور على طريقة الدفع
    }
    return View(paymentMethod); // عرض صفحة تأكيد الحذف
}

// دالة معالجة الحذف
// دالة معالجة الحذف
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    // البحث عن طريقة الدفع باستخدام المعرف (id)
    var paymentMethod = _context.PaymentMethods.Find(id);
    
    // إذا كانت طريقة الدفع موجودة
    if (paymentMethod != null)
    {
        _context.PaymentMethods.Remove(paymentMethod); // إزالة السجل من مجموعة PaymentMethods
        _context.SaveChanges(); // حفظ التغييرات في قاعدة البيانات
    }

    // إعادة التوجيه إلى صفحة العرض (Index)
    return RedirectToAction(nameof(Index)); 
}


// دالة عرض النموذج لتعديل طريقة الدفع
public IActionResult Edit(int id)
{
    var paymentMethod = _context.PaymentMethods.Find(id);
    if (paymentMethod == null)
    {
        return NotFound(); // إذا لم يتم العثور على طريقة الدفع
    }
    return View(paymentMethod); // عرض النموذج لتعديل البيانات
}

// دالة معالجة التعديل
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, PaymentMethod paymentMethod)
{
    if (id != paymentMethod.Id)
    {
        return NotFound(); // إذا كان المعرف لا يتطابق
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(paymentMethod); // تحديث طريقة الدفع في قاعدة البيانات
            _context.SaveChanges(); // حفظ التغييرات
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PaymentMethods.Any(pm => pm.Id == id))
            {
                return NotFound(); // إذا تم تعديل طريقة الدفع في مكان آخر
            }
            else
            {
                throw; // إذا حدث خطأ غير متوقع
            }
        }
        return RedirectToAction(nameof(Index)); // إعادة التوجيه بعد التعديل
    }
    return View(paymentMethod); // في حال وجود أخطاء في التحقق من النموذج
}

   }
}

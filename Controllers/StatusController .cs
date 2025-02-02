using Microsoft.AspNetCore.Mvc;
using SewPro.Models;  // تأكد من استيراد الموديل الذي يحتوي على Status و StatusViewModel
using SewPro.Data;    // تأكد من استيراد ApplicationDbContext

namespace SewPro.Controllers
{
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Status
        public IActionResult Index()
        {
            var statuses = _context.Statuses.ToList();  // جلب البيانات من قاعدة البيانات
            var model = statuses.Select(s => new StatusViewModel
            {
                Id = s.Id,
                ArabicName = s.ArabicName,
                EnglishName = s.EnglishName,
                StatusType = s.StatusType,  // تعديل من Status إلى StatusType
                SmsMessage = s.SmsMessage,
                Notes = s.Notes,
                CustomerName = s.CustomerName,
                BranchName = s.BranchName,
                TransCode = s.TransCode
            }).ToList();

            return View(model);  // إرسال البيانات إلى الفيو
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            var model = new StatusViewModel
            {
                CustomerName = "أحمد علي",  // يمكن استبدالها بقيم فعلية من قاعدة البيانات
                BranchName = "الفرع الرئيسي",  // نفس الشيء
                TransCode = "12345",  // رمز الإذن الفعلي
                SmsMessage = "عزيزي العميل @customer_name تم استلام طلبكم وسيتم العمل عليه في أسرع وقت ممكن @branch_name اسم العميل: @customer_name - الفرع: @branch_name - رقم الإذن: @trans_code",
                Notes = ""  // يمكن إضافة ملاحظات من المستخدم
            };

            return View(model);
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                // قم بتنفيذ منطق حفظ البيانات في قاعدة البيانات
                var status = new Status
                {
                    ArabicName = model.ArabicName,
                    EnglishName = model.EnglishName,
                    StatusType = model.StatusType,  // استخدام StatusType
                    SmsMessage = model.SmsMessage,
                    Notes = model.Notes,
                    CustomerName = model.CustomerName,
                    BranchName = model.BranchName,
                    TransCode = model.TransCode
                };

                // إضافة الكائن إلى قاعدة البيانات
                _context.Statuses.Add(status);
                _context.SaveChanges();

                // يمكنك إضافة منطق إضافي هنا حسب الحاجة

                return RedirectToAction("Index");  // إعادة توجيه إلى صفحة الفهرس بعد الحفظ
            }

            return View(model);  // في حال لم يكن النموذج صالحًا، إرجاعه مع الرسائل
        }

        // GET: Status/Edit/5
        public IActionResult Edit(int id)
        {
            var status = _context.Statuses.Find(id);
            if (status == null)
            {
                return NotFound();
            }

            var model = new StatusViewModel
            {
                Id = status.Id,
                ArabicName = status.ArabicName,
                EnglishName = status.EnglishName,
                StatusType = status.StatusType,  // تعديل من Status إلى StatusType
                SmsMessage = status.SmsMessage,
                Notes = status.Notes,
                CustomerName = status.CustomerName,
                BranchName = status.BranchName,
                TransCode = status.TransCode
            };

            return View(model);
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, StatusViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var status = _context.Statuses.Find(id);
                    if (status != null)
                    {
                        status.ArabicName = model.ArabicName;
                        status.EnglishName = model.EnglishName;
                        status.StatusType = model.StatusType;  // تعديل من Status إلى StatusType
                        status.SmsMessage = model.SmsMessage;
                        status.Notes = model.Notes;
                        status.CustomerName = model.CustomerName;
                        status.BranchName = model.BranchName;
                        status.TransCode = model.TransCode;

                        _context.Update(status);
                        _context.SaveChanges();
                    }
                }
                catch
                {
                    return View(model);  // إرجاع النموذج في حال فشل التحديث
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Status/Delete/5
        public IActionResult Delete(int id)
        {
            var status = _context.Statuses.Find(id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var status = _context.Statuses.Find(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

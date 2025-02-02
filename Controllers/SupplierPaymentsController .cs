using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SewPro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using SewPro.Data;  // إضافة الـ namespace الخاص بـ ApplicationDbContext

namespace SewPro.Controllers
{
    public class SupplierPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SupplierPayments
        public async Task<IActionResult> Index()
        {
            // Retrieve all payments along with the associated supplier
            var payments = await _context.SupplierPayments
                                         .Include(sp => sp.Supplier) // تضمين المورد
                                         .ToListAsync();
            return View(payments);  // تمرير المدفوعات إلى الـ View
        }

        // GET: SupplierPayments/Create
        public IActionResult Create()
        {
            // تحضير قائمة الموردين للربط بالقائمة المنسدلة
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }

        // POST: SupplierPayments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierId,AmountDue,AmountPaid,DueDate,PaymentDate")] SupplierPayment payment)
        {
            if (ModelState.IsValid)
            {
                // إضافة المدفوعات إلى قاعدة البيانات
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // إعادة التوجيه إلى صفحة الـ Index بعد الحفظ
            }
            // في حال وجود خطأ في البيانات، إعادة تحميل القائمة المنسدلة
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", payment.SupplierId);
            return View(payment);
        }

        // GET: SupplierPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.SupplierPayments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            // تحضير قائمة الموردين لتعديلها
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", payment.SupplierId);
            return View(payment);
        }

        // POST: SupplierPayments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierId,AmountDue,AmountPaid,DueDate,PaymentDate")] SupplierPayment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierPaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // في حال وجود خطأ في البيانات، إعادة تحميل القائمة المنسدلة
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", payment.SupplierId);
            return View(payment);
        }

        // GET: SupplierPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.SupplierPayments
                .Include(sp => sp.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: SupplierPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.SupplierPayments.FindAsync(id);
            _context.SupplierPayments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierPaymentExists(int id)
        {
            return _context.SupplierPayments.Any(e => e.Id == id);
        }
    }
}

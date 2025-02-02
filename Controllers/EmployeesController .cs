using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SewPro.Data;
using SewPro.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SewPro.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🟢 عرض جميع الموظفين
        public async Task<IActionResult> Index(string search)
        {
            var employees = from e in _context.Employees select e;

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(e => e.Name.Contains(search) || e.Email.Contains(search));
            }

            return View(await employees.ToListAsync());
        }

        // 🟢 عرض تفاصيل موظف
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // 🟢 إنشاء موظف جديد (عرض النموذج)
        public IActionResult Create()
        {
            return View();
        }

        // 🟢 إنشاء موظف جديد (حفظ البيانات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // 🟢 تعديل بيانات الموظف (عرض النموذج)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // 🟢 تعديل بيانات الموظف (حفظ التعديلات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // 🟢 حذف موظف (عرض التأكيد)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
    var employee = await _context.Employees.FindAsync(id);
    if (employee == null)
    {
        return NotFound();
    }

    _context.Employees.Remove(employee);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index)); // التوجيه مباشرة إلى القائمة بعد الحذف
}


        // 🟢 التحقق من وجود موظف
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        // 🟢 تصدير بيانات الموظفين إلى ملف Excel
        public async Task<IActionResult> ExportToExcel()
        {
            var employees = await _context.Employees.ToListAsync();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");
                worksheet.Cells.LoadFromCollection(employees, true);
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
            }
        }

        // 🟢 تصدير بيانات الموظفين إلى ملف PDF
        public async Task<IActionResult> ExportToPDF()
        {
            var employees = await _context.Employees.ToListAsync();
            using (var stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();
                document.Add(new Paragraph("Employee List"));
                foreach (var emp in employees)
                {
                    document.Add(new Paragraph($"{emp.EmployeeId} - {emp.Name} - {emp.Email} - {emp.JobTitle}"));
                }
                document.Close();
                return File(stream.ToArray(), "application/pdf", "Employees.pdf");
            }
        }
    }
}

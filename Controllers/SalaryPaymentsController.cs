using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SewPro.Models;
using SewPro.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;

namespace SewPro.Controllers
{
    public class SalaryPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SalaryPaymentsController> _logger;

        public SalaryPaymentsController(ApplicationDbContext context, ILogger<SalaryPaymentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // عرض قائمة دفع الرواتب
        public async Task<IActionResult> Index()
        {
            var salaryPayments = await _context.SalaryPayments
                .Include(sp => sp.Employee)
                .ToListAsync();

            return View(salaryPayments);
        }

        // عرض تفاصيل دفع راتب محدد
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaryPayment = await _context.SalaryPayments
                .Include(sp => sp.Employee)
                .FirstOrDefaultAsync(m => m.PaymentId == id);

            if (salaryPayment == null)
            {
                return NotFound();
            }

            return View(salaryPayment);
        }

        // عرض نموذج إنشاء دفع راتب
        [HttpGet]
        public IActionResult Create()
        {
            // تعبئة القائمة المنسدلة للموظفين
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            return View();
        }

        // إنشاء دفع راتب (POST)
      // إضافة دفعة راتب جديدة (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentDate,GrossSalary,Deductions,Bonus,PaymentMethod,PaymentStatus,EmployeeId")] SalaryPayment salaryPayment)
        {
            _logger.LogInformation($"EmployeeId: {salaryPayment.EmployeeId}"); // طباعة قيمة EmployeeId

            if (ModelState.IsValid)
            {
                try
                {
                    salaryPayment.NetSalary = salaryPayment.GrossSalary - salaryPayment.Deductions;
                    salaryPayment.TotalPayment = salaryPayment.NetSalary + salaryPayment.Bonus;

                    _context.Add(salaryPayment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred while creating salary payment: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving the salary payment. Please try again.");
                }
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"Validation Error: {error.ErrorMessage}");
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", salaryPayment.EmployeeId);
            return View(salaryPayment);
        }


// عرض تقرير دفع الرواتب
public IActionResult GenerateReport(DateTime? startDate, DateTime? endDate, string search)
{
    var reportData = _context.SalaryPayments
                             .Include(sp => sp.Employee)
                             .Where(sp => (startDate == null || sp.PaymentDate >= startDate) && 
                                          (endDate == null || sp.PaymentDate <= endDate) &&
                                          (string.IsNullOrEmpty(search) || sp.Employee.Name.Contains(search)))
                             .ToList();

    ViewData["Search"] = search;  // تمرير البحث إلى الفيو

    return View(reportData);
}


public IActionResult ExportToPdf(DateTime? startDate, DateTime? endDate, string search)
{
    var reportData = _context.SalaryPayments
                             .Include(sp => sp.Employee)
                             .Where(sp => (startDate == null || sp.PaymentDate >= startDate) && 
                                          (endDate == null || sp.PaymentDate <= endDate) &&
                                          (string.IsNullOrEmpty(search) || sp.Employee.Name.Contains(search)))
                             .ToList();

    using (MemoryStream stream = new MemoryStream())
    {
        Document document = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(document, stream);
        document.Open();

        // عنوان التقرير
        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
        Paragraph title = new Paragraph("تقرير دفع الرواتب", titleFont)
        {
            Alignment = Element.ALIGN_CENTER
        };
        document.Add(title);

        document.Add(new Paragraph(" ")); // سطر فارغ

        // إنشاء جدول يحتوي على 7 أعمدة
        PdfPTable table = new PdfPTable(7)
        {
            WidthPercentage = 100
        };
        table.SetWidths(new float[] { 2, 2, 2, 2, 2, 2, 2 });

        // إضافة العناوين إلى الجدول
        string[] headers = { "الموظف", "تاريخ الدفع", "الراتب الإجمالي", "الخصومات", "الراتب الصافي", "المكافأة", "إجمالي الدفع" };
        foreach (string header in headers)
        {
            PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
            {
                BackgroundColor = new BaseColor(200, 200, 200),
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
        }

        // إضافة البيانات إلى الجدول
        foreach (var item in reportData)
        {
            table.AddCell(new Phrase(item.Employee.Name));
            table.AddCell(new Phrase(item.PaymentDate.ToString("dd/MM/yyyy")));
            table.AddCell(new Phrase(item.GrossSalary.ToString()));
            table.AddCell(new Phrase(item.Deductions.ToString()));
            table.AddCell(new Phrase(item.NetSalary.ToString()));
            table.AddCell(new Phrase(item.Bonus.ToString()));
            table.AddCell(new Phrase(item.TotalPayment.ToString()));
        }

        document.Add(table);
        document.Close();

        return File(stream.ToArray(), "application/pdf", "SalaryReport.pdf");
    }
}


        // جلب الراتب الأساسي للموظف (AJAX)
        [HttpGet]
        public IActionResult GetEmployeeSalary(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee != null)
            {
                return Json(new { grossSalary = employee.Salary });
            }

            return Json(new { grossSalary = 0 });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SewPro.Data;
using SewPro.Models;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;

namespace SewPro.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

 public IActionResult Index(string searchQuery, int page = 1)
{
    int pageSize = 50; // Number of customers per page

    // Filter customers based on search query
    IQueryable<Customer> customersQuery = _context.Customers.OrderBy(c => c.Name);

    if (!string.IsNullOrEmpty(searchQuery))
    {
        customersQuery = customersQuery.Where(c =>
            c.Name.Contains(searchQuery) || 
            c.MobileNumber.Contains(searchQuery) || 
            c.CustomerCode.Contains(searchQuery));
    }

    // Calculate total number of customers and the number of pages
    var totalCustomers = customersQuery.Count();
    var totalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);

    // Fetch customers for the current page
    var customers = customersQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    // Create a pagination model
    var viewModel = new CustomerIndexViewModel
    {
        Customers = customers,
        CurrentPage = page,
        TotalPages = totalPages,
        SearchQuery = searchQuery
    };

    return View(viewModel);
}

public IActionResult Create()
{
    return View();
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Customer customer)
{
    if (!ModelState.IsValid)
    {
        return View(customer);
    }

    // تحقق من وجود العميل بنفس رقم الجوال
    var existingCustomer = _context.Customers.FirstOrDefault(c => c.MobileNumber == customer.MobileNumber);
    if (existingCustomer != null)
    {
        // إذا كان الاسم مختلفًا ولكن رقم الجوال نفسه، نعيد نفس كود العميل
        customer.CustomerCode = existingCustomer.CustomerCode;
    }
    else
    {
        // تعيين كود العميل الجديد بدون حروف أو رموز، كرقم تسلسلي يبدأ من 1
        var lastCustomer = _context.Customers.OrderByDescending(c => c.CustomerId).FirstOrDefault();
        int newCustomerCode = lastCustomer != null ? lastCustomer.CustomerId + 1 : 1;
        customer.CustomerCode = newCustomerCode.ToString();  // كود العميل فقط رقم
    }

    // تعيين التاريخ الحالي للـ CreatedAt
    customer.CreatedAt = DateTime.Now;

    // إضافة العميل إلى قاعدة البيانات
    _context.Customers.Add(customer);
    _context.SaveChanges();

    return RedirectToAction("Index");
}



        // عرض تفاصيل العميل
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // تعديل بيانات العميل
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _context.Customers.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // حذف العميل
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // تصدير إلى Excel
        public IActionResult ExportToExcel()
        {
            var customers = _context.Customers.OrderBy(c => c.Name).ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Customers");

                // Add header row
                worksheet.Cells[1, 1].Value = "Customer Code";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Mobile Number";

                // Add customer data
                for (int i = 0; i < customers.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = customers[i].CustomerCode;
                    worksheet.Cells[i + 2, 2].Value = customers[i].Name;
                    worksheet.Cells[i + 2, 3].Value = customers[i].MobileNumber;
                }

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
            }
        }

        // تصدير إلى PDF
        public IActionResult ExportToPdf()
        {
            var customers = _context.Customers.OrderBy(c => c.Name).ToList();

            var document = new Document();
            using (var stream = new MemoryStream())
            {
                PdfWriter.GetInstance(document, stream);

                document.Open();
                document.Add(new Paragraph("Customer List"));

                var table = new PdfPTable(3); // عدد الأعمدة
                table.AddCell("Customer Code");
                table.AddCell("Name");
                table.AddCell("Mobile Number");

                foreach (var customer in customers)
                {
                    table.AddCell(customer.CustomerCode);
                    table.AddCell(customer.Name);
                    table.AddCell(customer.MobileNumber);
                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "Customers.pdf");
            }
        }

        // استيراد من Excel
 // عرض صفحة استيراد إكسيل
public IActionResult ImportExcelPage()
{
    return View();
}

// استيراد من Excel
[HttpPost]
public IActionResult ImportExcel(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        ModelState.AddModelError("", "الرجاء تحميل ملف Excel.");
        return RedirectToAction("ImportExcelPage");
    }

    using (var stream = new MemoryStream())
    {
        file.CopyTo(stream);
        using (var package = new ExcelPackage(stream))
        {
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // تخطي الصف الأول (العناوين)
            {
                var customer = new Customer
                {
                    CustomerCode = worksheet.Cells[row, 1].Text,
                    Name = worksheet.Cells[row, 2].Text,
                    MobileNumber = worksheet.Cells[row, 3].Text
                };

                _context.Customers.Add(customer);
            }

            _context.SaveChanges();
        }
    }

    return RedirectToAction("Index");
}

    }
}
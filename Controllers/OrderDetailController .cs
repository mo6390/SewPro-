using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using System.Linq;

namespace SewPro.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض صفحة أمر تفصيل
        public IActionResult Index()
        {
            // إحضار قائمة البائعين لتحديد البائع عند إضافة تفاصيل الطلب
            ViewBag.Sellers = _context.Sellers.ToList();
            return View();
        }

        // استعلام العملاء
        [HttpGet]
        public IActionResult SearchCustomer(string searchQuery)
        {
            var customers = _context.Customers
                .Where(c => c.Name.Contains(searchQuery) || c.MobileNumber.Contains(searchQuery) || c.CustomerCode.Contains(searchQuery))
                .Select(c => new
                {
                    c.CustomerId,
                    c.Name,
                    c.MobileNumber,
                    c.CustomerCode
                })
                .ToList();

            return Json(customers);
        }

        // إضافة عميل جديد
        [HttpPost]
        public IActionResult AddCustomer(string name, string mobileNumber)
        {
            // توليد كود العميل بناءً على آخر كود عميل موجود في قاعدة البيانات
            var lastCustomer = _context.Customers
                .OrderByDescending(c => c.CustomerCode) // ترتيب حسب الكود تنازلياً للحصول على أكبر كود
                .FirstOrDefault();

            // إذا كانت قاعدة البيانات فارغة، يبدأ من 1، وإلا يتم إضافة 1 إلى أكبر كود موجود
            string customerCode = (lastCustomer != null ? (int.Parse(lastCustomer.CustomerCode) + 1).ToString() : "1");

            var newCustomer = new Customer
            {
                Name = name,
                MobileNumber = mobileNumber,
                CustomerCode = customerCode // تعيين الكود الجديد
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            return Json(new { success = true, message = "تم إضافة العميل بنجاح!" });
        }
        [HttpPost]
public IActionResult AddOrderDetail(int customerId, int sellerId, DateTime? receiptDate, DateTime? deliveryDate)
{
    var orderDetail = new OrderDetail
    {
        CustomerId = customerId,
        SellerId = sellerId,
        ReceiptDate = receiptDate,
        DeliveryDate = deliveryDate
    };

    _context.OrderDetails.Add(orderDetail);
    _context.SaveChanges();

    return Json(new { success = true, message = "تم إضافة تفاصيل الطلب بنجاح!" });
}


        // استعلام البائعين (اختياري: لتوفير بيانات للبائعين في صفحة الفاتورة)
        [HttpGet]
        public IActionResult GetSellers()
        {
            var sellers = _context.Sellers
                .Select(s => new
                {
                    s.SellerId,
                    s.Name
                })
                .ToList();

            return Json(sellers);
        }
    }
}

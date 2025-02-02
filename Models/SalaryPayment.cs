using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
   public class SalaryPayment
{
    public int PaymentId { get; set; }
    
    [Required(ErrorMessage = "يجب تحديد الموظف")]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; } // علاقة مع الموظف

    public DateTime PaymentDate { get; set; }
    public decimal GrossSalary { get; set; } // الراتب الإجمالي قبل الخصومات
    public decimal Deductions { get; set; } // الخصومات (ضرائب، تأمينات...)
    public decimal NetSalary { get; set; } // الراتب الصافي بعد الخصومات
    public decimal Bonus { get; set; } // المكافآت
    public decimal TotalPayment { get; set; } // إجمالي المدفوع بعد إضافة المكافآت وخصم الضرائب
    public string PaymentMethod { get; set; } // طريقة الدفع (تحويل بنكي، نقدًا)
    public string PaymentStatus { get; set; } // حالة الدفع (مدفوع، معلق...)
}

}

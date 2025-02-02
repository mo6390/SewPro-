namespace SewPro.Models
{
    public class SalaryPaymentSummary
    {
            public int SummaryId { get; set; }  // المفتاح الرئيسي

        public DateTime PaymentDate { get; set; }
        public decimal TotalSalary { get; set; } // إجمالي الرواتب المدفوعة
        public decimal TotalDeductions { get; set; } // إجمالي الخصومات
        public decimal TotalBonus { get; set; } // إجمالي المكافآت
        public decimal TotalPayment { get; set; } // إجمالي المدفوعات
    }
}

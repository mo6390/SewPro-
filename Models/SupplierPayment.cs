using System;
using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class SupplierPayment
    {
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; } // علاقة مع المورد
        public Supplier Supplier { get; set; } // المورد المرتبط بالمدفوعات

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "يجب أن يكون المبلغ المستحق أكبر من صفر")]
        public decimal AmountDue { get; set; }  // المبلغ المستحق

        [Range(0, double.MaxValue, ErrorMessage = "يجب أن يكون المبلغ المدفوع أكبر من صفر")]
        public decimal AmountPaid { get; set; }  // المبلغ المدفوع

        [Required]
        public DateTime DueDate { get; set; }  // تاريخ الاستحقاق

        public DateTime? PaymentDate { get; set; }  // تاريخ الدفع (إذا تم الدفع)

        // حساب الرصيد المتبقي
        public decimal RemainingAmount => AmountDue - AmountPaid;
    }
}

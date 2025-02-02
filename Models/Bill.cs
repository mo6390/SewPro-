using System;
using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Bill
    {
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; } // علاقة مع المورد
        public Supplier Supplier { get; set; } // المورد المرتبط بالفاتورة

        [Required]
        public DateTime IssueDate { get; set; }  // تاريخ الإصدار

        [Required]
        public DateTime DueDate { get; set; }  // تاريخ الاستحقاق

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "يجب أن يكون المبلغ أكبر من صفر")]
        public decimal Amount { get; set; }  // المبلغ

        // يمكنك إضافة خصائص أخرى إذا لزم الأمر
    }
}

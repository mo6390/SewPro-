using System;
using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class SleeveShape
    {
        public int Id { get; set; }  // المعرف (رقم فريد)
        
        [Required]  // تأكد من أن اسم شكل الكم إلزامي
        [MaxLength(100)]  // تحديد الحد الأقصى للطول
        public string Name { get; set; }  // اسم شكل الكم
        
        public DateTime CreatedAt { get; set; }  // تاريخ الإنشاء
        public DateTime UpdatedAt { get; set; }  // تاريخ التحديث
    }
}

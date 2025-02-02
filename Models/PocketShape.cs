using System;

namespace SewPro.Models
{
    public class PocketShape
    {
        public int Id { get; set; }  // المعرف (رقم فريد)
        public string Name { get; set; }  // اسم شكل الجيب
        
        // تأكد من إضافة هذه الخصائص
        public DateTime CreatedAt { get; set; }  // تاريخ الإنشاء
        public DateTime UpdatedAt { get; set; }  // تاريخ التحديث
    }
}

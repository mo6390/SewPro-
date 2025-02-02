using System.ComponentModel.DataAnnotations;
using SewPro.Models;

// Models/Store.cs
namespace SewPro.Models
{
    public class Store
    {
        public int Id { get; set; }  // معرف المخزن
        public string Name { get; set; }  // اسم المخزن
        public string Location { get; set; }  // موقع المخزن
        public string Description { get; set; }  // وصف المخزن
        public DateTime CreatedAt { get; set; }  // تاريخ الإنشاء
        public DateTime UpdatedAt { get; set; }  // تاريخ التحديث
    }
}

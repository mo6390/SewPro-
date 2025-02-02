using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }  // رقم الجوال (إلزامي)

        [EmailAddress]
        public string Email { get; set; }  // اختياري

        public string Address { get; set; }  // اختياري

        [StringLength(20)]
        public string TaxNumber { get; set; }  // اختياري

        public string Notes { get; set; }  // اختياري

        // إضافة الخاصية IsActive
        public bool IsActive { get; set; }  // هذه هي الخاصية المطلوبة
    }
}

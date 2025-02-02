using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewPro.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }

        [Display(Name = "الاسم الإنجليزي")]
        public string? EnglishName { get; set; }

        [Required]
        [Display(Name = "مجموعة الصنف")]
        public int ItemGroupId { get; set; }

        [ForeignKey("ItemGroupId")]
        public ItemGroup? ItemGroup { get; set; }

        [Required]
        [Display(Name = "سعر البيع")]
        [Range(0, double.MaxValue, ErrorMessage = "سعر البيع يجب أن يكون أكبر من 0")]
        public decimal Price { get; set; }

        [Display(Name = "الباركود")]
        public string? Barcode { get; set; }

        [Display(Name = "الصورة")]
        public string? ImagePath { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
         [NotMapped]  // هذا يعني أن هذه الخاصية لن تخزن في قاعدة البيانات
    public IFormFile? ImageFile { get; set; }  // خاصية رفع الملف
    }
}

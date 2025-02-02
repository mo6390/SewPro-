using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Field
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم بالعربية مطلوب")]
        [Display(Name = "الاسم بالعربية")]
        public string ArabicName { get; set; } // الاسم بالعربية للحقل

        [Required(ErrorMessage = "الاسم بالإنجليزية مطلوب")]
        [Display(Name = "الاسم بالإنجليزية")]
        public string EnglishName { get; set; } // الاسم بالإنجليزية للحقل

        [Required(ErrorMessage = "نوع الحقل مطلوب")]
        [Display(Name = "نوع الحقل")]
        public string FieldType { get; set; } // نوع الحقل (نص، رقم، اختيار، إلخ)

        [Display(Name = "هل الحقل مطلوب؟")]
        public bool IsRequired { get; set; } // هل الحقل مطلوب؟

        [Display(Name = "هل يحتوي على أرقام فقط؟")]
        public bool NumbersOnly { get; set; } // هل يحتوي الحقل على أرقام فقط؟

        [Display(Name = "ملاحظات")]
        public string Notes { get; set; } // ملاحظات إضافية عن الحقل
    }
}

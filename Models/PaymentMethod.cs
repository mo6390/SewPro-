using System.ComponentModel.DataAnnotations;


namespace SewPro.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        // اسم طريقة الدفع بالعربية (إجباري)
        [Required(ErrorMessage = "الاسم العربي هو حقل مطلوب")]
        public string NameAr { get; set; }

        // اسم طريقة الدفع بالإنجليزية (اختياري)
        public string NameEn { get; set; }

        // ملاحظات (اختياري)
        public string Notes { get; set; }
    }
}

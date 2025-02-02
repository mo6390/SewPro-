using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم العربي مطلوب")]
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }

        [Required(ErrorMessage = "الاسم الإنجليزي مطلوب")]
        [Display(Name = "الاسم الإنجليزي")]
        public string EnglishName { get; set; }

        [Display(Name = "ملاحظات")]
        public string Notes { get; set; }
    }
}

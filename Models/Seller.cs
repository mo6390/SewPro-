using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Seller
{
    public int SellerId { get; set; }

    [Required(ErrorMessage = "الاسم هو حقل إجبارى.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "رقم الجوال هو حقل إجبارى.")]
    public string PhoneNumber { get; set; }

    // الحقل الذي لا يحتوي على [Required] يعتبر اختياريا
    public string? JobTitle { get; set; }

    public string? EnglishName { get; set; }

    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح.")]
    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Nationality { get; set; }
}

}

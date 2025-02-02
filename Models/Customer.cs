using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    /// <summary>
    /// نموذج بيانات العميل.
    /// </summary>
public class Customer
{
    [Key]
    public int CustomerId { get; set; } // كود العميل (الرقم التسلسلي)

    [Required(ErrorMessage = "اسم العميل مطلوب")]
    [StringLength(100, ErrorMessage = "اسم العميل يجب أن لا يزيد عن 100 حرف")]
    public string Name { get; set; } // اسم العميل

    [Required(ErrorMessage = "رقم الجوال مطلوب")]
    [Phone(ErrorMessage = "رقم الجوال غير صحيح")]
    public string MobileNumber { get; set; } // رقم الجوال

    // إزالة [Required] لأن الكود يتم توليده تلقائيًا
    public string? CustomerCode { get; set; } // رمز العميل (يولد تلقائيًا بناءً على الرقم التسلسلي)

    [StringLength(20, ErrorMessage = "رقم الهوية يجب أن لا يزيد عن 20 حرف")]
    public string? NationalId { get; set; } // رقم الهوية

    [StringLength(50, ErrorMessage = "السجل التجاري يجب أن لا يزيد عن 50 حرف")]
    public string? CommercialRegistration { get; set; } // السجل التجاري

    [StringLength(200, ErrorMessage = "العنوان يجب أن لا يزيد عن 200 حرف")]
    public string? Address { get; set; } // العنوان

    [StringLength(50, ErrorMessage = "اسم المدينة يجب أن لا يزيد عن 50 حرف")]
    public string? City { get; set; } // المدينة
    public DateTime CreatedAt { get; set; } // إضافة هذه الخاصية

    public DateTime? RegistrationDate { get; set; } // أو أي اسم آخر للحقل


}
    
    


}

using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
   public class Industry
{
    public int Id { get; set; }

    public string ArabicName { get; set; }

    public string EnglishName { get; set; }

    public string? Notes { get; set; } // حقل اختياري
}

}

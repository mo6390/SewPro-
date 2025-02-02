
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SewPro.Models
{
public class ItemGroup
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "الاسم العربي")]
    public string ArabicName { get; set; }

    [Required]
    [Display(Name = "الاسم الإنجليزي")]
    public string EnglishName { get; set; }

    [Display(Name = "ملاحظات")]
    public string? Notes { get; set; }

    // علاقة One-to-Many مع Item
public ICollection<Item> Items { get; set; } = new List<Item>();
}




}

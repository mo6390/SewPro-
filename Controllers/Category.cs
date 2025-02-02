using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    public required string Name { get; set; } // تم إضافة required

    public string? Description { get; set; }

}

}

using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
   public class CustomerIndexViewModel
{
    public List<Customer> Customers { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string SearchQuery { get; set; }
}

}

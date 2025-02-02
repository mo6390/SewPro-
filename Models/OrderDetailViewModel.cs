using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class OrderDetailViewModel
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerMobileNumber { get; set; }
    public string CustomerCode { get; set; }
    public int SellerId { get; set; }
    public string SellerName { get; set; }
            public DateTime? ReceiptDate { get; set; }  // تاريخ الاستلام
    public DateTime? DeliveryDate { get; set; } // تاريخ التسليم
        
        
        
        
        
        
        
}

}

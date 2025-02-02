using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class OrderDetail
{
                public int OrderDetailId { get; set; }

      public int CustomerId { get; set; }

    public int SellerId { get; set; }

            public DateTime? ReceiptDate { get; set; }  // تاريخ الاستلام
    public DateTime? DeliveryDate { get; set; } // تاريخ التسليم
        
        
        
        
        
        
        
}

}

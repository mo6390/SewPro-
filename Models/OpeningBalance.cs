namespace SewPro.Models
{
    public class OpeningBalance
    {
        public int Id { get; set; }
        public decimal CashBalance { get; set; }
        public decimal BankBalance { get; set; }
        public decimal CustomerReceivables { get; set; }
        public decimal SupplierPayables { get; set; }
        public DateTime Date { get; set; }
    }
}

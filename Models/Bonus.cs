namespace SewPro.Models
{
    public class Bonus
    {
        public int BonusId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string BonusType { get; set; } // نوع المكافأة (أداء، عيد، إلخ)
        public decimal Amount { get; set; } // قيمة المكافأة
        public DateTime BonusDate { get; set; } // تاريخ المكافأة
    }
}

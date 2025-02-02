namespace SewPro.Models
{
    public class SalaryDeduction
    {
            public int Id { get; set; }

        public int DeductionId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string DeductionType { get; set; } // نوع الخصم (ضرائب، تأمينات، قرض)
        public decimal Amount { get; set; } // المبلغ المخصوم
        public DateTime DeductionDate { get; set; } // تاريخ الخصم
    }
}

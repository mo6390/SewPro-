namespace SewPro.Models
{
    public class StatusViewModel
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string StatusType { get; set; } // هذه هي الخاصية التي كنت تبحث عنها
        public string SmsMessage { get; set; }
        public string Notes { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string TransCode { get; set; }
    }
}

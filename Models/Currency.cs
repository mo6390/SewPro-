namespace SewPro.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string MainCurrencyArabic { get; set; }
        public string MainCurrencyEnglish { get; set; }
        public string SubCurrencyArabic { get; set; }
        public string SubCurrencyEnglish { get; set; }
        public int SubCurrencyDecimal { get; set; }
        public string NationalityArabic { get; set; }
        public string NationalityEnglish { get; set; }
        public string Notes { get; set; }
    }
}

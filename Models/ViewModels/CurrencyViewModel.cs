namespace SewPro.Models
{
    public class CurrencyViewModel
    {
        public int Id { get; set; }  // معرف العملة
        public string MainCurrencyArabic { get; set; }  // العملة الرئيسية بالعربي
        public string MainCurrencyEnglish { get; set; }  // العملة الرئيسية بالإنجليزي
        public string SubCurrencyArabic { get; set; }  // العملة الفرعية بالعربي
        public string SubCurrencyEnglish { get; set; }  // العملة الفرعية بالإنجليزي
        public int SubCurrencyDecimal { get; set; }  // الكسور العشرية للعملة الفرعية
        public string NationalityArabic { get; set; }  // الجنسية بالعربي
        public string NationalityEnglish { get; set; }  // الجنسية بالإنجليزي
        public string Notes { get; set; }  // ملاحظات إضافية
    }
}

namespace SewPro.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }  // الاسم العربي
        public string EnglishName { get; set; }  // الاسم الإنجليزي
        public string StatusType { get; set; }  // حالة الثوب (مثل "تحت العمل"، "جاهز" أو "تعديل")
        public string SmsMessage { get; set; }  // رسالة SMS
        public string Notes { get; set; }  // ملاحظات إضافية
        public string CustomerName { get; set; }  // اسم العميل
        public string BranchName { get; set; }  // اسم الفرع
        public string TransCode { get; set; }  // رمز الإذن
    }
}

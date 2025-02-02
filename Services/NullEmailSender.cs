using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace SewPro.Services  // تأكد من أن النيمسبيس يتوافق مع مشروعك
{
    public class NullEmailSender : IEmailSender
    {
        // هذه هي الدالة التي ستقوم بتنفيذ مهمة إرسال البريد الإلكتروني، ولكنها لن تفعل شيء هنا.
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // لا تفعل شيئًا هنا، فقط ارجع إلى Task.CompletedTask، مما يعني أن العملية تمت بنجاح ولكن لا شيء فعليًا تم
            return Task.CompletedTask;
        }
    }
}

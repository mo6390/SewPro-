
using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class SmsSettings
    {
        public int Id { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string SmsServiceUrl { get; set; }

        public bool IsEnabled { get; set; }
    }
}

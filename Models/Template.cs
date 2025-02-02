using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Template
    {
        public int Id { get; set; }

        [Required]
        public string ArabicName { get; set; } // اسم القالب باللغة العربية

        [Required]
        public string EnglishName { get; set; } // اسم القالب باللغة الإنجليزية

        public string Description { get; set; } // الوصف

        [Required]
        public string TemplateType { get; set; } // نوع القالب (عادي، رسومي، اختيارات...)

        public bool IsActive { get; set; } // إذا كان القالب مفعلًا

        public bool ShowNameWithValue { get; set; } // إظهار الاسم مع القيمة في الطباعة

        public bool ShowBorderAroundField { get; set; } // إظهار إطار حول الحقل
        public string DesignType { get; set; } // إضافة خاصية DesignType

        public ICollection<Field> Fields { get; set; } // الحقول المربوطة بالقالب
    }
}

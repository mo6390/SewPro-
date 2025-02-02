using System.Collections.Generic;
using SewPro.Models;

namespace SewPro.ViewModels
{
    public class TemplateViewModel
    {
        public string ArabicName { get; set; } // أضف هذه الخاصية
        public string EnglishName { get; set; } // أضف هذه الخاصية
        public string Description { get; set; } // أضف هذه الخاصية
        public string TemplateType { get; set; }
        public bool IsActive { get; set; }
        public bool ShowNameWithValue { get; set; }
        public bool ShowBorderAroundField { get; set; } // أضف هذه الخاصية
        public List<Field> AvailableFields { get; set; }
        public List<int> SelectedFields { get; set; }
        public string DesignType { get; set; } // أضف هذه الخاصية إذا كانت مطلوبة
    }
}
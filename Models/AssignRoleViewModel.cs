using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SewPro.Models
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}

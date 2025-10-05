using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmithSwimmingSchool.Models;

namespace SmithSwimmingSchool.ViewModels
{
    public class RoleAddUserRoleViewModel

    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
        public SelectList RoleList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OOPTut.Web.UI.Models.ManageViewModels
{
    public class AddRoleToUserViewModel
    {
        public List<SelectListItem> UserList { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}

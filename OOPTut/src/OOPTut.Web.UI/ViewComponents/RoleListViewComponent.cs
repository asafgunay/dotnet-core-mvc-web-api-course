using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OOPTut.Web.UI.ViewComponents
{
    [ViewComponent]
    public class RoleListViewComponent : ViewComponent
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleListViewComponent(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
    }
}

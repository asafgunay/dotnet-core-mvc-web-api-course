using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application.NavbarService;
using OOPTut.Core.Navbar;
using OOPTut.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPTut.Web.UI.ViewComponents
{
    [ViewComponent]
    public class NavBarMenuViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INavBarMenuItemService _navBarMenuItemService;
        public NavBarMenuViewComponent(
            UserManager<ApplicationUser> userManager,
            INavBarMenuItemService navBarMenuItemService)
        {
            _userManager = userManager;
            _navBarMenuItemService = navBarMenuItemService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // navbar
            List<NavBarMenuItem> model = new List<NavBarMenuItem>();
            // getAllMenuItems
            var navList = await _navBarMenuItemService.GetAll();
            // anonym menu nesnelerini model listesine ekle
            model.AddRange(navList.Where(x => x.IsAnonym).ToList());
            // client giris yapmış mı?
            bool isAuthenticated = User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                // kişi giriş yapmış ise anonim olmayan ve rol koşulu bulunmayan menü nesnelerini modele ekle
                model.AddRange(navList.Where(x => !x.IsAnonym && string.IsNullOrEmpty(x.Roles)).ToList());
                // giriş yaptıysa rolü var mı?
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                List<string> userRoles = _userManager.GetRolesAsync(user).Result.ToList();
                // eger kullanicin rolu varsa?
                if (userRoles.Any())
                {
                    var roledNavList = navList.Where(x => !string.IsNullOrEmpty(x.Roles)).ToList();
                    foreach (var item in roledNavList)
                    {
                        List<string> navRoles = item.Roles.Split(",").ToList();
                        bool hasEqualRoles = navRoles.Intersect(userRoles).Count() > 0;
                        if (hasEqualRoles)
                            model.Add(item);
                    }
                }
            }

            // yukarıdaki koşullara göre servisten gelen menü nesnelerini filtrele
            // ardından bunu view'e dön
            return View(model.OrderBy(x => x.Order).ToList());
        }
    }
}

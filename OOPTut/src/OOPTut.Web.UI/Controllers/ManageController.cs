using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOPTut.Core.Users;
using OOPTut.EntityFramework.Contexts;
using OOPTut.Web.UI.Models.AccountViewModels;
using OOPTut.Web.UI.Models.ManageViewModels;

namespace OOPTut.Web.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationUserDbContext _context;

        public ManageController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationUserDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        // [Authorize(Roles ="Admin, TeamLeader")]
        public async Task<IActionResult> Index()
        {
            //var usersRoles = _roleManager
            //
            var userRoles = await _context.UserRoles.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            List<UserListViewModel> users = await _userManager.Users
                .Select(x => new UserListViewModel
                {
                    Id = x.Id,
                    Email = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserRoles = userRoles.Where(ur => ur.UserId == x.Id).Any()
                            ?
                        /*kulanicinin rolu varsa*/
                        roles.Where(
                            r =>
                   userRoles.Where(ur => ur.UserId == x.Id).Select(ur => ur.RoleId).Contains(r.Id)).ToList()
                        :
                        /*yoksa*/
                        new List<IdentityRole>()
                }).ToListAsync();

            return View(users);
        }
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(model);
                return RedirectToAction("CreateRole");
            }
            return View(model);
        }

        public async Task<IActionResult> AddRoleToUser()
        {
            // iki tane ddl lazim
            // birincisi kullanici listesi
            List<SelectListItem> userList = new List<SelectListItem>();
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                userList.Add(new SelectListItem
                {
                    Selected = false,
                    Text = user.Email,
                    Value = user.Id
                });
            }
            // ikincisi eklenebilir roller listesi
            List<SelectListItem> roleList = new List<SelectListItem>();
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            foreach (var identityRole in roles)
            {
                roleList.Add(new SelectListItem
                {
                    Selected = false,
                    Text = identityRole.Name,
                    Value = identityRole.Id
                });
            }
            AddRoleToUserViewModel model = new AddRoleToUserViewModel
            {
                UserList = userList,
                RoleList = roleList
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // business logic
                // Kullaniciyi Role Ekle
                var user = await _userManager.FindByIdAsync(model.UserId);
                var roleName = await _roleManager.FindByIdAsync(model.RoleId);
                var assignRole = await _userManager.AddToRoleAsync(user, roleName.Name);
                if (assignRole.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        public async Task<IActionResult> RolesListPartial()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
    }
}
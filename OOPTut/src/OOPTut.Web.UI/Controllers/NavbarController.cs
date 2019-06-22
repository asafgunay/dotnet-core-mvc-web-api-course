using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application.NavbarService;
using OOPTut.Application.NavbarService.Dto;

namespace OOPTut.Web.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NavbarController : Controller
    {
        private readonly INavBarMenuItemService _navBarMenuItemService;
        public NavbarController(INavBarMenuItemService navBarMenuItemService)
        {
            _navBarMenuItemService = navBarMenuItemService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _navBarMenuItemService.GetAll();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateNavBarMenuItemInput model)
        {
            if (ModelState.IsValid)
            {
                await _navBarMenuItemService.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
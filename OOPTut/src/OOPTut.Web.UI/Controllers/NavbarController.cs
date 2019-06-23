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

        public async Task<IActionResult> Update(int id)
        {
            var item = await _navBarMenuItemService.Get(id);
            UpdateNavBarMenuItemInput model = new UpdateNavBarMenuItemInput
            {
                Id = item.Id,
                Icon=item.Icon,
                IsAnonym=item.IsAnonym,
                OpenInSamePage=item.OpenInSamePage,
                Roles=item.Roles,
                Title=item.Title,
                Url=item.Url,
                Order=item.Order
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateNavBarMenuItemInput model)
        {
            if (ModelState.IsValid)
            {
                await _navBarMenuItemService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _navBarMenuItemService.Get(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _navBarMenuItemService.Delete(new DeleteNavBarMenuItemInput { Id = id });
            return RedirectToAction("Index");
        }
    }
}
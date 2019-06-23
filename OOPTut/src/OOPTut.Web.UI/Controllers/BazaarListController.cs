using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OOPTut.Application;
using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OOPTut.Web.UI.Controllers
{
    [Authorize]
    public class BazaarListController : Controller
    {
        private readonly IBazaarListService _bazaarListService;
        public BazaarListController(IBazaarListService bazaarListService)
        {
            _bazaarListService = bazaarListService;
        }
        /// <summary>
        /// Servis katmanindan gelecek olan BazaarList Listesi burada goruntulenir
        /// </summary>
        /// <returns>BazaarList Tablosun liste gorunumu</returns>
        public async Task<IActionResult> Index()
        {
            // return View(await _bazaarListService.GetAll());
            if (User.IsInRole("Admin"))
            {
                return View(await _bazaarListService.GetAll());
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(await _bazaarListService.GetAllByOwner(userId));
        }
        public async Task<JsonResult> GetList()
        {
            List<BazaarList> list = await _bazaarListService.GetAllRaw();
            var resultJson = JsonConvert.SerializeObject(list);
            return Json(resultJson);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBazaarList model)
        {
            if (ModelState.IsValid)
            {
                model.CreatorUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var newItem = await _bazaarListService.Create(model);
                return RedirectToAction("Index", "BazaarList");
            }
            return View();
        }
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _bazaarListService.Get(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(DeleteBazaarList model)
        {
            if (ModelState.IsValid)
            {
                // silme islemini yap 
                await _bazaarListService.Delete(model.Id);
            }
            // ve

            // liste sayfasina gonder
            return RedirectToAction("Index", "BazaarList");

        }

        public async Task<ActionResult> Update(int id)
        {
            var model = await _bazaarListService.Get(id);
            UpdateBazaarList updateModel = new UpdateBazaarList
            {
                Id = model.Id,
                CreatorUserId = model.CreatorUserId,
                Description = model.Description,
                Title = model.Title
            };
            return View(updateModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateBazaarList model)
        {
            if (ModelState.IsValid)
            {
                var updatedBazaarList = await _bazaarListService.Update(model);

                UpdateBazaarList updateModel = new UpdateBazaarList
                {
                    Id = updatedBazaarList.Id,
                    CreatorUserId = updatedBazaarList.CreatorUserId,
                    Description = updatedBazaarList.Description,
                    Title = updatedBazaarList.Title
                };
                return View(updateModel);
            }
            return View(model);
        }

    }
}
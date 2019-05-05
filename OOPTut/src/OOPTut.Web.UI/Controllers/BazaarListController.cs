using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application;
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
            return View(await _bazaarListService.GetAll());
        }
        public async Task<ActionResult> Create()
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
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
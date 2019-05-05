using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OOPTut.Web.UI.Controllers
{
    [Authorize]
    public class BazaarListController : Controller
    {
        /// <summary>
        /// Servis katmanindan gelecek olan BazaarList Listesi burada goruntulenir
        /// </summary>
        /// <returns>BazaarList Tablosun liste gorunumu</returns>
        public IActionResult Index()
        {
            return View(/*bazaarListService.GetAll()*/);
        }
    }
}
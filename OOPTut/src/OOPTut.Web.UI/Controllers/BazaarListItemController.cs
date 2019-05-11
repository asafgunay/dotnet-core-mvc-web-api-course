using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application.BazaarListItemServices.Dto;

namespace OOPTut.Web.UI.Controllers
{
    [Authorize]
    public class BazaarListItemController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
        public IActionResult Create(CreateBazaarListItem model)
        {

        }
    }
}
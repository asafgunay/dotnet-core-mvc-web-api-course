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
            // 1- servis katmaninda GetAllByIdAsync diye bir metod olusturun ve bu metod gelen bazaarListId parametresine gore ilgili bazaarListItem'leri ceksin 
            return View();
        }
        //[HttpGet("{id}")]
        public IActionResult Create(int id)
        {
            // Yeni bir CreateBazaarListItem sınıfını ayağa kaldır
            CreateBazaarListItem model = new CreateBazaarListItem();

            // model.BazaarListId'yi parametreden gelen id'ye eşitle
            model.BazaarListId = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(int id, CreateBazaarListItem model)
        {
            // CreateBazaarListItem model
            // servis katmani
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application;
using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOPTut.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BazaarListController : ControllerBase
    {
        private readonly IBazaarListService _bazaarListService;
        public BazaarListController(IBazaarListService bazaarListService)
        {
            _bazaarListService = bazaarListService;
        }
        // nasil calisir?
        // istek yapildiginda ona tum listeyi dön
        // servisAdi.MetodAdi(parametreler varsa?);
        // _bazaarListService.GetAll()
        [HttpGet]
        public async Task<ActionResult<List<BazaarList>>> GetAll(){
            return await _bazaarListService.GetAll();
        }


        // [HttpGet({"id"})]
        // istek yapildiginda id ye sahip tek öğeyi dönecek

        // [HttpPost]
        // Yeni kayit olusturur

        // [HttpPut]
        // Update yapar

        // [HttpDelete({"id"})]
        // Silme yapar 
    }
}
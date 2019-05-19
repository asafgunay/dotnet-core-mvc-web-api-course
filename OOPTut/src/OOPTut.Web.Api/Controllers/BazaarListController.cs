using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application;
using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOPTut.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<List<BazaarList>>> GetAll()
        {
            return await _bazaarListService.GetAll();
        }


        // istek yapildiginda id ye sahip tek öğeyi dönecek
        [HttpGet("{id}")]
        public async Task<ActionResult<BazaarList>> Get(int id)
        {
            return await _bazaarListService.Get(id);
        }

        [HttpPost]
        // Yeni kayit olusturur
        public async Task<ActionResult<BazaarList>> Create(CreateBazaarList input)
        {
            return await _bazaarListService.Create(input);
        }

        // Update yapar
        [HttpPut]
        public async Task<ActionResult<BazaarList>> Update(UpdateBazaarList input)
        {
            return await _bazaarListService.Update(input);
        }


        // Silme yapar 
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bazaarListService.Delete(id);
            return Ok();
        }
    }
}
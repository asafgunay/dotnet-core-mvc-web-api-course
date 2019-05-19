using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOPTut.Application.BazaarListItemServices;
using OOPTut.Application.BazaarListItemServices.Dto;
using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOPTut.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BazaarListItemController : ControllerBase
    {
        private readonly IBazaarListItemService _bazaarListItemService;
        public BazaarListItemController(IBazaarListItemService bazaarListItemService)
        {
            _bazaarListItemService = bazaarListItemService;
        }
        [HttpGet("{id}")]
        [ActionName("GetAll")]
        public async Task<ActionResult<List<BazaarListItem>>> GetAllByIdAsync(int id)
        {
            return await _bazaarListItemService.GetAllByIdAsync(id);
        }
        [HttpGet("{id}")]
        [ActionName("Get")]
        public async Task<ActionResult<BazaarListItem>> GetAsync(int id)
        {
            return await _bazaarListItemService.GetAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<BazaarListItem>> CreateAsync(CreateBazaarListItem input)
        {
            return await _bazaarListItemService.CreateAsync(input);
        }

        [HttpPut]
        public async Task<ActionResult<BazaarListItem>> UpdateAsync(UpdateBazaarListItem input)
        {
            return await _bazaarListItemService.UpdateAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _bazaarListItemService.DeleteAsync(id);
            return Ok();
        }

    }
}
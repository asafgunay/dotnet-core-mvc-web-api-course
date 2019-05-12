using OOPTut.Application.BazaarListItemServices.Dto;
using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOPTut.Application.BazaarListItemServices
{
   public interface IBazaarListItemService
    {
        Task<List<BazaarListItem>> GetAllByIdAsync(int bazaarListId);

        Task<BazaarListItem> CreateAsync (CreateBazaarListItem input);
        Task<BazaarListItem> UpdateAsync(UpdateBazaarListItem input);
        Task<BazaarListItem> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}

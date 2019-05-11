using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOPTut.Application.BazaarListItemServices
{
   public interface IBazaarListItemService
    {
        Task<List<BazaarListItem>> GetAllByIdAsync(int bazaarListId);

    }
}

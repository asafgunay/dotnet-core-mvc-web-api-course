using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OOPTut.Core.Bazaar;

namespace OOPTut.Application.BazaarListItemServices
{
    public class BazaarListItemService : IBazaarListItemService
    {
        public async Task<List<BazaarListItem>> GetAllByIdAsync(int bazaarListId)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPTut.Core.Bazaar;
using OOPTut.EntityFramework.Contexts;

namespace OOPTut.Application.BazaarListItemServices
{
    public class BazaarListItemService : IBazaarListItemService
    {
        // context i çağır!
        private readonly ApplicationUserDbContext _context;
        public BazaarListItemService(ApplicationUserDbContext context)
        {
            _context = context;
        }

        public async Task<List<BazaarListItem>> GetAllByIdAsync(int bazaarListId)
        {
            // List<BazaarListItem> içine veritabanından ilgili satırlar gönderilecek ve return edilecek
            List<BazaarListItem> result = await (from u in _context.BazaarListItems where u.BazaarListId == bazaarListId select u).ToListAsync();
            // List<BazaarListItem> result = await _context.BazaarListItems.Where(x => x.BazaarListId == bazaarListId).ToListAsync();
            return result;
            //
        }
    }
}

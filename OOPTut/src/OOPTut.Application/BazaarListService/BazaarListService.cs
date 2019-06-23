using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPTut.Core.Bazaar;
using OOPTut.EntityFramework.Contexts;

namespace OOPTut.Application
{
    public class BazaarListService : IBazaarListService
    {
        private ApplicationUserDbContext _context;
        public BazaarListService(ApplicationUserDbContext context)
        {
            _context = context;
        }


        public async Task<BazaarList> Create(CreateBazaarList input)
        {
            BazaarList newBazaarList = BazaarList.Create(input.Title, input.Description, input.CreatorUserId);
            await _context.BazaarLists.AddAsync(newBazaarList);
            await _context.SaveChangesAsync();
            return newBazaarList;
        }

        public async Task Delete(int id)
        {
            var item = await Get(id);
            _context.BazaarLists.Remove(item);
            await _context.SaveChangesAsync();

        }

        public async Task<BazaarList> Get(int id)
        {
            var item = await _context
                .BazaarLists
                .Where(x => x.Id == id)
                .Include(x => x.BazaarListItems)
                .FirstOrDefaultAsync();
            return item;
        }

        public async Task<List<BazaarList>> GetAll()
        {
            // veritabani icerisindek bazaarLists tablosunun tum satirlarini liste halinde dön
            var list = await _context.BazaarLists
                .Include(x => x.BazaarListItems)
                .ToListAsync();
            return list;
        }
        public async Task<List<BazaarList>> GetAllRaw()
        {
            // veritabani icerisindek bazaarLists tablosunun tum satirlarini liste halinde dön
            var list = await _context.BazaarLists
                .ToListAsync();
            return list;
        }

        public async Task<List<BazaarList>> GetAllByOwner(string userId)
        {
            var list = await _context.BazaarLists
                .Where(x => x.CreatorUserId == userId)
                .Include(x => x.BazaarListItems).ToListAsync();
            return list;
        }

        public async Task<BazaarList> Update(UpdateBazaarList input)
        {
            var updateBazaarList = await Get(input.Id);
            updateBazaarList.Title = input.Title;
            updateBazaarList.Description = input.Description;
            _context.BazaarLists.Update(updateBazaarList);
            await _context.SaveChangesAsync();
            return updateBazaarList;
        }

    }
}

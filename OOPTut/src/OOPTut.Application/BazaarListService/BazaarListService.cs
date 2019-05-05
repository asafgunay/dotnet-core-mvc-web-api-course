using System;
using System.Collections.Generic;
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
            var item = await _context.BazaarLists.FindAsync(id);
            return item;
        }

        public async Task<List<BazaarList>> GetAll()
        {
            // veritabani icerisindek bazaarLists tablosunun tum satirlarini liste halinde dön
            var list = await _context.BazaarLists.ToListAsync();
            return list;
        }

        public async Task<BazaarList> Update(UpdateBazaarList input)
        {
            throw new NotImplementedException();
        }
    }
}

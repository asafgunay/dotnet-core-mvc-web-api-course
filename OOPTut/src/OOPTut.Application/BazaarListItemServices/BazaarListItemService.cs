using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPTut.Application.BazaarListItemServices.Dto;
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

        public async Task<BazaarListItem> CreateAsync(CreateBazaarListItem input)
        {
            var item = BazaarListItem.Create(input.Name, input.BazaarListId, input.CreatorUserId);

            await _context.BazaarListItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<BazaarListItem> GetAsync(int id)
        {
            return await _context.BazaarListItems.FindAsync(id);
        }

        public async Task<List<BazaarListItem>> GetAllByIdAsync(int bazaarListId)
        {
            // List<BazaarListItem> içine veritabanından ilgili satırlar gönderilecek ve return edilecek
            //List<BazaarListItem> result = await (from u in _context.BazaarListItems where u.BazaarListId == bazaarListId select u).ToListAsync();
            List<BazaarListItem> result = await _context.BazaarListItems
                .Where(x => x.BazaarListId == bazaarListId)
                .Include(i => i.BazaarList)
                .ToListAsync();
            return result;
            //
        }

        public async Task<BazaarListItem> UpdateAsync(UpdateBazaarListItem input)
        {
            var willUpdate = await GetAsync(input.Id);
            willUpdate.IsCanceled = input.IsCanceled;
            willUpdate.IsCompleted = input.IsCompleted;
            willUpdate.Name = input.Name;
            _context.BazaarListItems.Update(willUpdate);
            await _context.SaveChangesAsync();
            return willUpdate;
        }

        public async Task DeleteAsync(int id)
        {
            var willDeleted = await GetAsync(id);
            _context.BazaarListItems.Remove(willDeleted);
            await _context.SaveChangesAsync();
        }
    }
}

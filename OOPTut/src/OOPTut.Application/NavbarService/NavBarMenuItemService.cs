using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOPTut.Application.NavbarService.Dto;
using OOPTut.Core.Navbar;
using OOPTut.EntityFramework.Contexts;

namespace OOPTut.Application.NavbarService
{
    public class NavBarMenuItemService : INavBarMenuItemService
    {
        private readonly ApplicationUserDbContext _context;
        public NavBarMenuItemService(ApplicationUserDbContext context)
        {
            _context = context;
        }

        public async Task<List<NavBarMenuItem>> GetAll()
        {
            return await _context.NavBarMenuItems.ToListAsync();
        }

        public async Task<NavBarMenuItem> Create(CreateNavBarMenuItemInput input)
        {
            // create'e hazir bir model olusturan metodu calistiriyor.
            NavBarMenuItem createModel = NavBarMenuItem.Create(input.Title, input.Url, input.OpenInSamePage, input.Icon, input.Roles);
            // olusan createModel context e kaydediliyor
            await _context.NavBarMenuItems.AddAsync(createModel);
            // sonra contextteki degisiklikler veritabanina iletiliyor
            await _context.SaveChangesAsync();
            return createModel;
        }
        public async Task<NavBarMenuItem> Update(UpdateNavBarMenuItemInput input)
        {
            throw new NotImplementedException();
        }
        public async Task Delete(DeleteNavBarMenuItemInput input)
        {
            throw new NotImplementedException();
        }
    }
}

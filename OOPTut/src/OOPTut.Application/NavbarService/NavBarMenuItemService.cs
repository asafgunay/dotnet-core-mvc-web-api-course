using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.NavBarMenuItems.OrderBy(x => x.Order).ToListAsync();
        }
        public async Task<NavBarMenuItem> Get(int id)
        {
            return await _context.NavBarMenuItems.FindAsync(id);
        }

        public async Task<NavBarMenuItem> Create(CreateNavBarMenuItemInput input)
        {
            // create'e hazir bir model olusturan metodu calistiriyor.
            NavBarMenuItem createModel = NavBarMenuItem.Create(input.Title, input.Url, input.OpenInSamePage, input.Icon, input.Roles, input.IsAnonym, input.Order);
            // olusan createModel context e kaydediliyor
            await _context.NavBarMenuItems.AddAsync(createModel);
            // sonra contextteki degisiklikler veritabanina iletiliyor
            await _context.SaveChangesAsync();
            return createModel;
        }
        public async Task<NavBarMenuItem> Update(UpdateNavBarMenuItemInput input)
        {
            var navbarItem = await Get(input.Id);
            navbarItem.Icon = input.Icon;
            navbarItem.IsAnonym = string.IsNullOrEmpty(input.Roles) ? input.IsAnonym : false;
            // roller bossa => gelen is anonym neyse onu yap
            // roller doluysa => isanonym'i false don
            navbarItem.OpenInSamePage = input.OpenInSamePage;
            navbarItem.Roles = input.Roles;
            navbarItem.Title = input.Title;
            navbarItem.Url = input.Url;
            navbarItem.Order = input.Order;
            _context.NavBarMenuItems.Update(navbarItem);
            await _context.SaveChangesAsync();
            return navbarItem;
        }
        public async Task Delete(DeleteNavBarMenuItemInput input)
        {
            try
            {
                var deleteItem = await Get(input.Id);
                if (deleteItem != null)
                {
                    _context.NavBarMenuItems.Remove(deleteItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

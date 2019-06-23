using OOPTut.Application.NavbarService.Dto;
using OOPTut.Core.Navbar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOPTut.Application.NavbarService
{
    public interface INavBarMenuItemService
    {
        // GetAll
        Task<List<NavBarMenuItem>> GetAll();

        // Get 
        Task<NavBarMenuItem> Get(int id);

        // Create
        Task<NavBarMenuItem> Create(CreateNavBarMenuItemInput input);
        // Update
        Task<NavBarMenuItem> Update(UpdateNavBarMenuItemInput input);
        // Delete
        Task Delete(DeleteNavBarMenuItemInput input);

        // Menuye yeni bir nesne eklendiginde once eklendigi siraya bakalim eger sira numarasina sahip bir nesne varsa o ve ondan buyuk olanlarin sira numaralarini birer artiralim.

        Task OrderNumberFix(int newOrder);
    }
}

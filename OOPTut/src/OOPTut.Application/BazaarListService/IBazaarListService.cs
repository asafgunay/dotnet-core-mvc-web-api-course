﻿using OOPTut.Core.Bazaar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOPTut.Application
{
    public interface IBazaarListService
    {
        Task<List<BazaarList>> GetAll();
        Task<BazaarList> Get(int id);
        Task<BazaarList> Create(CreateBazaarList input);
        Task<BazaarList> Update(UpdateBazaarList input);
        Task Delete(int id);
    }
}

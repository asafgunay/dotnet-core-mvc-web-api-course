﻿using Microsoft.EntityFrameworkCore;
using OOPTut.Application.BazaarListItemServices;
using OOPTut.Application.BazaarListItemServices.Dto;
using OOPTut.Core.Bazaar;
using OOPTut.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OOPTut.UnitTests
{
    public class BazaarListItemServiceShould
    {
        // sabit bir bazaarlist id
        private readonly int bazaarListId;
        // options
        private readonly DbContextOptions<ApplicationUserDbContext> options;
        // context
        private readonly ApplicationUserDbContext inMemoryDbContext;
        // servis
        private readonly BazaarListItemService service;

        // constructor
        public BazaarListItemServiceShould()
        {
            bazaarListId = 1;
            options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "BazaarListItemService_TestDb").Options;
            inMemoryDbContext = new ApplicationUserDbContext(options);
            service = new BazaarListItemService(inMemoryDbContext);
        }
        private async Task CreateFirst()
        {
            // parametresiyle fake data olustur
            CreateBazaarListItem fakeInput = new CreateBazaarListItem {
                BazaarListId = bazaarListId,
                CreatorUserId = Guid.NewGuid().ToString(),
                Name = "Domates_Test"
            };
            // servisi calistir
            await service.CreateAsync(fakeInput);
        }
        private async Task CleanAll()
        {
            // tum veriyi list halinde getir
            var getAll = await inMemoryDbContext.BazaarListItems.ToListAsync();
            // ilgili tum veriyi sil
            inMemoryDbContext.RemoveRange(getAll);
            await inMemoryDbContext.SaveChangesAsync();
        }
        [Fact]
        public async Task Create()
        {
            await CreateFirst();
            // kaydedilen veriyi cek
            BazaarListItem created = await inMemoryDbContext.BazaarListItems.FirstOrDefaultAsync();
            // kontrol et!
            Assert.False(created.IsCanceled);
            Assert.False(created.IsCompleted);
            Assert.Equal(bazaarListId, created.BazaarListId);
            Assert.Equal("Domates_Test", created.Name);
        }
        [Fact]
        public async Task Get()
        {
            // BazaarListItem id si bul
            // id ye gore servisi calistir
            // kontrol et!
        }
        [Fact]
        public async Task GetAllById()
        {
            // BazaarListId bul
            // servisi calistir
            // kontrol et@
        }
        [Fact]
        public async Task Update()
        {
            // id yi bul
            // update icin parametreleri olustur
            // servisi calistir
            // kontrol et!
        }
        [Fact]
        public async Task Delete()
        {
            // id yi bul
            // servisi calistir
            // kontrol et!
        }
    }
}

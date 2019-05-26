﻿using Microsoft.EntityFrameworkCore;
using OOPTut.Application;
using OOPTut.Core.Bazaar;
using OOPTut.EntityFramework.Contexts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OOPTut.UnitTests
{
    public class BazaarListServiceShould
    {
        private readonly DbContextOptions<ApplicationUserDbContext> options;
        private readonly ApplicationUserDbContext inMemoryContext;
        private readonly BazaarListService service;
        public BazaarListServiceShould()
        {
            // veritabani ayarlarini hafizada calisacak sekilde ayarlar
            options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "BazaarListService_TestDb").Options;
            inMemoryContext = new ApplicationUserDbContext(options);
            service = new BazaarListService(inMemoryContext);
        }
        [Fact]
        public async Task Create()
        {
            // Create metodu test edilecek
            // Parametresini olusturalim
            CreateBazaarList testInput = new CreateBazaarList
            {
                CreatorUserId = Guid.NewGuid().ToString(),
                Description = "Test_Aciklama",
                Title = "Test_Baslik"
            };
            // fake data ile metodu calistir.
            await service.Create(testInput);

            // olusturulan fake datanin durumunu test et


            // Assert bir test sorgusu cesididir sorgu basariliysa test adimi da basarilidir.
            // Assert.Equal degerlerin birebir esit olup olmadigini kontrol eder.
            Assert.Equal(1, await inMemoryContext.BazaarLists.CountAsync());
            var item = await inMemoryContext.BazaarLists.FirstAsync();
            Assert.Equal("Test_Aciklama", item.Description);
            Assert.Equal("Test_Baslik", item.Title);

        }

        [Fact]
        public async Task Get()
        {

            var getResponse = new BazaarList();
            // Create metodu test edilecek
            // Parametresini olusturalim
            CreateBazaarList testInput = new CreateBazaarList
            {
                CreatorUserId = Guid.NewGuid().ToString(),
                Description = "Test_Aciklama",
                Title = "Test_Baslik"
            };
            // fake data ile metodu calistir.
            var createResponse = await service.Create(testInput);
            getResponse = await service.Get(createResponse.Id);

            Assert.Equal(1, await inMemoryContext.BazaarLists.CountAsync());
            Assert.Equal("Test_Baslik", getResponse.Title);
            Assert.Equal("Test_Aciklama", getResponse.Description);
        }

        [Fact]
        public async Task Update()
        {
            // set context / contexti ayarla
            // declare id variable / id diye bir degisken tanimla
            int id = 0;
            // first scope / ilk context scope u
            // create
            // Create metodu test edilecek
            // Parametresini olusturalim
            CreateBazaarList testInput = new CreateBazaarList
            {
                CreatorUserId = Guid.NewGuid().ToString(),
                Description = "Test_Aciklama",
                Title = "Test_Baslik"
            };
            // fake data ile metodu calistir.
            var createResponse = await service.Create(testInput);
            // set id variable / id yi ayarla
            id = createResponse.Id;
            // update / update i calistir
            UpdateBazaarList input = new UpdateBazaarList
            {
                CreatorUserId = Guid.NewGuid().ToString(),
                Description = "Test_Aciklama_Guncel",
                Title = "Test_Baslik_Guncel",
                Id = id
            };
            await service.Update(input);

            // get / get ile datayi cek
            var getResponse = await service.Get(id);
            // asserts
            Assert.Equal("Test_Baslik_Guncel", getResponse.Title);
            Assert.Equal("Test_Aciklama_Guncel", getResponse.Description);
        }
    }
}

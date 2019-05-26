using Microsoft.EntityFrameworkCore;
using OOPTut.Application;
using OOPTut.Core.Bazaar;
using OOPTut.EntityFramework.Contexts;
using System;
using System.Linq;
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

        private async Task CreateFirst()
        {
            var count = await inMemoryContext.BazaarLists.CountAsync();
            // Parametresini olusturalim
            if (count > 1)
            {
                await CleanAll();
                await CreateFirst();
            }
            if (count == 0)
            {
                CreateBazaarList testInput = new CreateBazaarList
                {
                    CreatorUserId = Guid.NewGuid().ToString(),
                    Description = "Test_Aciklama",
                    Title = "Test_Baslik"
                };
                // fake data ile metodu calistir.
                await service.Create(testInput);
            }
            
        }

        private async Task CleanAll()
        {
            var getAll = await service.GetAll();
            inMemoryContext.RemoveRange(getAll);
            await inMemoryContext.SaveChangesAsync();
        }

        [Fact]
        public async Task Create()
        {
            await CreateFirst();
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
            await CreateFirst();
            var firstData = await inMemoryContext.BazaarLists.FirstAsync();
            getResponse = await service.Get(firstData.Id);
            Assert.Equal(1, await inMemoryContext.BazaarLists.CountAsync());
            Assert.Equal("Test_Baslik", getResponse.Title);
            Assert.Equal("Test_Aciklama", getResponse.Description);
        }
        [Fact]
        public async Task GetAll()
        {
            var getResponse = new BazaarList();
            await CreateFirst();
            // GetAll service test
            var getAll = await service.GetAll();
            getResponse = getAll.First();
            Assert.Equal(1, await inMemoryContext.BazaarLists.CountAsync());
            Assert.Equal("Test_Baslik", getResponse.Title);
            Assert.Equal("Test_Aciklama", getResponse.Description);
        }
        [Fact]
        public async Task Delete()
        {

            await CreateFirst();
            var firstData = await inMemoryContext.BazaarLists.FirstAsync();
            // Delete test
            await service.Delete(firstData.Id);
            Assert.Equal(0, await inMemoryContext.BazaarLists.CountAsync());
        }

        [Fact]
        public async Task Update()
        {

            // declare id variable / id diye bir degisken tanimla
            int id = 0;
            // create
            await CreateFirst();
            var firstData = await inMemoryContext.BazaarLists.FirstAsync();
            // set id variable / id yi ayarla
            id = firstData.Id;
            // update / update i calistir
            UpdateBazaarList input = new UpdateBazaarList
            {
                CreatorUserId = Guid.NewGuid().ToString(),
                Description = "Test_Aciklama_Guncel",
                Title = "Test_Baslik_Guncel",
                Id = id
            };
            await service.Update(input);
            // get ile datayi cek
            var getResponse = await service.Get(id);
            // asserts
            Assert.Equal("Test_Baslik_Guncel", getResponse.Title);
            Assert.Equal("Test_Aciklama_Guncel", getResponse.Description);
            await CleanAll();
        }
    }
}

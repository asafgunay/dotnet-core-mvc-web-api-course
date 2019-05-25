using Microsoft.EntityFrameworkCore;
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
        [Fact]
        public async Task Create()
        {
            // veritabani ayarlarini hafizada calisacak sekilde ayarlar
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "TestCreate_Context").Options;

            // belirtilen ayarlar ile bir context olusturur
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // test edecegimiz servisin context ini ayarlar 
                var service = new BazaarListService(inMemoryContext);

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
            }

            // olusturulan fake datanin durumunu test et

            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // Assert bir test sorgusu cesididir sorgu basariliysa test adimi da basarilidir.
                // Assert.Equal degerlerin birebir esit olup olmadigini kontrol eder.
                Assert.Equal(1, await inMemoryContext.BazaarLists.CountAsync());
                var item = await inMemoryContext.BazaarLists.FirstAsync();
                Assert.Equal("Test_Aciklama", item.Description);
                Assert.Equal("Test_Baslik", item.Title);
            }

        }

        [Fact]
        public async Task Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "TestGet_Context").Options;
            var getResponse = new BazaarList();

            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // test edecegimiz servisin context ini ayarlar 
                var service = new BazaarListService(inMemoryContext);

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
            }
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                Assert.Equal(1, await inMemoryContext.BazaarLists.CountAsync());
                Assert.Equal("Test_Baslik", getResponse.Title);
                Assert.Equal("Test_Aciklama", getResponse.Description);
            }
        }

        [Fact]
        public async Task Update()
        {
            // set context / contexti ayarla
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "TestUpdate_Context").Options;
            // declare id variable / id diye bir degisken tanimla
            int id = 0;
            // first scope / ilk context scope u
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // create
                // test edecegimiz servisin context ini ayarlar 
                var service = new BazaarListService(inMemoryContext);

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
            }

            // second scope
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // update / update i calistir
                var service = new BazaarListService(inMemoryContext);
                UpdateBazaarList input = new UpdateBazaarList
                {
                    CreatorUserId = Guid.NewGuid().ToString(),
                    Description = "Test_Aciklama_Guncel",
                    Title = "Test_Baslik_Guncel",
                    Id =id
                };
                await service.Update(input);
            }

            // third scope
            using (var inMemoryContext = new ApplicationUserDbContext(options))
            {
                // get / get ile datayi cek
                var service = new BazaarListService(inMemoryContext);
                var getResponse = await service.Get(id);
                // asserts
                Assert.Equal("Test_Baslik_Guncel", getResponse.Title);
                Assert.Equal("Test_Aciklama_Guncel", getResponse.Description);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OOPTut.Application;
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
            var options = new DbContextOptionsBuilder<ApplicationUserDbContext>().UseInMemoryDatabase(databaseName: "Test_Context").Options;

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

    }
}

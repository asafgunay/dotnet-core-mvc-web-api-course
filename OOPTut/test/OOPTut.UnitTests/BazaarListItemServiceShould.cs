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
        // options
        // context
        // servis
        // constructor
        private async Task CreateFirst()
        {
            // parametresiyle fake data olustur
            // servisi calistir
        }
        private async Task CleanAll()
        {
            // ilgili tum veriyi sil
        }
        [Fact]
        public async Task Create()
        {

            // kaydedilen veriyi cek
            // kontrol et!
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

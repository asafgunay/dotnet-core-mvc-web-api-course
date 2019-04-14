using DotnetMVCBaslangic.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetMVCBaslangic.EntityFramework
{
    // Entity Framework ile veritabanimizi yonetmeye saglayan ana sinif
    public class DotnetMVCBaslangicDbContext : DbContext // EF yapisindan Context sinifini kalitim alir
    {
        // yapici metodu olusturmak icin `ctor` yazip iki kere tab yapalim
        public DotnetMVCBaslangicDbContext(DbContextOptions<DotnetMVCBaslangicDbContext> options) : base(options)
        {
            // yapici metod
        }
        //ayca
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        //}

        // buraya DbSet tipi vererek veritabani tablolarimi olusturabilirim.

        public DbSet<Ogrenci> Ogrenciler { get; set; }

    }
}

using OOPTut.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OOPTut.Core.Bazaar
{
    // Paylaşılan pazar listeleri kiminle paylaşıldığı burada belirtilecek
    public class SharedBazaarList
    {
        public int Id { get; set; }

        // bazaarlist icin foreign key 
        [ForeignKey("BazaarListId")]
        public virtual BazaarList BazaarList { get; set; }
        public virtual int? BazaarListId { get; set; }

        // izin verilen kullanici ve foreign key
        [ForeignKey("AllowedUserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual string AllowedUserId { get; set; }
    }
    // EF kisa tekrar
    //
    // Veritabanina eklemek icin gereken adimlar
    // İlk önce bu class'ı Context'e ekleyelim
    // OOPTut.EntityFramework/Contexts/ApplicationUserDbContext
    // public DbSet<SharedBazaarList> SharedBazaarLists {get; set;}
    // Bu satir eklendikten sonra artik migration alabiliriz
    // Migration: Veritabanina uygulamadiki ilgili degisiklikleri ileten kod paketidir.
    // PackageManagerConsole ile migration yapalim
    // Default Project kisminda contextin bulundugu proje secili olacak
    // Add-Migration CreateSharedBazaarListTable
    // Sonra oluşan migration a bakalım.
    // Migration uygun degilse Remove-Migration ile kaldir
    // Sonra modeldeki degisikligi yapin
    // ardindan tekrar migration alin ve kontrol edin
    // Her sey dogruysa
    // Package Manager Console'da Update-Database yapin
}

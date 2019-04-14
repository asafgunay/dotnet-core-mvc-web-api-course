using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMVCBaslangic.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int OgrenciNo { get; set; }

        // EF dahil edildikten sonra bu metod kullanilmayacak
        //public static List<Ogrenci> GetFakeDataList()
        //{
        //    var birinciOgrenci = new Ogrenci
        //    {
        //        Id = 1,
        //        Adi = "Mehmet",
        //        Soyadi = "Durmaz",
        //        OgrenciNo = 5
        //    };
        //    Ogrenci ikinciOgrenci = new Ogrenci();
        //    ikinciOgrenci.Id = 2;
        //    ikinciOgrenci.Adi = "Aylin";
        //    ikinciOgrenci.Soyadi = "Celik";
        //    ikinciOgrenci.OgrenciNo = 20;
        //    List<Ogrenci> ogrenciler = new List<Ogrenci>();
        //    ogrenciler.Add(birinciOgrenci);
        //    ogrenciler.Add(ikinciOgrenci);
        //    return ogrenciler;
        //}
    }
}

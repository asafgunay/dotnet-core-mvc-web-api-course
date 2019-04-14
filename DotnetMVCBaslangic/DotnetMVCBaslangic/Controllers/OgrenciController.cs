using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetMVCBaslangic.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMVCBaslangic.Controllers
{
    public class OgrenciController : Controller
    {
        /// <summary>
        /// Ogrenci controller icin varsayilan sayfa action'i
        /// </summary>
        /// <returns>Ogrencilerin Listesini doner</returns>
        public IActionResult Index()
        {
            //List<Ogrenci> ogrenciListesi = Ogrenci.GetFakeDataList();
            return View();
        }
    }
}
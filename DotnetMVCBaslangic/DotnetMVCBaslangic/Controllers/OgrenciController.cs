using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetMVCBaslangic.EntityFramework;
using DotnetMVCBaslangic.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMVCBaslangic.Controllers
{
    public class OgrenciController : Controller
    {
        private DotnetMVCBaslangicDbContext _context;
        public OgrenciController(DotnetMVCBaslangicDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Ogrenci controller icin varsayilan sayfa action'i
        /// </summary>
        /// <returns>Ogrencilerin Listesini doner</returns>
        public IActionResult Index()
        {
            // List<Ogrenci> ogrenciListesi = Ogrenci.GetFakeDataList();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Ogrenci model)
        {
            if (ModelState.IsValid)
            {
                _context.Ogrenciler.Add(model);
                _context.SaveChanges();
                return View();
            }
            return View();
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetMVCBaslangic.EntityFramework;
using DotnetMVCBaslangic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<Ogrenci> ogrenciListesi = _context.Ogrenciler.ToList();
            return View(ogrenciListesi);
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
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                // silinecek ogrenciyi getir
                Ogrenci silinecekOgrenci = _context.Ogrenciler.Find(id);
                // Ogrenci geldi mi kontrol et
                if (silinecekOgrenci == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                // Ogrenci'yi sil
                _context.Ogrenciler.Remove(silinecekOgrenci);
                _context.SaveChanges();
                // Tekrar liste sayfasina yonlendir
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            // id 0 dan buyuk mu?
            if (id > 0)
            {
                // Ogrenciyi id ye gore getir
                Ogrenci gelenOgrenci = _context.Ogrenciler.Find(id);
                // gelen ogrenci null mu?
                if (gelenOgrenci == null)
                {
                    // null ise anasayfaya yonlendir
                    return RedirectToAction("Index", "Home");
                }

                // Gelen Ogrenciyi View e bind et
                return View(gelenOgrenci);
            }
            // id 0 dan buyuk degilse anasayfaya gonder
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // id 0 dan buyuk mu?
            if (id > 0)
            {
                // Ogrenciyi id ye gore getir
                Ogrenci gelenOgrenci = _context.Ogrenciler.Find(id);
                // gelen ogrenci null mu?
                if (gelenOgrenci == null)
                {
                    // null ise anasayfaya yonlendir
                    return NotFound();
                }

                // Gelen Ogrenciyi View e bind et
                return View(gelenOgrenci);
            }
            // id 0 dan buyuk degilse anasayfaya gonder
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Adi,Soyadi,OgrenciNo")] Ogrenci ogrenci)
        {
            if (id != ogrenci.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // guncelleme icin kodlar

                try
                {
                    _context.Update(ogrenci);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Ogrenciler.Find(ogrenci.Id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
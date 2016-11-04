using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewMusicApp.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NewMusicApp.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly MusicDbContext _context;

        public ArtistsController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var artists = _context.Artists.ToList();
            return View(artists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            var artistCheck = _context.Artists.Any(a => a.Name == artist.Name);
            if(artistCheck == true)
            {
                 return View();
            }
            if (ModelState.IsValid && artistCheck == false)
            {
                _context.Artists.Add(artist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = _context.Artists.Single(a => a.ArtistID == id);
            var albums = _context.Albums.Where(a => a.ArtistID == artist.ArtistID).ToList();

            ViewBag.ArtistName = artist.Name;
            ViewBag.ArtistBio = artist.Bio;

            if (artist == null)
            {
                return NotFound();
            }

            return View(albums);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = _context.Artists.AsNoTracking().Single(m => m.ArtistID == id);
            ViewBag.ArtistName = artist.Name;

            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(artist).State = EntityState.Modified;
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}

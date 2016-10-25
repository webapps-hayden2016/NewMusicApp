using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewMusicApp.Models;

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
            if (ModelState.IsValid)
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

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }
    }
}

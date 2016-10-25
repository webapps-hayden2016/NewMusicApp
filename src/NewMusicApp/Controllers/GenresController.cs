using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewMusicApp.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NewMusicApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly MusicDbContext _context;

        public GenresController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        //public IActionResult Listing(int? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }
        //    var albums = _context.Albums.
        //}
    }
}

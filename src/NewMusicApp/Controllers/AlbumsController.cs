using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewMusicApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NewMusicApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;

        public AlbumsController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var albums = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Genre = new SelectList(_context.Genres, "GenreID", "Name");
            ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Album album)
        {
            ViewBag.Genre = new SelectList(_context.Genres, "GenreID", "Name");
            ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");

            if (ModelState.IsValid)
            {   
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var album = _context.Albums.AsNoTracking().Single(a => a.AlbumID == id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }
        //    PopulateArtistsDropDownList(album.ArtistID);
        //    PopulateGenresDropDownList(album.GenreID);
        //    return View(album);
        //}

        //[HttpPost, ActionName("Edit")]
        //public IActionResult EditPost(int? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }
        //    var album = _context.Albums.Single(a => a.AlbumID == id);
        //    //if (id != album.AlbumID)
        //    //{
        //    //    return RedirectToAction("Index");
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        _context.Albums.Update(album);
        //        _context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    PopulateArtistsDropDownList(album.ArtistID);
        //    PopulateGenresDropDownList(album.GenreID);
        //    return View(album);
        //}

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .AsNoTracking()
                .Single(m => m.AlbumID == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).AsNoTracking().Single(m => m.AlbumID == id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");
        //    ViewBag.Genres = new SelectList(_context.Genres, "GenreID", "Name");
        //    return View(album);
        //}

        //[HttpPost, ActionName("Edit")]
        //public IActionResult EditPost(int? id, byte[] rowVersion)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }
        //    var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).AsNoTracking().Single(m => m.AlbumID == id);
        //    if (ModelState.IsValid)
        //    {
        //        _context.Albums.Update(album);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");
        //    ViewBag.Genres = new SelectList(_context.Genres, "GenreID", "Name");
        //    return View(album);
        //}

        //[HttpPost]
        //public IActionResult Edit(Album album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(album).State = EntityState.Modified;
                
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(album);
        //}

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Album album = _context.Albums.Single(m => m.AlbumID == id);
            _context.Albums.Remove(album);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

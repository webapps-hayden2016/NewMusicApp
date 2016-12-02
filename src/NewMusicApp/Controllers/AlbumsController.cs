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
        public IActionResult Index(string searchString, string sortOrder)
        {
            ViewBag.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.ArtistSort = sortOrder == "Artist" ? "artist_desc" : "Artist";
            ViewBag.GenreSort = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.PriceSort = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.LikesSort = sortOrder == "Likes" ? "likes_desc" : "Likes";

            var albums = from m in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.Title.Contains(searchString) || a.Artist.Name.Contains(searchString) || a.Genre.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    albums = albums.OrderByDescending(a => a.Title);
                    break;
                case "Artist":
                    albums = albums.OrderBy(a => a.Artist.Name);
                    break;
                case "artist_desc":
                    albums = albums.OrderByDescending(a => a.Artist.Name);
                    break;
                case "Genre":
                    albums = albums.OrderBy(a => a.Genre.Name);
                    break;
                case "genre_desc":
                    albums = albums.OrderByDescending(a => a.Genre.Name);
                    break;
                case "Price":
                    albums = albums.OrderBy(a => a.Price);
                    break;
                case "price_desc":
                    albums = albums.OrderByDescending(a => a.Price);
                    break;
                case "Likes":
                    albums = albums.OrderBy(a => a.Likes);
                    break;
                case "likes_desc":
                    albums = albums.OrderByDescending(a => a.Likes);
                    break;
                default:
                    albums = albums.OrderBy(a => a.Title);
                    break;
            }

            return View(albums.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Genre = new SelectList(_context.Genres, "GenreID", "Name");
            ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Album album, string artistName, string genreName)
        {
            ViewBag.Genre = new SelectList(_context.Genres, "GenreID", "Name");
            ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");

            if (artistName != null)
            {
                Artist artist = new Models.Artist();
                artist.Name = artistName;

                var artistCheck = _context.Artists.Any(a => a.Name == artist.Name);
                if (artistCheck == false)
                {
                    _context.Artists.Add(artist);
                    _context.SaveChanges();
                    var art = _context.Artists.Single(a => a.Name == artistName);
                    album.ArtistID = art.ArtistID;
                }
                else
                {
                    var art = _context.Artists.Single(a => a.Name == artistName);
                    album.ArtistID = art.ArtistID;
                }
            }

            if (genreName != null)
            {
                Genre genre = new Models.Genre();
                genre.Name = genreName;

                var genreCheck = _context.Genres.Any(g => g.Name == genre.Name);
                if (genreCheck == false)
                {
                    _context.Genres.Add(genre);
                    _context.SaveChanges();
                    var gen = _context.Genres.Single(g => g.Name == genreName);
                    album.GenreID = gen.GenreID;

                }
                else
                {
                    var gen = _context.Genres.Single(g => g.Name == genreName);
                    album.GenreID = gen.GenreID;
                }
                ModelState.Remove("ArtistID");
                ModelState.Remove("GenreID");

            }
            if (ModelState.IsValid)
            {
                album.AlbumPlaylists = new List<AlbumPlaylist>();
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            //else
            //{
            //    _context.Albums.Add(album);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(album);
        }

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

            ViewBag.AlbumTitle = album.Title;

            var album1 = _context.Albums.First(a => a.ArtistID == album.ArtistID && a.Title != album.Title);
            var album2 = _context.Albums.Last(a => a.ArtistID == album.ArtistID && a.Title != album.Title);

            if(album1 != null)
            {
                ViewBag.Recommended1 = album1.Title;
            }
            else
            {
                ViewBag.Recommended1 = "";
            }
            if (album2 != null)
            {
                ViewBag.Recommended2 = album2.Title;
            }
            else
            {
                ViewBag.Recommended2 = "";
            }
            

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).AsNoTracking().Single(m => m.AlbumID == id);
            ViewBag.AlbumTitle = album.Title;
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.Artists = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.Genres = new SelectList(_context.Genres, "GenreID", "Name");
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(album).State = EntityState.Modified;
                _context.Entry(album).Property(a => a.AlbumPlaylists).IsModified = false;
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).AsNoTracking().Single(m => m.AlbumID == id);
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirm(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).AsNoTracking().Single(m => m.AlbumID == id);
        //        _context.Albums.Remove(album);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        public IActionResult Likes(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).AsNoTracking().Single(m => m.AlbumID == id);
            if (ModelState.IsValid)
            {
                album.Likes++;
                _context.Entry(album).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

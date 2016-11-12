using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewMusicApp.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NewMusicApp.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly MusicDbContext _context;

        public PlaylistsController(MusicDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var playlists = _context.Playlists.ToList();
            return View(playlists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Playlists.Add(playlist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        public IActionResult AddToPlaylist(AlbumPlaylist ap)
        {

        }
    }
}

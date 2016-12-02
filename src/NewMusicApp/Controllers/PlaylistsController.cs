using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewMusicApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NewMusicApp.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlaylistsController(MusicDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var playlists = from p in _context.Playlists
                            where p.OwnerID == userId
                            select p;

            var user = _context.Users.Single(u => u.Id == userId);
            string date = user.DateJoined.ToString("MMM") + " " + user.DateJoined.Year;
            ViewBag.Date = date;

            var returnList = playlists.ToList();
            return View(returnList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Playlist playlist)
        {
            var userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                playlist.OwnerID = userId;
                playlist.AlbumPlaylists = new List<AlbumPlaylist>();
                _context.Playlists.Add(playlist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        public IActionResult Rename(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var playlist = _context.Playlists.Single(p => p.PlaylistID == id);
            return View(playlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rename(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                //var userId = _userManager.GetUserId(User);
                //playlist.OwnerID = userId;
                _context.Entry(playlist).State = EntityState.Modified;
                _context.Entry(playlist).Property(p => p.OwnerID).IsModified = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddToPlaylist(int? id)
        {
            var userId = _userManager.GetUserId(User);
            var playlistsList = from p in _context.Playlists
                                where p.OwnerID == userId
                                select p;

            ViewBag.Playlists = new SelectList(playlistsList.ToList(), "PlaylistID", "Name");
           
            if(id == null)
            {
                return NotFound();
            }

            ViewBag.AlbumTitle = _context.Albums.Single(a => a.AlbumID == id).Title;

            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToPlaylist(int id, Playlist playlist)
        {
            Album album = _context.Albums.Single(a => a.AlbumID == id);
            Playlist playlist2 = _context.Playlists.Single(p => p.PlaylistID == playlist.PlaylistID);

            AlbumPlaylist albumPlaylist = new AlbumPlaylist();
            albumPlaylist.AlbumID = album.AlbumID;
            albumPlaylist.PlaylistID = playlist.PlaylistID;
            _context.AlbumPlaylists.Add(albumPlaylist);
            UpdateAlbum(album, albumPlaylist);
            UpdatePlaylist(playlist2, albumPlaylist);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public void UpdatePlaylist(Playlist playlist, AlbumPlaylist albumPlay)
        {
            playlist.AlbumPlaylists.Add(albumPlay);
            _context.Entry(playlist).State = EntityState.Modified;
            _context.Entry(playlist).Property(p => p.OwnerID).IsModified = false;
            _context.Entry(playlist).Property(p => p.Name).IsModified = false;
            _context.SaveChanges();
        }

        public void UpdateAlbum(Album album, AlbumPlaylist albumPlay)
        {
            album.AlbumPlaylists.Add(albumPlay);
            _context.Entry(album).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public IActionResult Listing(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var playlist = _context.Playlists.Single(p => p.PlaylistID == id);
            ViewBag.Playlist = playlist.Name;
            if(playlist == null)
            {
                return NotFound();
            }

            var aps = _context.AlbumPlaylists.Include(ap => ap.Album).Where(ap => ap.PlaylistID == id);
            List<Album> albums = new List<Album>();
            foreach(var item in aps)
            {
                albums.Add(item.Album);
            }
            return View(albums);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var playlist = _context.Playlists.Single(p => p.PlaylistID == id);
            var albumPlaylists = from p in _context.AlbumPlaylists
                                where p.PlaylistID == id
                                select p;

            _context.Playlists.Remove(playlist);
            foreach(var aps in albumPlaylists)
            {
                _context.AlbumPlaylists.Remove(aps);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

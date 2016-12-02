using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace NewMusicApp.Models
{
    public class MusicDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options)
            : base(options)
        {

        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<AlbumPlaylist> AlbumPlaylists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AlbumPlaylist>()
                .HasKey(t => new { t.AlbumID, t.PlaylistID });

            modelBuilder.Entity<AlbumPlaylist>()
                .HasOne(ap => ap.Album)
                .WithMany(a => a.AlbumPlaylists)
                .HasForeignKey(ap => ap.AlbumID);

            modelBuilder.Entity<AlbumPlaylist>()
                .HasOne(ap => ap.Playlist)
                .WithMany(p => p.AlbumPlaylists)
                .HasForeignKey(ap => ap.PlaylistID);
        }
    }
}

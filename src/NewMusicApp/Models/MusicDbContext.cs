﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace NewMusicApp.Models
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options)
            : base(options)
        {

        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}

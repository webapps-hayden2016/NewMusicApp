using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.Models
{
    public class Album
    {
        public int AlbumID { get; set; }

        [Required (ErrorMessage = "Please enter the album title!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a price!")]
        [Range(0.01, 100.00, ErrorMessage = "Must be a price between {1} and {2}!")]
        public decimal Price { get; set; }

        // Foreign key
        public int ArtistID { get; set; }
        // Navigation property
        public Artist Artist { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
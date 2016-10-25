using System.ComponentModel.DataAnnotations;

namespace NewMusicApp.Models
{
    public class Genre
    {
        public int GenreID { get; set; }

        [Required(ErrorMessage = "Please enter a genre name!")]
        [StringLength(20, ErrorMessage = "Genre name must be between 1 and 20 characters!")]
        public string Name { get; set; }
    }
}

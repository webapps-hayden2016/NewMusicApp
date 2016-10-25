using System.ComponentModel.DataAnnotations;

namespace NewMusicApp.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }

        [Required (ErrorMessage = "Please enter the artist's name!")]
        public string Name { get; set; }

        public string Bio { get; set; }
    }
}
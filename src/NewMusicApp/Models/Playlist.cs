using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        //[ForeignKey("ApplicationUser")]
        public string OwnerID { get; set; }

        public List<AlbumPlaylist> AlbumPlaylists { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        
        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public int OwnerID { get; set; }

        public List<AlbumPlaylist> AlbumPlaylists { get; set; }

    }
}

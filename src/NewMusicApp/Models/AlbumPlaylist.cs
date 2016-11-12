using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicApp.Models
{
    public class AlbumPlaylist
    {
        public int AlbumID { get; set; }
        public Album Album { get; set; }

        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyRoad.Data
{
    public class FavoritePath
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Path))]
        public int PathId { get; set; }
        public virtual Path Path { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Favorite))]
        public int FavoriteId { get; set; }
        public virtual Favorite Favorite { get; set; }
    }
}

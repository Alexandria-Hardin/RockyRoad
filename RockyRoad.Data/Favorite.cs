using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyRoad.Data
{
    public class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<FavoritePath> FavoritePaths { get; set; } = new List<FavoritePath>();
        
        //application user connection
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;

namespace RockyRoad.Models
{
    public class FavoriteDetail
    {
        public int FavoriteId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FavoritePath> FavoritePaths { get; set; } 
        public int ClimberId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey(nameof(Climber))]
        public int ClimberId { get; set; }
        public virtual Climber Climber { get; set; }
    }
}

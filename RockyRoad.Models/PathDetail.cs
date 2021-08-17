using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using static RockyRoad.Data.Path;

namespace RockyRoad.Models
{
    public class PathDetail
    {
        public int PathId { get; set; }
       
        public string Name { get; set; }
       
        public RouteType TypeOfRoute { get; set; }
        
        public DifficultyLevel LevelOfDifficulty { get; set; }
        public virtual ICollection<FavoritePath> ListOfFavoritePaths { get; set; } 
      
        public int LocationId { get; set; }
    }
}

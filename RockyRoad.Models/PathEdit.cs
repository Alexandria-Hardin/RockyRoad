using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RockyRoad.Data.Path;

namespace RockyRoad.Models
{
    public class PathEdit
    {
        public int PathId { get; set; }
        public string Name { get; set; }

        public RouteType TypeOfRoute { get; set; }

        public DifficultyLevel LevelOfDifficulty { get; set; }
        public int LocationId { get; set; }
    }
}

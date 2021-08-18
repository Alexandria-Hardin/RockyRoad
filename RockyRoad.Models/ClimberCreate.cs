using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using static RockyRoad.Data.Climber;

namespace RockyRoad.Models
{
    public class ClimberCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ExperienceLevel LevelOfExperience { get; set; }
        public ApplicationUser User { get; set; }
    }
}

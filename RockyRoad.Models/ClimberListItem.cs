using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;

namespace RockyRoad.Models
{
    public class ClimberListItem
    {
        public string ClimberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser User { get; set; }
    }
}

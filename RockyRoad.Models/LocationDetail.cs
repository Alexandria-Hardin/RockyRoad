using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;

namespace RockyRoad.Models
{
    public class LocationDetail
    {
        public int LocationId { get; set; }
       
        public string State { get; set; }
        
        public string City { get; set; }
       
        public decimal Lattitude { get; set; }
       
        public decimal Longitude { get; set; }
        public string Address { get; set; }
       
        public string Description { get; set; }
        public virtual ICollection<Path> ClimbingPaths { get; set; }
    }
}

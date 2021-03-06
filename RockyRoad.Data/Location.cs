using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyRoad.Data
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public decimal Lattitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        //make sure to look into address being null 
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Path> ClimbingPaths { get; set; } = new List<Path>();
    }
}

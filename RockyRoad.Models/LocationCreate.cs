using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyRoad.Models
{
    public class LocationCreate
    {
        public string State { get; set; }
       
        public string City { get; set; }
       
        public decimal Lattitude { get; set; }
   
        public decimal Longitude { get; set; }
        public string Address { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RockyRoad.Data
{
    public class Climber 
    {
        public enum ExperienceLevel
        {
            Beginner,
            Intermediate, 
            Skilled,
            Advanced,
            Expert
        }
        [Key, ForeignKey("User")]
        public string ClimberId { get; set; }
        //[Required]
        //public string Username { get; set; }
        //[Required]
        //public string Email { get; set; }
        //[Required]
        ////figure out if password is encrypted 
        //public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public ExperienceLevel LevelOfExperience { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual ApplicationUser User { get; set; }

    }
}

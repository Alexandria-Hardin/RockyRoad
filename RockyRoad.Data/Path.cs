using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RockyRoad.Data
{
    public class Path
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RouteType
        {
            [Display(Name = "Bouldering")]
            Bouldering,
            [Display(Name = "Top Rope")]
            TopRope,
            [Display(Name = "Lead")]
            Lead
        }
        public enum DifficultyLevel
        {
            [Display(Name = "5.1-5.4")]
            Easy,
            [Display(Name = "5.5-5.8")]
            Intermediate,
            [Display(Name = "5.9-5.10")]
            Hard,
            [Display(Name = "5.11-5.12")]
            HardtoDifficult,
            [Display(Name = "5.13-5.15")]
            VeryDifficult
        }

        [Key]
        public int PathId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public RouteType TypeOfRoute { get; set; }
        [Required]
        public DifficultyLevel LevelOfDifficulty { get; set; }
        public virtual ICollection<FavoritePath> ListOfFavoritePaths { get; set; } = new List<FavoritePath>();
        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

    }
}

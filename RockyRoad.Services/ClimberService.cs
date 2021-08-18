using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using RockyRoad.Models;

namespace RockyRoad.Services
{
    public class ClimberService
    {
        public bool CreateClimber(ClimberCreate model)
        {
            var entity =
                new Climber()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    LevelOfExperience = model.LevelOfExperience,
                    User = model.User,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Climbers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ClimberListItem> GetClimbers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Climbers
                    .Select(
                        e =>
                        new ClimberListItem
                        {
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            User = e.User,
                            ClimberId = e.ClimberId
                        }
                        );
                return query.ToArray();
            }
        }
        public ClimberDetail GetClimberById(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Climbers
                    .Single(e => e.ClimberId == id);
                return
                    new ClimberDetail
                    {
                        ClimberId = entity.ClimberId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        LevelOfExperience = entity.LevelOfExperience,
                        Favorites = entity.Favorites,
                        User = entity.User
                    };
            }
        }

        public bool UpdateClimber(ClimberEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Climbers
                    .Single(e => e.ClimberId == model.ClimberId);
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.LevelOfExperience = model.LevelOfExperience;
                entity.User = model.User;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteClimber(string climberId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Climbers
                    .Single(e => e.ClimberId == climberId);
                ctx.Climbers.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

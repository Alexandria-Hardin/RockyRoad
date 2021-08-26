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
                    UserId = model.UserId,
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
                            UserId = e.UserId,
                            ClimberId = e.ClimberId
                        }
                        );
                return query.ToArray();
            }
        }
        public ClimberDetail GetClimberById(int id)
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
                        UserId = entity.UserId
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
                entity.UserId = model.UserId;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteClimber(int climberId)
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

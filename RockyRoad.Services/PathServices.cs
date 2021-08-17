using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using RockyRoad.Models;
using static RockyRoad.Data.Path;

namespace RockyRoad.Services
{
    public class PathServices
    {
        public bool CreatePath(PathCreate model)
        {
            var entity =
                new Path()
                {
                    Name = model.Name,
                    TypeOfRoute = model.TypeOfRoute,
                    LevelOfDifficulty = model.LevelOfDifficulty,
                    LocationId = model.LocationId,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Paths.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PathListItem> GetPaths()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Paths
                    .Select(
                        e =>
                        new PathListItem
                        {
                            PathId = e.PathId,
                            Name = e.Name,
                            LocationId = e.LocationId
                        }
                        );
                return query.ToArray();
            }
        }
        public PathDetail GetPathById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Paths
                    .Single(e => e.PathId == id);
                return
                    new PathDetail
                    {
                        PathId = entity.PathId,
                        Name= entity.Name,
                        TypeOfRoute = entity.TypeOfRoute,
                        LevelOfDifficulty = entity.LevelOfDifficulty,
                        LocationId = entity.LocationId,
                        ListOfFavoritePaths = entity.ListOfFavoritePaths
                    };
            }
        }

        public PathDetail GetPathByDifficulty(DifficultyLevel levelOfDifficulty)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Paths
                        .Single(e => e.LevelOfDifficulty == levelOfDifficulty);
                return
                    new PathDetail
                    {
                        PathId = entity.PathId,
                        Name = entity.Name,
                        TypeOfRoute = entity.TypeOfRoute,
                        LevelOfDifficulty = entity.LevelOfDifficulty,
                        LocationId = entity.LocationId,
                        ListOfFavoritePaths = entity.ListOfFavoritePaths
                    };
            }
        }
        public bool UpdatePath(PathEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Paths
                    .Single(e => e.PathId == model.PathId);
                entity.Name = model.Name;
                entity.TypeOfRoute = model.TypeOfRoute;
                entity.LevelOfDifficulty = model.LevelOfDifficulty;
                entity.LocationId = model.LocationId;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePath(int pathId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Paths
                    .Single(e => e.PathId == pathId);
                ctx.Paths.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
    


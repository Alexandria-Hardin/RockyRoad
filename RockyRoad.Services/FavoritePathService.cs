using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using RockyRoad.Models;

namespace RockyRoad.Services
{
    public class FavoritePathService
    {
        public bool CreateFavoritePath(FavoritePathCreate model)
        {
            var entity =
                new FavoritePath()
                {
                    PathId = model.PathId,
                    FavoriteId = model.FavoriteId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.FavoritePaths.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FavoritePathListItem> GetFavoritePaths()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .FavoritePaths
                    .Select(
                        e =>
                        new FavoritePathListItem
                        {
                            PathId = e.PathId,
                            FavoriteId = e.FavoriteId 
                        }
                        );
                return query.ToArray();
            }
        }

        public bool DeletePathFromFavorites(int pathId, int favoriteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var foundPath = ctx.FavoritePaths.Single(s => s.PathId == pathId && s.FavoriteId == favoriteId);

                ctx.FavoritePaths.Remove(foundPath);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

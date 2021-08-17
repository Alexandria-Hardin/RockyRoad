using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using RockyRoad.Models;

namespace RockyRoad.Services
{
    public class FavoriteService
    {
        private readonly string _climberId;
        public FavoriteService(string climberId)
        {
            _climberId = climberId;
        }
        public bool CreateFavorite(FavoriteCreate model)
        {
            var entity =
                new Favorite()
                {
                    ClimberId = _climberId,
                    Name = model.Name
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Favorites.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<FavoriteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Favorites
                    .Where(e => e.ClimberId == _climberId)
                    .Select(
                        e =>
                        new FavoriteListItem
                        {
                            FavoriteId = e.FavoriteId,
                            Name = e.Name,
                            ClimberId = e.ClimberId
                        }
                        );
                return query.ToArray();
            }
        }
        public FavoriteDetail GetFavoriteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Favorites
                    .Single(e => e.FavoriteId == id && e.ClimberId == _climberId);
                return
                    new FavoriteDetail
                    {
                        FavoriteId = entity.FavoriteId,
                        Name = entity.Name,
                        ClimberId= entity.ClimberId,
                        FavoritePaths = entity.FavoritePaths
                    };
            }
        }
        public FavoriteDetail GetFavoriteByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Favorites
                        .Single(e => e.Name == name);
                return
                    new FavoriteDetail
                    {

                        FavoriteId = entity.FavoriteId,
                        Name = entity.Name,
                        ClimberId = entity.ClimberId,
                        FavoritePaths = entity.FavoritePaths
                    };
            }
        }
        public bool UpdateFavorite(FavoriteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Favorites
                    .Single(e => e.FavoriteId == model.FavoriteId && e.ClimberId == _climberId);
                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteFavorite(int favoriteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Favorites
                    .Single(e => e.FavoriteId == favoriteId && e.ClimberId == _climberId);
                ctx.Favorites.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

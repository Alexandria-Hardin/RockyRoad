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
        private readonly string _userId;
        public FavoriteService(string userId)
        {
            _userId = userId;
        }

        public bool CreateFavorite(FavoriteCreate model)
        {
            var entity =
                new Favorite()
                {
                    UserId = _userId,
                    Name = model.Name
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Favorites.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<FavoriteListItem> GetFavorites()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Favorites
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new FavoriteListItem
                        {
                            FavoriteId = e.FavoriteId,
                            Name = e.Name,
                            UserId = e.UserId
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
                    .Single(e => e.FavoriteId == id && e.UserId == _userId);
                return
                    new FavoriteDetail
                    {
                        FavoriteId = entity.FavoriteId,
                        Name = entity.Name,
                        UserId= entity.UserId,
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
                        UserId = entity.UserId,
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
                    .Single(e => e.FavoriteId == model.FavoriteId && e.UserId == _userId);
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
                    .Single(e => e.FavoriteId == favoriteId && e.UserId == _userId);
                ctx.Favorites.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

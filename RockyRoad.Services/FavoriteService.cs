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
        public bool CreateFavorite(FavoriteCreate model)
        {
            var entity =
                new Favorite()
                {
                    Name = model.Name,
                    ClimberId = model.ClimberId
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
                    .Single(e => e.FavoriteId == id);
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
                    .Single(e => e.FavoriteId == model.FavoriteId);
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
                    .Single(e => e.FavoriteId == favoriteId);
                ctx.Favorites.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Joining Table
        //public bool CreateFavoritePath(FavoritePathCreate model)
        //{
        //    var entity =
        //        new FavoritePath()
        //        {
        //            PathId = model.PathId,
        //            FavoriteId = model.FavoriteId
        //        };
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        ctx.FavoritePaths.Add(entity);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        //public bool DeletePathFromFavorites(int pathId, int favoriteId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var foundPath = ctx.FavoritePaths.Single(s => s.PathId == pathId && s.FavoriteId == favoriteId);

        //        ctx.FavoritePaths.Remove(foundPath);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockyRoad.Data;
using RockyRoad.Models;

namespace RockyRoad.Services
{
    public class LocationServices
    {
        public bool CreateLocation(LocationCreate model)
        {
            var entity =
                new Location()
                {
                    State = model.State,
                    City = model.City,
                    Lattitude = model.Lattitude,
                    Longitude = model.Longitude,
                    Description = model.Description
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Locations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<LocationListItem> GetLocations()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Locations
                    .Select(
                        e =>
                        new LocationListItem
                        {
                           LocationId = e.LocationId,
                           State = e.State,
                           City = e.City,
                           Lattitude = e.Lattitude,
                           Longitude = e.Longitude
                        }
                        );
                return query.ToArray();
            }
        }
        public LocationDetail GetLocationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == id);
                return
                    new LocationDetail
                    {
                        LocationId = entity.LocationId,
                        State = entity.State,
                        City = entity.City,
                        Lattitude = entity.Lattitude,
                        Longitude = entity.Longitude,
                        Address = entity.Address,
                        Description = entity.Description,
                        ClimbingPaths = entity.ClimbingPaths
                    };
            }
        }

        public LocationDetail GetSongByState(string state)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Locations
                        .Single(e => e.State == state);
                return
                    new LocationDetail
                    {
                        LocationId = entity.LocationId,
                        State = entity.State,
                        City = entity.City,
                        Lattitude = entity.Lattitude,
                        Longitude = entity.Longitude,
                        Address = entity.Address,
                        Description = entity.Description,
                        ClimbingPaths = entity.ClimbingPaths
                    };
            }
        }
        public bool UpdateLocation(LocationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == model.LocationId);
                entity.State = model.State;
                entity.City = model.City;
                entity.Lattitude = model.Lattitude;
                entity.Longitude = model.Longitude;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteLocation(int locationId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == locationId);
                ctx.Locations.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

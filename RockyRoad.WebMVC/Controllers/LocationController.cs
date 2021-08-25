using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockyRoad.Models;
using RockyRoad.Services;

namespace RockyRoad.WebMVC.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            var model = new LocationListItem[0];
            return View(model);
        }

        //GET: Location
        //Get/Location/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateLocationService();

            if (service.CreateLocation(model))
            {
                TempData["SaveResult"] = "Your location was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Location could not be added.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateLocationService();
            var model = svc.GetLocationById(id);

            return View(model);
        }
        public ActionResult Details(string state)
        {
            var svc = CreateLocationService();
            var model = svc.GetLocationByState(state);

            return View(model);
        }
        private LocationService CreateLocationService()
        {
            var service = new LocationService();
            return service;
        }
        public ActionResult Edit(int id)
        {
            var service = CreateLocationService();
            var detail = service.GetLocationById(id);
            var model =
                new LocationEdit
                {
                    State = detail.State,
                    City = detail.City,
                    Lattitude = detail.Lattitude,
                    Longitude = detail.Longitude,
                    Description = detail.Description
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.LocationId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateLocationService();
            if (service.UpdateLocation(model))
            {
                TempData["SaveResult"] = "The location was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The location could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateLocationService();
            var model = svc.GetLocationById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateLocationService();
            service.DeleteLocation(id);
            TempData["SaveResult"] = "Your location was deleted";
            return RedirectToAction("Index");
        }

    }
}
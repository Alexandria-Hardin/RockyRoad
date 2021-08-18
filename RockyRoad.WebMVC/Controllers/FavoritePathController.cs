using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockyRoad.Models;
using RockyRoad.Services;

namespace RockyRoad.WebMVC.Controllers
{
    public class FavoritePathController : Controller
    {
        // GET: FavoritePath
        public ActionResult Index()
        {
            return View();
        }
        //GET: FavoritePath
        //Get/Favorite/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FavoritePathCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFavoritePathService();

            if (service.CreateFavoritePath(model))
            {
                TempData["SaveResult"] = "Your path was added to favorites.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Path could not be added to favorites.");
            return View(model);
        }
        private FavoritePathService CreateFavoritePathService()
        {
            var service = new FavoritePathService();
            return service;
        }
        [ActionName("Delete")]
        public ActionResult Delete(int pathId, int favoriteId)
        {
            var svc = CreateFavoritePathService();
            var model = svc.DeletePathFromFavorites(pathId, favoriteId);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int pathId, int favoriteId)
        {
            var service = CreateFavoritePathService();
            service.DeletePathFromFavorites(pathId, favoriteId);
            TempData["SaveResult"] = "One of your favorite paths was deleted";
            return RedirectToAction("Index");
        }

    }
}
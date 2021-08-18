using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RockyRoad.Models;
using RockyRoad.Services;

namespace RockyRoad.WebMVC.Controllers
{
    public class FavoriteController : Controller
    {
        // GET: Favorite
        public ActionResult Index()
        {
            return View();
        }

        //GET: Favorite
        //Get/Favorite/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FavoriteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFavoriteService();

            if (service.CreateFavorite(model))
            {
                TempData["SaveResult"] = "Your favorites list was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Favorites list could not be added.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateFavoriteService();
            var model = svc.GetFavoriteById(id);

            return View(model);
        }
        public ActionResult Details(string name)
        {
            var svc = CreateFavoriteService();
            var model = svc.GetFavoriteByName(name);

            return View(model);
        }
        private FavoriteService CreateFavoriteService()
        {
            //figure out how to connect climberId
            var climberId = User.Identity.GetUserId();
            var service = new FavoriteService(climberId);
            return service;
        }
        public ActionResult Edit(int id)
        {
            var service = CreateFavoriteService();
            var detail = service.GetFavoriteById(id);
            var model =
                new FavoriteEdit
                {
                    FavoriteId = detail.FavoriteId,
                    Name = detail.Name
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FavoriteEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.FavoriteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateFavoriteService();
            if (service.UpdateFavorite(model))
            {
                TempData["SaveResult"] = "The favorites list was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The favorites list could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateFavoriteService();
            var model = svc.GetFavoriteById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateFavoriteService();
            service.DeleteFavorite(id);
            TempData["SaveResult"] = "Your favorites list was deleted";
            return RedirectToAction("Index");
        }
    }
}
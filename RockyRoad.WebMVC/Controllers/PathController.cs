using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockyRoad.Models;
using RockyRoad.Services;
using static RockyRoad.Data.Path;

namespace RockyRoad.WebMVC.Controllers
{
    public class PathController : Controller
    {
        // GET: Path
        public ActionResult Index()
        {
            return View();
        }

        //GET: Path
        //Get/Path/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PathCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePathService();

            if (service.CreatePath(model))
            {
                TempData["SaveResult"] = "Your path was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Path could not be added.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreatePathService();
            var model = svc.GetPathById(id);

            return View(model);
        }
        public ActionResult Details(DifficultyLevel levelOfDifficulty)
        {
            var svc = CreatePathService();
            var model = svc.GetPathByDifficulty(levelOfDifficulty);

            return View(model);
        }
        private PathService CreatePathService()
        {
            var service = new PathService();
            return service;
        }
        public ActionResult Edit(int id)
        {
            var service = CreatePathService();
            var detail = service.GetPathById(id);
            var model =
                new PathEdit
                {
                    Name = detail.Name,
                    TypeOfRoute = detail.TypeOfRoute,
                    LevelOfDifficulty = detail.LevelOfDifficulty,
                    LocationId = detail.LocationId
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PathEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.PathId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreatePathService();
            if (service.UpdatePath(model))
            {
                TempData["SaveResult"] = "The path was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The path could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreatePathService();
            var model = svc.GetPathById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePathService();
            service.DeletePath(id);
            TempData["SaveResult"] = "Your path was deleted";
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RockyRoad.Data;
using RockyRoad.Models;
using RockyRoad.Services;

namespace RockyRoad.WebMVC.Controllers
{
    public class ClimberController : Controller
    {
        // GET: Climber
        public ActionResult Index()
        {
            var model = new ClimberListItem[0];
            return View(model);
        }
        public ActionResult Create()
        {
            return View();

        }
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Climber c)
        {
            var _userId = User.Identity.GetUserId();
            if (c == null)
            {
                c = db.Climbers.Create();
                db.Climbers.Add(c);
            }
            c.FirstName = c.FirstName;
            c.LastName = c.LastName;
            c.LevelOfExperience = c.LevelOfExperience;
            c.UserId = c.UserId;

            return View();

        }

        public ActionResult Details(int id)
        {
            var svc = CreateClimberService();
            var model = svc.GetClimberById(id);

            return View(model);
        }

        private ClimberService CreateClimberService()
        {
            var service = new ClimberService();
            return service;
        }

        public ActionResult Edit(int id)
        {
            var service = CreateClimberService();
            var detail = service.GetClimberById(id);
            var model =
                new ClimberEdit
                {
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    LevelOfExperience = detail.LevelOfExperience,
                    UserId = detail.UserId
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClimberEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ClimberId == id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateClimberService();
            if (service.UpdateClimber(model))
            {
                TempData["SaveResult"] = "The climbing profile was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The climbing profile could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateClimberService();
            var model = svc.GetClimberById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateClimberService();
            service.DeleteClimber(id);
            TempData["SaveResult"] = "Your climbing profile was deleted";
            return RedirectToAction("Index");
        }
    }
}
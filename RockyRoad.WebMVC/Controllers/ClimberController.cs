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
        public ActionResult AddClimber()
        {
            return View();

        }
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClimber(Climber c)
        {
            var currentUserId = User.Identity.GetUserId();
            var climberinfo = db.Climbers.FirstOrDefault(d => d.ClimberId == currentUserId);
            if (climberinfo == null)
            {
                climberinfo = db.Climbers.Create();
                climberinfo.ClimberId = currentUserId;
                db.Climbers.Add(climberinfo);
            }
            climberinfo.FirstName = c.FirstName;
            climberinfo.LastName = c.LastName;
            climberinfo.LevelOfExperience = c.LevelOfExperience;
            climberinfo.Favorites = c.Favorites;
            climberinfo.User = c.User;
            db.SaveChanges();

            return View();

        }

        public ActionResult Details(string id)
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

        public ActionResult Edit(string id)
        {
            var service = CreateClimberService();
            var detail = service.GetClimberById(id);
            var model =
                new ClimberEdit
                {
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    LevelOfExperience = detail.LevelOfExperience,
                    User = detail.User
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, ClimberEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ClimberId != id)
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
        public ActionResult Delete(string id)
        {
            var svc = CreateClimberService();
            var model = svc.GetClimberById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(string id)
        {
            var service = CreateClimberService();
            service.DeleteClimber(id);
            TempData["SaveResult"] = "Your climbing profile was deleted";
            return RedirectToAction("Index");
        }
    }
}
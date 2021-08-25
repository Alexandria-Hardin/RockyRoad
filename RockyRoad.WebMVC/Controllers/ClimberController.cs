using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RockyRoad.Data;
using RockyRoad.Models;

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

    }
}
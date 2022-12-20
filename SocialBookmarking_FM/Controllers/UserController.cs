﻿using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;

namespace SocialBookmarking_FM.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        public UserController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Bookmarks(string uname)
        {
            var uid = db.Users.Where(x => x.UserName == uname).ToList()[0].Id;
            var bkm = db.Bookmarks.Where(x => x.UserId == uid).OrderByDescending(x => x.Date).ToList();

            ViewBag.bkm = bkm;
            ViewBag.user = db.Users.Where(x => x.UserName == uname).ToList()[0];
            
            ViewBag.userRole = (from x in db.UserRoles
                                where x.UserId == uid
                                join y in db.Roles
                                on x.RoleId equals y.Id

                                select new { name = y.Name }).ToList()[0].name;
            return View();
        }
    }
}

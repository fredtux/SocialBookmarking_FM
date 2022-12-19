﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Data;

namespace SocialBookmarking_FM.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

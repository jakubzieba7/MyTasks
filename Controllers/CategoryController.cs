﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Service;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private CategoryRepository _categoryRepository;

        public CategoryController(ApplicationDbContext _context)
        {
            _categoryRepository = new CategoryRepository(_context);
        }


        public IActionResult Categories()
        {
            var userId = User.GetUserId();

            var vm = new TasksViewModel
            {
                Categories = _categoryRepository.GetCategories(userId)
            };

            return View(vm);
        }

        public IActionResult Category(int id = 0)
        {
            var userId = User.GetUserId();
            var category = id == 0 ? new Category { Id = 0, UserId = userId } : _categoryRepository.Get(id, userId);

            var vm = new CategoryViewModel
            {
                Category = category,
                Heading = id == 0 ? "Dodawanie nowej kategorii" : "Edytowanie kategorii"
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new CategoryViewModel
                {
                    Category = category,
                    Heading = category.Id == 0 ? "Dodawanie nowej kategorii" : "Edytowanie kategorii"
                };

                return View("Category", vm);

            }

            if (category.Id == 0)
                _categoryRepository.Add(category);
            else
                _categoryRepository.Update(category);

            return RedirectToAction("Categories");
        }

    }
}

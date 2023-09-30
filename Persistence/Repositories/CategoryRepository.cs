﻿using MyTasks.Core.Models.Domains;
using MyTasks.Persistence;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories.Where(x => x.UserId == userId).ToList();
        }

        public Category Get(int id, string userId)
        {
            return _context.Categories.Single(x => x.UserId == userId && x.Id == id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id && x.UserId == category.UserId);

            categoryToUpdate.Name = category.Name;

            _context.SaveChanges();
        }

    }
}

using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using MyTasks.Core.Service;
using MyTasks.Persistence;
using System.Diagnostics.Metrics;
using System.Net.Mail;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IApplicationDbContext _context;

        public CategoryRepository(IApplicationDbContext context)
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
            var categories = _context.Categories.Where(x => x.UserId == category.UserId);
            var categoryLp = categories.Any() == true ? categories.ToList().Select(x => x.Lp).Last() : 0;

            category.Lp = categoryLp + 1;

            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id && x.UserId == category.UserId);

            categoryToUpdate.Name = category.Name;
        }

        public void UpdateId(string userId)
        {
            var categories = _context.Categories.Where(x => x.UserId == userId);
            var counter = 1;

            foreach (var category in categories)
            {
                category.Lp = counter++;
            }
        }

        public void Delete(int id, string userId)
        {
            var categoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);
            var categories = _context.Categories.Where(x => x.UserId == userId).ToList();

            _context.Categories.Remove(categoryToDelete);
        }

        public void AddDefaultCategory(string userId)
        {
            bool IsAnyCategoryExist = _context.Categories.Where(x => x.UserId == userId).Any() == true ? true : false;
            Category defaultCategory = new Category()
            {
                UserId = userId,
                Name = "Default",
            };

            if (!IsAnyCategoryExist)
                _context.Categories.Add(defaultCategory);
        }
    }
}

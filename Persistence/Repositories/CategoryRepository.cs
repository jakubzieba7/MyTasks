using MyTasks.Core.Models.Domains;
using MyTasks.Persistence;
using System.Net.Mail;

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
            AddDefaultCategory(userId);
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
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id && x.UserId == category.UserId);

            categoryToUpdate.Name = category.Name;

            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var categoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);
            var categories = _context.Categories.Where(x => x.UserId == userId).ToList();
            var counter = 1;

            _context.Categories.Remove(categoryToDelete);


            foreach (var category in categories)
            {
                category.Lp = counter++;
            }

            _context.SaveChanges();
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

            _context.SaveChanges();
        }
    }
}

using MyTasks.Core.Models.Domains;
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
    }
}

using MyTasks.Core.Models.Domains;
using MyTasks.Persistence;

namespace MyTasks.Core.Service
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetCategories(string userId);

        public Category Get(int id, string userId);

        public void Add(Category category);

        public void Update(Category category);

        public void Delete(int id, string userId);

        public void AddDefaultCategory(string userId);
    }
}

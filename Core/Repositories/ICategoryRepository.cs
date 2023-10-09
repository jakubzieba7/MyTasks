using Microsoft.EntityFrameworkCore;
using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(string userId);

        Category Get(int id, string userId);

        void Add(Category category);

        void Update(Category category);
        public void UpdateId(string userId);

        void Delete(int id, string userId);

        void AddDefaultCategory(string userId);
    }
}

using MyTasks.Core.Models.Domains;
using Task = MyTasks.Core.Models.Domains.Task;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository
    {
        private ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task Get(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void Add(Task task)
        {
            throw new NotImplementedException();
        }

        public void Update(Task task)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void Finish(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

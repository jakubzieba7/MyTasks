using MyTasks.Core.Models.Domains;
using Task = MyTasks.Core.Models.Domains.Task;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository
    {
        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}

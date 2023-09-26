using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;
using System.Security.Claims;
using Task = MyTasks.Core.Models.Domains.Task;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private UnitOfWork _unitOfWork;

        public TaskController(ApplicationDbContext context)
        {
            _unitOfWork=new UnitOfWork(context);
        }

        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var vm = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Tasks = _unitOfWork.Task.Get(userId),
                Categories = _unitOfWork.Task.GetCategories()
            };

            return View(vm);
        }

        public IActionResult Task(int id = 0)
        {
            var userId = User.GetUserId();
            var task = id == 0 ? new Task { Id = 0, UserId = userId, Term = DateTime.Now } : _unitOfWork.Task.Get(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Categories = _unitOfWork.Task.GetCategories(),
                Heading = id == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania"
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();
            var tasks = _unitOfWork.Task.Get(userId, viewModel.FilterTasks.IsExecuted, viewModel.FilterTasks.CategoryId, viewModel.FilterTasks.Title);

            return PartialView("_TasksTable", tasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Categories = _unitOfWork.Task.GetCategories(),
                    Heading = task.Id == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania"
                };

                return View("Task", vm);

            }

            if (task.Id == 0)
                _unitOfWork.Task.Add(task);
            else
                _unitOfWork.Task.Update(task);

            _unitOfWork.Complete();

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _unitOfWork.Task.Delete(id, userId);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true});
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _unitOfWork.Task.Finish(id, userId);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

using ToDoListApp.ViewModels;

namespace ToDoListApp.Controllers
{
    public class DeadlineController : Controller
    {
        private readonly AppDbContext _context;

        public DeadlineController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Deadlinega 1 kun qolgan tasklarni aniqlash uchun target date hisoblaymiz
            DateTime targetDate = DateTime.Today.AddDays(1);

            // Tasklar jadvalidagi deadline sanasi targetDate ga teng bo'lgan tasklarni olamiz
            var tasks = await _context.ToDoTasks
                .Where(t => t.Deadline.Date == targetDate)
                .ToListAsync();

            // Har bir task uchun xabar tuzib, view modelga o'tkazamiz
            var viewModel = tasks.Select(t => new DeadlineViewModel
            {
                TaskTitle = t.Name,
                Message = $"{t.Title} vazifasi deadline yaqinlashmoqda!"
            }).ToList();

            return View(viewModel);
        }
    }
}

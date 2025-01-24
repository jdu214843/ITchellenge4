using ToDoListApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

public class TaskController : Controller
{
    private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Task/Index
    public IActionResult Index()
    {
        var tasks = _context.ToDoTasks.ToList();
        return View(tasks);
    }

    // GET: Task/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Task/Create
    [HttpPost]
    public IActionResult Create(ToDoTask task, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            // Handle image upload
            if (image != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                task.ImagePath = "/images/" + image.FileName;
            }

            // Add the task to the database
            _context.ToDoTasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(task);
    }

    // GET: Task/Delete/{id}
    public IActionResult Delete(int id)
    {
        var task = _context.ToDoTasks.Find(id);
        if (task != null)
        {
            _context.ToDoTasks.Remove(task);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // GET: Task/Edit/{id}
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _context.ToDoTasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    // POST: Task/Edit/{id}
    [HttpPost]
    public IActionResult Edit(int id, ToDoTask task, IFormFile? image)
    {
        if (id != task.Id)
        {
            return NotFound();
        }

        var existingTask = _context.ToDoTasks.Find(id);
        if (existingTask == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            // Log errors to the console for debugging
            Console.WriteLine("ModelState errors:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(task);
        }

        // Handle image upload (update image if new one is provided)
        if (image != null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            existingTask.ImagePath = "/images/" + image.FileName;
        }

        // Update other fields
        existingTask.Name = task.Name;
        existingTask.Description = task.Description;
        existingTask.Date = task.Date;
        existingTask.Deadline = task.Deadline; // Ensure Deadline is updated
        existingTask.IsCompleted = task.IsCompleted;
        existingTask.Priority = task.Priority;

        // Update the task in the database
        _context.ToDoTasks.Update(existingTask);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}

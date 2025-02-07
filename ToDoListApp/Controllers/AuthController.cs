using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using ToDoListApp.Models;
using System.Linq;

public class AuthController : Controller
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    // Sign Up (GET)
    [HttpGet("signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    // Sign Up (POST)
    [HttpPost("signup")]
    public IActionResult SignUp(string username, string password, string email)
    {
        if (_context.Users.Any(u => u.Username == username || u.Email == email))
        {
            ModelState.AddModelError("", "Username or email already exists.");
            return View();
        }

        using var sha256 = SHA256.Create();
        var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));

        var user = new User
        {
            Username = username,
            PasswordHash = passwordHash,
            Email = email
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("SignIn");
    }

    // Sign In (GET)
    [HttpGet("signin")]
    public IActionResult SignIn()
    {
        return View();
    }

    // Sign In (POST)
    [HttpPost("signin")]
    public IActionResult SignIn(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        using var sha256 = SHA256.Create();
        var passwordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));

        if (user.PasswordHash != passwordHash)
        {  
            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        // Session saqlash
        HttpContext.Session.SetString("Username", user.Username);

        return RedirectToAction("Index", "Task");
    }

    // Log Out
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("SignIn");
    }
}

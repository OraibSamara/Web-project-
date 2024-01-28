using LibraryProj.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProj.Controllers
{
    public class UsersController : Controller
    {

        private LibraryContext _context { get; set; }
        public UsersController(LibraryContext ctx) => _context = ctx;

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("username") == null)
                return View();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(string username, string password) {
            var user = _context.Users.Where(user=> user.Username == username && user.Password == password).FirstOrDefault();
            if (user == null)
                return View();
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetString("name", user.Name);
            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public IActionResult ChangePassword() {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");
            return View("ChangePassword");
        }
        [HttpPost]
        public IActionResult ChangePassword(string password)
        {
            string? username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Users");
            var user = _context.Users.Where(user=> user.Username == username).FirstOrDefault();
            user.Password = password;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            ViewBag.username = null;
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Books");
        }
    }
}

using LibraryProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProj.Controllers
{
    public class AuthorsController : Controller
    {
        private LibraryContext _context { get; set; }
        public AuthorsController(LibraryContext ctx) => _context = ctx;
        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null)
            {
                return RedirectToAction("Index", "Authors");
            }
            ViewBag.Books = _context.Books.Where(book=> book.AuthorId == id).OrderBy(book => book.BookId).ToList();
            return View(author);
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Author author) {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");
            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index", "Authors");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            var author = _context.Authors.Find(id);
            if (author != null)
                return View(author);
            return RedirectToAction("Index", "Authors");
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            _context.Authors.Update(author);
            _context.SaveChanges();
            return RedirectToAction("Index", "Authors");
        }

        [HttpGet]
        public IActionResult Search(string searchKey)
        {
            if (string.IsNullOrEmpty(searchKey))
            {
                return RedirectToAction("Index", "Authors");
            }
            var authors = _context.Authors.Where(author => author.Name.Contains(searchKey)).OrderBy(author => author.AuthorId).ToList();
            return View("Index", authors);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Remove(author);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Authors");


        }
    }
}

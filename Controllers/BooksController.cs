using LibraryProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProj.Controllers
{
    public class BooksController : Controller
    {
        private LibraryContext _context { get; set; }
        public BooksController(LibraryContext ctx) => _context = ctx;
        public IActionResult Index()
        {
            var books = _context.Books.OrderBy(book => book.BookId).Include(book => book.Author).ToList();
            return View(books);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            var authors = _context.Authors.ToList();
            ViewBag.Authors = authors;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Book book) 
        { 
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            ViewBag.Authors = _context.Authors.ToList();
            var book = _context.Books.Find(id);
            if (book != null)

                return View(book);

            else
                return RedirectToAction("Index", "Books");
        }
        
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");

        }

        [HttpGet]
        public IActionResult Search(string searchKey) {
            if (string.IsNullOrEmpty(searchKey))
            {
                return RedirectToAction("Index", "Books");
            }
            var books = _context.Books.Where(book => book.Name.Contains(searchKey)).OrderBy(book => book.BookId).Include(book=> book.Author).ToList();
            return View("Index", books);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Login", "Users");

            var book = _context.Books.Find(id);
            if (book != null) {
                _context.Remove(book);
                _context.SaveChanges();
            }
                
            return RedirectToAction("Index", "Books");


        }
        
    }
}

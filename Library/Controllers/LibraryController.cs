using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Library
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.BookModel.ToListAsync());
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        // GET: Library/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bookModel = await _context.BookModel
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (bookModel == null)
                {
                    return NotFound();
                }

                return View(bookModel);
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        // GET: Library/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        // POST: Library/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,ReleaseDate,Descritpion")] BookModel bookModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(bookModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(bookModel);
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        // GET: Library/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bookModel = await _context.BookModel.FindAsync(id);
                if (bookModel == null)
                {
                    return NotFound();
                }
                return View(bookModel);
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        // POST: Library/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,ReleaseDate,Descritpion")] BookModel bookModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != bookModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(bookModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BookModelExists(bookModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(bookModel);
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        // GET: Library/Reserve
        public async Task<IActionResult> Reserve(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                BookModel book = _context.BookModel.Where(e => e.Id == id).First();
                IdentityUser user = _context.Users.Where(e => e.UserName == User.Identity.Name).First();
                if (book != null && user != null)
                {
                    if (_context.ReservationModel.Any(e => e.Book.Id == book.Id && e.User.Id == user.Id))
                        return View("~/Views/Library/Exist.cshtml");
                    _context.ReservationModel.Add(new ReservationModel(0, user, book, DateTime.Now));
                    await _context.SaveChangesAsync();
                    return View("~/Views/Library/Reserve.cshtml");
                }
                return NotFound();
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }
        // GET: Library/ReservationList
        public async Task<IActionResult> ReservationList(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                BookModel book = _context.BookModel.Where(e => e.Id == id).First();
                if (book != null)
                {
                    ViewBag.Message = book.Name;
                    return View(await _context.ReservationModel.Where(r => r.Book == book).Include(r => r.User).ToListAsync());
                }
                return NotFound();
            }
            else
            {
                return View("~/Views/Library/ErrorLogin.cshtml");
            }
        }

        private bool BookModelExists(int id)
        {
            return _context.BookModel.Any(e => e.Id == id);
        }
    }
}

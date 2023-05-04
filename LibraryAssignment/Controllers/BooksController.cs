using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryAssignment.Data;
using LibraryAssignment.Models;

namespace LibraryAssignment.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }





        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }








        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            int newId = 0;

            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Index));
            }
            id = id.Trim();

            // If convert == success. User searching for an ID. ELSE get string search-function
            try
            {
                newId = Convert.ToInt32(id);
            }
            catch (Exception)
            {

                return await searchViaString();
            }

            if (newId == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            var books = await _context.Books
                    .FirstOrDefaultAsync(b => b.BookID == newId);
            if (books == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(books);


            /*Local function if id == STRING ------------*/
            async Task<IActionResult> searchViaString()
            {
                var books = await _context.Books
                .ToListAsync();
                books = books.FindAll(b =>
                b.Titel.Contains(id, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(id, StringComparison.OrdinalIgnoreCase) ||
                b.ISBN.Contains(id, StringComparison.OrdinalIgnoreCase));

                return View("GetBooks", books);
            }
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Titel,Description,Author,ISBN")] Book book)
        {
            book.RegisteredBookDate = DateTime.Now;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }













        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }








        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Titel,Description,Author,ISBN")] Book book)
        {
            if (id != book.BookID)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookID))
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

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'LibraryContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookID == id)).GetValueOrDefault();
        }
    }
}

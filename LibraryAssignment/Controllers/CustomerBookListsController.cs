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
    public class CustomerBookListsController : Controller
    {
        private readonly LibraryContext _context;

        public CustomerBookListsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: CustomerBookLists
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.CustomerBookLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .OrderByDescending(c => c.StartBookedDate);

           
            return View(await libraryContext.ToListAsync());
        }

        // GET: GetBooksViaCustomer
        public async Task<IActionResult> GetBooksViaCustomer(string id)
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


            var customerBookList = await _context.CustomerBookLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .Where(m => m.FK_CustomerID == newId)
                .OrderByDescending(o => o.StartBookedDate)
                .ToListAsync();
            if (customerBookList == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Index));
            }
            return View("BookedBooksSingleCustomer",customerBookList);

            /*Local function if id == STRING ------------*/
            async Task<IActionResult> searchViaString()
            {
                var customer = await _context.CustomerBookLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .OrderByDescending(o => o.StartBookedDate)
                .ToListAsync();
                customer = customer.FindAll(b =>
                b.Customers.FirstName.Contains(id, StringComparison.OrdinalIgnoreCase) ||
                b.Customers.LastName.Contains(id, StringComparison.OrdinalIgnoreCase));

                return View("BookedBooksSingleCustomer", customer);
            }
        }


        // GET: CustomerBookLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerBookLists == null)
            {
                return NotFound();
            }

            var customerBookList = await _context.CustomerBookLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CustomerBookListID == id);
            if (customerBookList == null)
            {
                return NotFound();
            }

            return View(customerBookList);
        }

        // GET: CustomerBookLists/Create
        public async Task<IActionResult> Create()
        {
            //var customerBookList = await _context.CustomerBookLists.ToListAsync();
            //ViewData["FK_BookID"] = new SelectList(_context.Books, "BookID", "Author", customerBookList.FK_BookID);
            //ViewData["FK_CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstName", customerBookList.FK_CustomerID);
            //return View();
            ViewData["FK_BookID"] = new SelectList(_context.Books, "BookID", "Author");
            ViewData["FK_CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            return View();
        }

        // POST: CustomerBookLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerBookListID,FK_CustomerID,FK_BookID,Retrieved,StartBookedDate,EndBookedDate")] CustomerBookList customerBookList)
        {
            customerBookList.Returned = false;
            _context.CustomerBookLists.Add(customerBookList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerBookLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerBookLists == null)
            {
                return NotFound();
            }

            var customerBookList = await _context.CustomerBookLists.FindAsync(id);
            if (customerBookList == null)
            {
                return NotFound();
            }
            ViewData["FK_BookID"] = new SelectList(_context.Books, "BookID", "Author", customerBookList.FK_BookID);
            ViewData["FK_CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstName", customerBookList.FK_CustomerID);
            return View(customerBookList);
        }

        // POST: CustomerBookLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerBookListID,FK_CustomerID,FK_BookID,Retrieved,StartBookedDate,EndBookedDate,Returned")] CustomerBookList customerBookList)
        {
            if (id != customerBookList.CustomerBookListID)
            {
                return NotFound();
            }
            ViewData["FK_BookID"] = new SelectList(_context.Books, "BookID", "Author", customerBookList.FK_BookID);
            ViewData["FK_CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstName", customerBookList.FK_CustomerID);

                try
                {
                    _context.Update(customerBookList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerBookListExists(customerBookList.CustomerBookListID))
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

        // GET: CustomerBookLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerBookLists == null)
            {
                return NotFound();
            }

            var customerBookList = await _context.CustomerBookLists
                .Include(c => c.Books)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.CustomerBookListID == id);
            if (customerBookList == null)
            {
                return NotFound();
            }

            return View(customerBookList);
        }

        // POST: CustomerBookLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerBookLists == null)
            {
                return Problem("Entity set 'LibraryContext.CustomerBookLists'  is null.");
            }
            var customerBookList = await _context.CustomerBookLists.FindAsync(id);
            if (customerBookList != null)
            {
                _context.CustomerBookLists.Remove(customerBookList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerBookListExists(int id)
        {
          return (_context.CustomerBookLists?.Any(e => e.CustomerBookListID == id)).GetValueOrDefault();
        }
    }
}

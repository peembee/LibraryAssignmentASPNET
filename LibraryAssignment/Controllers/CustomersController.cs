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
    public class CustomersController : Controller
    {
        private readonly LibraryContext _context;

        public CustomersController(LibraryContext context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }


        // GET: Customers/Details/5
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
            var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.CustomerID == newId);
                if (customer == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);


            /*Local function if id == STRING ------------*/
            async Task<IActionResult> searchViaString()
            {
                var customers = await _context.Customers
                .ToListAsync();
                customers = customers.FindAll(c =>
                c.FirstName.Contains(id, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(id, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(id, StringComparison.OrdinalIgnoreCase));

                return View("GetCustomers", customers);
            }          
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,FirstName,LastName,Phone")] Customer customer)
        {
                customer.RegisteredDate = DateTime.Now;
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,FirstName,LastName,Phone")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return RedirectToAction(nameof(Index));
            }

         
                try
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
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

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'LibraryContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.CustomerID == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSampleApp;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ClientContext _context;

        public EmployeesController(ClientContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Include(e => e.Service).ToListAsync() as ICollection<Employee>);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);

            if (employee == null)
            {
                return NotFound();
            }

            if (employee.Service != null)
            {
                var service = await _context.Services.Where(s => s.ID == employee.Service.ID).FirstOrDefaultAsync();
                employee.Service = service;
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var query = from s in _context.Services select s;
            var result = query.AsNoTracking();
            ViewBag.Services = new SelectList(query.AsNoTracking(), "ServiceID", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Salary,ID,Name,Address,Service")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ID = Guid.NewGuid();
                if (employee.Service != null)
                {
                    var service = await _context.Services.FindAsync(employee.Service);
                    employee.Service = service;
                }
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(e => e.Service).FirstOrDefaultAsync(e => e.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            var query = from s in _context.Services select s;
            var result = query.AsNoTracking();
            ViewBag.Services = new SelectList(query.AsNoTracking(), "ID", "Name");
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Salary,ID,Name,Address,Service, ServiceID")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}

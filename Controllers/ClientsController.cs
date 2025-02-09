using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSampleApp;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientContext _context;

        public ClientsController(ClientContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.Include(c => c.Services).ToListAsync());
        }

        public async Task<IActionResult> Unregistered()
        {
            var clients = await _context.Clients.Include(c => c.Services).Where(c => c.Services.Count() == 0).ToListAsync();
            return View(clients);
        }

        // GET: Clients
        public async Task<IActionResult> SortedByCity()
        {
            var list = await _context.Clients.GroupBy(c => c.Address).ToListAsync();
            return View(list);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Balance,DateOfBirth,PhotoUpload,ID,Name,Address")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.PhotoUpload != null)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        await client.PhotoUpload.CopyToAsync(memoryStream);
                        client.Photo = memoryStream.ToArray();
                    }
                }
                client.ID = Guid.NewGuid();
                
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var client = await _context.Clients.Include(client => client.Services).FirstOrDefaultAsync(i => i.ID == id);
            if (client == null)
            {
                return NotFound();
            }
            var query = from s in _context.Services select s;
            var result = query.AsNoTracking();
            ViewBag.services = result;
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Balance,DateOfBirth,ID,Name,Address,PhotoUpload,ClientServices,Services")] Client client, Guid[] selectedServices)
        {
            if (id != client.ID)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var service in client.Services)
                    {
                        _context.Services.Remove(service);
                    }
                    if (selectedServices.Length > 0)
                    {

                        var serviceIds = await _context.Services.Select(s => s.ID).ToListAsync();
                        var services = await _context.Services.Where(s => selectedServices.Any(ss => ss == s.ID)).ToListAsync();

                        foreach (var service in services)
                        {
                            client.Services.Add(service);
                        }
                    }

                    if (client.PhotoUpload != null)
                    {

                        using (var memoryStream = new MemoryStream())
                        {
                            await client.PhotoUpload.CopyToAsync(memoryStream);
                            client.Photo = memoryStream.ToArray();
                        }
                    }
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(client => client.Services).FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }

        private void PopulateDepartmentsDropDownList(object selectedService = null)
        {
            var servicesQuery = from d in _context.Services
                                   orderby d.Name
                                   select d;
            ViewBag.Services = new SelectList(servicesQuery.AsNoTracking(), "DepartmentID", "Name", selectedService);
        }
    }
}

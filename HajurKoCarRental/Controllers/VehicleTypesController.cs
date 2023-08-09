using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models;

namespace HajurKoCarRental.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VehicleTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Types.ToListAsync());
        }

        // GET: VehicleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        // GET: VehicleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleType);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Type Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["StatusMessage"] = "Error: Type Creation Failed";
            return View(vehicleType);
        }

        // GET: VehicleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.Types.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return View(vehicleType);
        }

        // POST: VehicleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeExists(vehicleType.Id))
                    {
                        TempData["StatusMessage"] = "Error: Editing Type Failed";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["StatusMessage"] = "Edited Type Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["StatusMessage"] = "Error: Editing Type Failed";
            return View(vehicleType);
        }

        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            var cars = _context.Inventory.Where(c => c.TypeId == id);
            ViewData["numOfCars"] = cars.Count();

            var subTypes = _context.SubTypes.Where(st => st.TypeId == id);
            ViewData["numOfSubTypes"] = subTypes.Count();

            return View(vehicleType);
        }

        // POST: VehicleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? delOption)
        {
            if (_context.Types == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Types'  is null.");
            }
            var vehicleType = await _context.Types.FindAsync(id);
            if (vehicleType != null)
            {
                if (delOption != null && delOption == "delete")
                {
                    var cars = _context.Inventory.Where(c => c.TypeId == vehicleType.Id);
                    foreach (var c in cars)
                    {
                        _context.Inventory.Remove(c);
                    }

                    var subTypes = _context.SubTypes.Where(st => st.TypeId == vehicleType.Id);
                    foreach(var st in subTypes)
                    {
                        _context.SubTypes.Remove(st);
                    }
                }
                _context.Types.Remove(vehicleType);
            }
            
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Deleted Type Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeExists(int id)
        {
          return (_context.Types?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
	public class VehicleSubTypesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public VehicleSubTypesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: VehicleSubTypes
		public async Task<IActionResult> Index()
		{
			ViewData["vehicleTypes"] = await _context.Types.ToDictionaryAsync(x => x.Id, x => x.Name);
			return View(await _context.SubTypes.ToListAsync());
		}

		// GET: VehicleSubTypes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.SubTypes == null)
			{
				return NotFound();
			}

			var vehicleSubType = await _context.SubTypes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (vehicleSubType == null)
			{
				return NotFound();
			}

			ViewData["vehicleType"] = await _context.Types.FirstAsync(x => x.Id == vehicleSubType.TypeId);
			return View(vehicleSubType);
		}

		// GET: VehicleSubTypes/Create
		public IActionResult Create()
		{
			ViewData["vehicleTypes"] = _context.Types.ToList();
			return View();
		}

		// POST: VehicleSubTypes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,TypeId,Name")] VehicleSubType vehicleSubType)
		{
			if (ModelState.IsValid)
			{
				_context.Add(vehicleSubType);
				await _context.SaveChangesAsync();
				TempData["StatusMessage"] = "Sub-Type Created Successfully";
				return RedirectToAction(nameof(Index));
			}
			TempData["StatusMessage"] = "Error: Sub-Type Creation Failed";
			ViewData["vehicleTypes"] = await _context.Types.ToListAsync();
			return View(vehicleSubType);
		}

		// GET: VehicleSubTypes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.SubTypes == null)
			{
				return NotFound();
			}

			var vehicleSubType = await _context.SubTypes.FindAsync(id);
			if (vehicleSubType == null)
			{
				return NotFound();
			}
			ViewData["vehicleTypes"] = await _context.Types.ToListAsync();
			return View(vehicleSubType);
		}

		// POST: VehicleSubTypes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,TypeId,Name")] VehicleSubType vehicleSubType)
		{
			if (id != vehicleSubType.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(vehicleSubType);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!VehicleSubTypeExists(vehicleSubType.Id))
					{
						TempData["StatusMessage"] = "Error: Editing Sub-Type Failed";
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				TempData["StatusMessage"] = "Edited Sub-Type Successfully";
				return RedirectToAction(nameof(Index));
			}
			TempData["StatusMessage"] = "Error: Editing Sub-Type Failed";
			ViewData["vehicleTypes"] = await _context.Types.ToListAsync();
			return View(vehicleSubType);
		}

		// GET: VehicleSubTypes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.SubTypes == null)
			{
				return NotFound();
			}

			var vehicleSubType = await _context.SubTypes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (vehicleSubType == null)
			{
				return NotFound();
			}

			var cars = _context.Inventory.Where(c => c.SubTypeId == id);
			ViewData["numOfCars"] = cars.Count();

			ViewData["vehicleType"] = await _context.Types.FirstAsync(x => x.Id == vehicleSubType.TypeId);
			return View(vehicleSubType);
		}

		// POST: VehicleSubTypes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id, string? delOption)
		{
			if (_context.SubTypes == null)
			{
				return Problem("Entity set 'ApplicationDbContext.SubTypes'  is null.");
			}
			var vehicleSubType = await _context.SubTypes.FindAsync(id);
			if (vehicleSubType != null)
			{
				if (delOption != null && delOption == "delete")
				{
					var cars = _context.Inventory.Where(c => c.SubTypeId == vehicleSubType.Id);
					foreach (var c in cars)
					{
						_context.Inventory.Remove(c);
					} 
				}
				_context.SubTypes.Remove(vehicleSubType);
			}
			
			await _context.SaveChangesAsync();
			TempData["StatusMessage"] = "Deleted Type Successfully";
			return RedirectToAction(nameof(Index));
		}

		private bool VehicleSubTypeExists(int id)
		{
		  return (_context.SubTypes?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}

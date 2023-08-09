using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace HajurKoCarRental.Controllers
{
	[Authorize]
	public class CarsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _appEnvironment;

		public CarsController(ApplicationDbContext context,
									UserManager<ApplicationUser> userManager,
									IWebHostEnvironment appEnvironment
		)
		{
			_context = context;
			_userManager = userManager;
			_appEnvironment = appEnvironment;
		}

		// GET: CarsController
		[Authorize(Roles = "Admin,Staff")]
		public async Task<IActionResult> Index()
		{
			var cars = await _context.Inventory.ToListAsync();

			var carsViews = new List<InventoryViewModel>();

			foreach(var car in cars)
			{
				var carsView = new InventoryViewModel();
				carsView.Id = car.Id;
				carsView.Name = car.Name;
				carsView.BrandId = car.BrandId;
				carsView.TypeId = car.TypeId;
				carsView.SubTypeId = car.SubTypeId;
				carsView.Description = car.Description;
				carsView.Picture = car.Picture;
				carsView.PictureName = car.PictureName;
				carsView.PictureExtension = car.PictureExtension;
				carsView.Fuel = car.Fuel;
				carsView.Transmission = car.Transmission;
				carsView.PlateNumber = car.PlateNumber;
				carsView.PricePerDay = car.PricePerDay;
				carsView.InserterId = car.InserterId;
				carsView.RecordCreatedOn = car.RecordCreatedOn;

				carsView.Brand = _context.Brands.First(b => b.Id == carsView.BrandId).Name ?? "Deleted";
				carsView.VehicleType = _context.Types.First(t => t.Id == carsView.TypeId).Name ?? "Deleted";
				carsView.VehicleSubType = _context.SubTypes.First(st => st.Id == carsView.SubTypeId).Name ?? "Deleted";

				var user = await _userManager.FindByIdAsync(carsView.InserterId);
				carsView.InserterName = (user != null)? ((_userManager.GetUserId(User) == user.Id)? "You" : user.FirstName + " " + user.LastName) : "Deleted";
				carsView.InserterAvatar = (carsView.InserterName == "Deleted")?
					(byte[])(new ImageConverter()).ConvertTo(
						Image.FromFile(_appEnvironment.WebRootPath + "/images/ghost-user.jpg"),
						typeof(byte[])
					) 
				: user!.Picture;

				carsViews.Add( carsView );
			}

			return View(carsViews);
		}

		[AllowAnonymous]
		// GET: CarsController/Details/5
		public async Task<IActionResult> Details(int id)
		{
			if (id == null || _context.Inventory == null)
			{
				return NotFound();
			}

			var car = await _context.Inventory.FindAsync(id);
			if (car == null)
			{
				return NotFound();
			}

			InventoryCreateModel createForm = new InventoryCreateModel();

			createForm.Id = car.Id;
			createForm.BrandId = car.BrandId;
			createForm.TypeId = car.TypeId;
			createForm.SubTypeId = car.SubTypeId;
			createForm.Name = car.Name;
			createForm.Description = car.Description;
			createForm.Picture = car.Picture;
			createForm.PictureName = car.PictureName;
			createForm.PictureExtension = car.PictureExtension;
			createForm.Fuel = car.Fuel;
			createForm.Transmission = car.Transmission;
			createForm.PlateNumber = car.PlateNumber;
			createForm.PricePerDay = car.PricePerDay;
			createForm.InserterId = car.InserterId;
			createForm.OutOfDisplay = car.OutOfDisplay;
			createForm.RecordCreatedOn = car.RecordCreatedOn;


			List<Brands> brandTypes = await _context.Brands.ToListAsync();
			List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

			Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
			foreach (var vehicleType in vehicleTypes)
			{
				List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
				vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
			}

			createForm.Brands = brandTypes;
			createForm.VehicleTypes = vehicleTypes;
			createForm.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;
			return View(createForm);
		}

		// GET: CarsController/Create
		[Authorize(Roles = "Admin,Staff")]
		public async Task<IActionResult> Create()
		{
			InventoryCreateModel createForm = new InventoryCreateModel();

			List<Brands> brandTypes = await _context.Brands.ToListAsync();

			List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

			Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
			foreach(var vehicleType in vehicleTypes)
			{
				List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
				vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
			}

			createForm.Brands = brandTypes;
			createForm.VehicleTypes = vehicleTypes;
			createForm.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

			return View(createForm);
		}

		// POST: CarsController/Create
		[Authorize(Roles = "Admin,Staff")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,BrandId,TypeId,SubTypeId,Name,Description,Picture,PictureName,PictureExtension," +
			"Fuel,Transmission,PlateNumber,PricePerDay,OutOfDisplay,InserterId,RecordCreatedOn,")] 
			Inventory inventoryFormData)
		{
			InventoryCreateModel createFormData = new InventoryCreateModel();
			createFormData.Id = inventoryFormData.Id;
			createFormData.BrandId = inventoryFormData.BrandId;
			createFormData.TypeId = inventoryFormData.TypeId;
			createFormData.SubTypeId = inventoryFormData.SubTypeId;
			createFormData.Name = inventoryFormData.Name;
			createFormData.Description = inventoryFormData.Description;
			
			createFormData.Fuel = inventoryFormData.Fuel;
			createFormData.Transmission = inventoryFormData.Transmission;
			createFormData.PlateNumber = inventoryFormData.PlateNumber;
			createFormData.PricePerDay = inventoryFormData.PricePerDay;


			List<Brands> brandTypes = await _context.Brands.ToListAsync();
			List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();
			Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
			foreach (var vehicleType in vehicleTypes)
			{
				List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
				vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
			}
			createFormData.Brands = brandTypes;
			createFormData.VehicleTypes = vehicleTypes;
			createFormData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

			if (Request.Form.Files.Count > 0)
			{
				IFormFile file = Request.Form.Files.First(file => file.Name == "Picture");

				using (var dataStream = new MemoryStream())
				{
					await file.CopyToAsync(dataStream);
					inventoryFormData.Picture = dataStream.ToArray(); // Set picture value as byte array from chosen image
				}

				inventoryFormData.PictureName = file.FileName.ToString();
				inventoryFormData.PictureExtension = file.ContentType.ToString();
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Must Provide Picture of Car");
				return View(createFormData);
			}

			if (ModelState.IsValid)
			{

				var loggedInUser = await _userManager.GetUserAsync(User);
				inventoryFormData.InserterId = loggedInUser.Id;

				inventoryFormData.Description = inventoryFormData.Description.ToString();

				inventoryFormData.OutOfDisplay = false;
				inventoryFormData.RecordCreatedOn = DateTime.Now;

				_context.Add(inventoryFormData);
				await _context.SaveChangesAsync();
				TempData["StatusMessage"] = "Car Added Successfully";
				return RedirectToAction(nameof(Index));
			}

			ModelState.AddModelError(string.Empty, "Car Creation Failed!");
			return View(createFormData);
		}

		// GET: CarsController/Edit/5
		[Authorize(Roles = "Admin,Staff")]
		public async Task<IActionResult> Edit(int id)
		{
			if (id == null || _context.Inventory == null)
			{
				return NotFound();
			}

			var car = await _context.Inventory.FindAsync(id);
			if (car == null)
			{
				return NotFound();
			}

			InventoryCreateModel createForm = new InventoryCreateModel();

			createForm.Id = car.Id;
			createForm.BrandId = car.BrandId;
			createForm.TypeId = car.TypeId;
			createForm.SubTypeId = car.SubTypeId;
			createForm.Name = car.Name;
			createForm.Description = car.Description;
			createForm.Picture = car.Picture;
			createForm.PictureName = car.PictureName;
			createForm.PictureExtension = car.PictureExtension;
			createForm.Fuel = car.Fuel;
			createForm.Transmission = car.Transmission;
			createForm.PlateNumber = car.PlateNumber;
			createForm.PricePerDay = car.PricePerDay;
			createForm.InserterId = car.InserterId;
			createForm.OutOfDisplay = car.OutOfDisplay;
			createForm.RecordCreatedOn = car.RecordCreatedOn;


			List<Brands> brandTypes = await _context.Brands.ToListAsync();
			List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

			Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
			foreach (var vehicleType in vehicleTypes)
			{
				List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
				vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
			}

			createForm.Brands = brandTypes;
			createForm.VehicleTypes = vehicleTypes;
			createForm.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;
			return View(createForm);
		}

		// POST: CarsController/Edit/5
		[Authorize(Roles = "Admin,Staff")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id,
			[Bind("Id,BrandId,TypeId,SubTypeId,Name,Description,Picture,PictureName,PictureExtension," +
			"Fuel,Transmission,PlateNumber,PricePerDay,OutOfDisplay,InserterId,RecordCreatedOn,")]
			Inventory inventoryFormData)
		{
			InventoryCreateModel createFormData = new InventoryCreateModel();

			createFormData.Id = inventoryFormData.Id;
			createFormData.BrandId = inventoryFormData.BrandId;
			createFormData.TypeId = inventoryFormData.TypeId;
			createFormData.SubTypeId = inventoryFormData.SubTypeId;
			createFormData.Name = inventoryFormData.Name;
			createFormData.Description = inventoryFormData.Description;
			
			createFormData.Fuel = inventoryFormData.Fuel;
			createFormData.Transmission = inventoryFormData.Transmission;
			createFormData.PlateNumber = inventoryFormData.PlateNumber;
			createFormData.PricePerDay = inventoryFormData.PricePerDay;
			createFormData.InserterId = inventoryFormData.InserterId;
			createFormData.OutOfDisplay = inventoryFormData.OutOfDisplay;
			createFormData.RecordCreatedOn = inventoryFormData.RecordCreatedOn;


			List<Brands> brandTypes = await _context.Brands.ToListAsync();
			List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();
			Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
			foreach (var vehicleType in vehicleTypes)
			{
				List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
				vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
			}
			createFormData.Brands = brandTypes;
			createFormData.VehicleTypes = vehicleTypes;
			createFormData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

			if (Request.Form.Files.Count > 0)
			{
				IFormFile file = Request.Form.Files.First(file => file.Name == "Picture");

				using (var dataStream = new MemoryStream())
				{
					await file.CopyToAsync(dataStream);
					inventoryFormData.Picture = dataStream.ToArray(); // Set picture value as byte array from chosen image
				}

				inventoryFormData.PictureName = file.FileName.ToString();
				inventoryFormData.PictureExtension = file.ContentType.ToString();
			}

			if (ModelState.IsValid)
			{
				inventoryFormData.Description = inventoryFormData.Description.ToString();

				_context.Update(inventoryFormData);
				await _context.SaveChangesAsync();
				TempData["StatusMessage"] = "Car Updated Successfully";
				return RedirectToAction(nameof(Index));
			}

			ModelState.AddModelError(string.Empty, "Car Updating Failed!");
			return View(createFormData);
		}

		// GET: CarsController/Delete/5
		[Authorize(Roles = "Admin,Staff")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id == null || _context.Inventory == null)
			{
				return NotFound();
			}

			var car = await _context.Inventory.FindAsync(id);
			if (car == null)
			{
				return NotFound();
			}

			InventoryCreateModel createForm = new InventoryCreateModel();

			createForm.Id = car.Id;
			createForm.BrandId = car.BrandId;
			createForm.TypeId = car.TypeId;
			createForm.SubTypeId = car.SubTypeId;
			createForm.Name = car.Name;
			createForm.Description = car.Description;
			createForm.Picture = car.Picture;
			createForm.PictureName = car.PictureName;
			createForm.PictureExtension = car.PictureExtension;
			createForm.Fuel = car.Fuel;
			createForm.Transmission = car.Transmission;
			createForm.PlateNumber = car.PlateNumber;
			createForm.PricePerDay = car.PricePerDay;
			createForm.InserterId = car.InserterId;
			createForm.OutOfDisplay = car.OutOfDisplay;
			createForm.RecordCreatedOn = car.RecordCreatedOn;


			List<Brands> brandTypes = await _context.Brands.ToListAsync();
			List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

			Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
			foreach (var vehicleType in vehicleTypes)
			{
				List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
				vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
			}

			createForm.Brands = brandTypes;
			createForm.VehicleTypes = vehicleTypes;
			createForm.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;
			return View(createForm);
		}

		// POST: CarsController/Delete/5
		[Authorize(Roles = "Admin,Staff")]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Inventory == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Inventory'  is null.");
			}
			var car = await _context.Inventory.FindAsync(id);
			if (car != null)
			{
				_context.Inventory.Remove(car);
			}

			await _context.SaveChangesAsync();
			TempData["StatusMessage"] = "Deleted Car Successfully";
			return RedirectToAction(nameof(Index));
		}
	}
}

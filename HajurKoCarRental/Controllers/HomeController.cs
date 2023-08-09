using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Drawing;

namespace HajurKoCarRental.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _appEnvironment;

		public HomeController(ApplicationDbContext context,
									UserManager<ApplicationUser> userManager,
									ILogger<HomeController> logger,
									IWebHostEnvironment appEnvironment
		)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
			_appEnvironment = appEnvironment;
		}

		public async Task<IActionResult> Index()
		{
			var cars = await _context.Inventory.Where(c => c.OutOfDisplay == false).ToListAsync();

			var carsViews = new List<InventoryViewModel>();

			foreach (var car in cars)
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
				carsView.InserterName = (user != null) ? ((_userManager.GetUserId(User) == user.Id) ? "You" : user.FirstName + " " + user.LastName) : "Deleted";
				carsView.InserterAvatar = (carsView.InserterName == "Deleted") ?
					(byte[])(new ImageConverter()).ConvertTo(
						Image.FromFile(_appEnvironment.WebRootPath + "/images/ghost-user.jpg"),
						typeof(byte[])
					)
				: user!.Picture;

				carsViews.Add(carsView);
			}

			return View(carsViews);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
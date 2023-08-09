using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Controllers
{
    public class DamagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public DamagesController(ApplicationDbContext context,
                                    UserManager<ApplicationUser> userManager,
                                    IWebHostEnvironment appEnvironment
        )
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var damages = await _context.Damages.Where(r => r.RequesterId == loggedInUser.Id).ToListAsync();

            var damageViews = new List<DamageCreateViewModel>();

            foreach (var damage in damages)
            {
                DamageCreateViewModel damageView = new DamageCreateViewModel();
                InventoryCreateModel carData = new InventoryCreateModel();

                var car = await _context.Inventory.FindAsync(damage.CarId);

                if (car == null)
                {
                    carData.Name = "Deleted";
                    carData.Picture = null;
                }
                else
                {
                    carData.Name = car.Name;
                    carData.BrandId = car.BrandId;
                    carData.TypeId = car.TypeId;
                    carData.SubTypeId = car.SubTypeId;
                    carData.Description = car.Description;
                    carData.Picture = car.Picture;
                    carData.PictureName = car.PictureName;
                    carData.PictureExtension = car.PictureExtension;
                    carData.Fuel = car.Fuel;
                    carData.Transmission = car.Transmission;
                    carData.PlateNumber = car.PlateNumber;
                    carData.PricePerDay = car.PricePerDay;
                    carData.InserterId = car.InserterId;
                    carData.OutOfDisplay = car.OutOfDisplay;
                    carData.RecordCreatedOn = car.RecordCreatedOn;
                }

                carData.Id = damage.CarId;

                List<Brands> brandTypes = await _context.Brands.ToListAsync();
                List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

                Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
                foreach (var vehicleType in vehicleTypes)
                {
                    List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
                    vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
                }

                carData.Brands = brandTypes;
                carData.VehicleTypes = vehicleTypes;
                carData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

                damageView.Id = damage.Id;
                damageView.CarData = carData;
                damageView.CarId = carData.Id;
                damageView.Description = damage.Description;
                damageView.RecordCreatedOn = damage.RecordCreatedOn;
                damageView.RepairPrice = damage.RepairPrice;
                damageView.Pending = damage.Pending;
                damageView.Reviewed = damage.Reviewed;
                damageView.Paid = damage.Paid;
                damageView.RequesterId = damage.RequesterId;
                damageView.ApproverId = damage.ApproverId;

                damageViews.Add(damageView);
            }

            return View(damageViews);
        }

        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser.VerificationDocuments == null)
            {
                TempData["StatusMessage"] = "You need to provide identification document before renting";
                return Redirect("~/Identity/Account/Manage/Document");
            }

            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var car = await _context.Inventory.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            InventoryCreateModel carData = new InventoryCreateModel();

            carData.Id = car.Id;
            carData.BrandId = car.BrandId;
            carData.TypeId = car.TypeId;
            carData.SubTypeId = car.SubTypeId;
            carData.Name = car.Name;
            carData.Description = car.Description;
            carData.Picture = car.Picture;
            carData.PictureName = car.PictureName;
            carData.PictureExtension = car.PictureExtension;
            carData.Fuel = car.Fuel;
            carData.Transmission = car.Transmission;
            carData.PlateNumber = car.PlateNumber;
            carData.PricePerDay = car.PricePerDay;
            carData.InserterId = car.InserterId;
            carData.OutOfDisplay = car.OutOfDisplay;
            carData.RecordCreatedOn = car.RecordCreatedOn;


            List<Brands> brandTypes = await _context.Brands.ToListAsync();
            List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

            Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
            foreach (var vehicleType in vehicleTypes)
            {
                List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
                vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
            }

            carData.Brands = brandTypes;
            carData.VehicleTypes = vehicleTypes;
            carData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

            DamageCreateViewModel damageCreateData = new DamageCreateViewModel();

            damageCreateData.CarData = carData;
            damageCreateData.CarId = carData.Id;

            damageCreateData.RequesterId = loggedInUser.Id;

            return View(damageCreateData);
        }

        // POST: CarsController/Create
        [Authorize(Roles = "User,Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,CarId,Description,RepairPrice," +
            "Pending,Reviewed,Paid,RequesterId,ApproverId,RecordCreatedOn,")]
            Damage requestFormData)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser.VerificationDocuments == null)
            {
                TempData["StatusMessage"] = "You need to provide identification document before renting";
                return Redirect("~/Identity/Account/Manage/Document");
            }

            if (requestFormData.Id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var car = await _context.Inventory.FindAsync(requestFormData.Id);
            if (car == null)
            {
                return NotFound();
            }

            InventoryCreateModel carData = new InventoryCreateModel();

            carData.Id = car.Id;
            carData.BrandId = car.BrandId;
            carData.TypeId = car.TypeId;
            carData.SubTypeId = car.SubTypeId;
            carData.Name = car.Name;
            carData.Description = car.Description;
            carData.Picture = car.Picture;
            carData.PictureName = car.PictureName;
            carData.PictureExtension = car.PictureExtension;
            carData.Fuel = car.Fuel;
            carData.Transmission = car.Transmission;
            carData.PlateNumber = car.PlateNumber;
            carData.PricePerDay = car.PricePerDay;
            carData.InserterId = car.InserterId;
            carData.OutOfDisplay = car.OutOfDisplay;
            carData.RecordCreatedOn = car.RecordCreatedOn;


            List<Brands> brandTypes = await _context.Brands.ToListAsync();
            List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

            Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
            foreach (var vehicleType in vehicleTypes)
            {
                List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
                vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
            }

            carData.Brands = brandTypes;
            carData.VehicleTypes = vehicleTypes;
            carData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

            DamageCreateViewModel requestCreateData = new DamageCreateViewModel();

            requestCreateData.CarData = carData;
            requestCreateData.CarId = carData.Id;

            requestCreateData.RequesterId = loggedInUser.Id;

            requestCreateData.Description = requestFormData.Description;
            requestCreateData.Pending = requestFormData.Pending;
            requestCreateData.Reviewed = requestFormData.Reviewed;
            requestCreateData.Paid = requestFormData.Paid;

            if (ModelState.IsValid)
            {
                requestCreateData.Pending = 1;
                requestCreateData.Reviewed = 0;
                requestCreateData.Paid = 0;

                requestCreateData.RecordCreatedOn = DateTime.Now;

                _context.Add(requestCreateData);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Damage Request Filed Successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Filing Damage Request Failed!");
            return View(requestCreateData);
        }

        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Requesters()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var damages = await _context.Damages.ToListAsync();

            var damageViews = new List<DamageCreateViewModel>();

            foreach (var damage in damages)
            {
                DamageCreateViewModel damageView = new DamageCreateViewModel();
                InventoryCreateModel carData = new InventoryCreateModel();

                var car = await _context.Inventory.FindAsync(damage.CarId);

                if (car == null)
                {
                    carData.Name = "Deleted";
                    carData.Picture = null;
                }
                else
                {
                    carData.Name = car.Name;
                    carData.BrandId = car.BrandId;
                    carData.TypeId = car.TypeId;
                    carData.SubTypeId = car.SubTypeId;
                    carData.Description = car.Description;
                    carData.Picture = car.Picture;
                    carData.PictureName = car.PictureName;
                    carData.PictureExtension = car.PictureExtension;
                    carData.Fuel = car.Fuel;
                    carData.Transmission = car.Transmission;
                    carData.PlateNumber = car.PlateNumber;
                    carData.PricePerDay = car.PricePerDay;
                    carData.InserterId = car.InserterId;
                    carData.OutOfDisplay = car.OutOfDisplay;
                    carData.RecordCreatedOn = car.RecordCreatedOn;
                }

                carData.Id = damage.CarId;

                List<Brands> brandTypes = await _context.Brands.ToListAsync();
                List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

                Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
                foreach (var vehicleType in vehicleTypes)
                {
                    List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
                    vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
                }

                carData.Brands = brandTypes;
                carData.VehicleTypes = vehicleTypes;
                carData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

                damageView.Id = damage.Id;
                damageView.CarData = carData;
                damageView.CarId = carData.Id;
                damageView.Description = damage.Description;
                damageView.RecordCreatedOn = damage.RecordCreatedOn;
                damageView.RepairPrice = damage.RepairPrice;
                damageView.Pending = damage.Pending;
                damageView.Reviewed = damage.Reviewed;
                damageView.Paid = damage.Paid;
                damageView.RequesterId = damage.RequesterId;
                damageView.ApproverId = damage.ApproverId;

                damageViews.Add(damageView);
            }

            return View(damageViews);
        }

        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Damages.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            var car = await _context.Inventory.FindAsync(request.CarId);
            if (car == null)
            {
                return NotFound();
            }

            InventoryCreateModel carData = new InventoryCreateModel();

            carData.Id = car.Id;
            carData.BrandId = car.BrandId;
            carData.TypeId = car.TypeId;
            carData.SubTypeId = car.SubTypeId;
            carData.Name = car.Name;
            carData.Description = car.Description;
            carData.Picture = car.Picture;
            carData.PictureName = car.PictureName;
            carData.PictureExtension = car.PictureExtension;
            carData.Fuel = car.Fuel;
            carData.Transmission = car.Transmission;
            carData.PlateNumber = car.PlateNumber;
            carData.PricePerDay = car.PricePerDay;
            carData.InserterId = car.InserterId;
            carData.OutOfDisplay = car.OutOfDisplay;
            carData.RecordCreatedOn = car.RecordCreatedOn;


            List<Brands> brandTypes = await _context.Brands.ToListAsync();
            List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

            Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
            foreach (var vehicleType in vehicleTypes)
            {
                List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
                vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
            }

            carData.Brands = brandTypes;
            carData.VehicleTypes = vehicleTypes;
            carData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

            ViewData["carData"] = carData;
            return View(request);
        }

        // POST: CarsController/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,CarId,Description,RepairPrice,Pending,Reviewed,Paid,RequesterId,ApproverId,RecordCreatedOn,")]
            Damage requestFormData)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var car = await _context.Inventory.FindAsync(requestFormData.CarId);
            if (car == null)
            {
                return NotFound();
            }

            InventoryCreateModel carData = new InventoryCreateModel();

            carData.Id = car.Id;
            carData.BrandId = car.BrandId;
            carData.TypeId = car.TypeId;
            carData.SubTypeId = car.SubTypeId;
            carData.Name = car.Name;
            carData.Description = car.Description;
            carData.Picture = car.Picture;
            carData.PictureName = car.PictureName;
            carData.PictureExtension = car.PictureExtension;
            carData.Fuel = car.Fuel;
            carData.Transmission = car.Transmission;
            carData.PlateNumber = car.PlateNumber;
            carData.PricePerDay = car.PricePerDay;
            carData.InserterId = car.InserterId;
            carData.OutOfDisplay = car.OutOfDisplay;
            carData.RecordCreatedOn = car.RecordCreatedOn;


            List<Brands> brandTypes = await _context.Brands.ToListAsync();
            List<VehicleType> vehicleTypes = await _context.Types.ToListAsync();

            Dictionary<int, List<VehicleSubType>> vehicleTypesAndSubtypes = new Dictionary<int, List<VehicleSubType>>();
            foreach (var vehicleType in vehicleTypes)
            {
                List<VehicleSubType> vehicleSubType = await _context.SubTypes.Where(st => st.TypeId == vehicleType.Id).ToListAsync();
                vehicleTypesAndSubtypes.Add(vehicleType.Id, vehicleSubType);
            }

            carData.Brands = brandTypes;
            carData.VehicleTypes = vehicleTypes;
            carData.VehicleTypesAndSubTypes = vehicleTypesAndSubtypes;

            ViewData["carData"] = carData;

            var loggedInUser = await _userManager.GetUserAsync(User);
            var request = await _context.Damages.FindAsync(requestFormData.Id);

            requestFormData.Description = request.Description;

            request.Pending = requestFormData.Pending ?? request.Pending;
            request.Reviewed = requestFormData.Reviewed ?? request.Reviewed;
            request.Paid = requestFormData.Paid ?? request.Paid;

            request.RepairPrice = requestFormData.RepairPrice ?? request.RepairPrice;

            if (requestFormData.Reviewed == 1)
            {
                request.ApproverId = loggedInUser.Id;
            }

            _context.Update(request);
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Damage Request Updated Successfully";
            return RedirectToAction(nameof(Requesters));
            

            ModelState.AddModelError(string.Empty, "Damage Request Update Failed!");
            return View(requestFormData);
        }
    }
}

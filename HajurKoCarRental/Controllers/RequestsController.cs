using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;

namespace HajurKoCarRental.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public RequestsController(ApplicationDbContext context,
                                    UserManager<ApplicationUser> userManager,
                                    IWebHostEnvironment appEnvironment
        )
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public IActionResult GotoCreate(int id)
        {
            return Redirect("~/Requests/Create/?id=" + id);
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var requests = await _context.Requests.Where(r => r.RequesterId == loggedInUser.Id).ToListAsync();

            var requestViews = new List<RequestCreateViewModel>();

            foreach (var request in requests)
            {
                InventoryCreateModel carData = new InventoryCreateModel();

                var car = await _context.Inventory.FindAsync(request.CarId);
               
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

                carData.Id = request.CarId;

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

                RequestCreateViewModel requestCreateData = new RequestCreateViewModel();

                requestCreateData.Id = request.Id;
                requestCreateData.CarData = carData;
                requestCreateData.CarId = carData.Id;

                requestCreateData.RequesterId = request.RequesterId;

                requestCreateData.PickingUp = request.PickingUp;
                requestCreateData.DroppingOff = request.DroppingOff;
                requestCreateData.Pending = request.Pending;
                requestCreateData.Approved = request.Approved;
                requestCreateData.Claimed = request.Claimed;
                requestCreateData.Returned = request.Returned;
                requestCreateData.Paid = request.Paid;

                requestCreateData.RecordCreatedOn = request.RecordCreatedOn;
                requestCreateData.TotalPrice = request.TotalPrice;

                requestViews.Add(requestCreateData);
            }

            return View(requestViews);
        }

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

            RequestCreateViewModel requestCreateData = new RequestCreateViewModel();

            requestCreateData.CarData = carData;
            requestCreateData.CarId = carData.Id;

            requestCreateData.RequesterId = loggedInUser.Id;

            return View(requestCreateData);
        }

        // POST: CarsController/Create
        [Authorize(Roles = "User,Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,CarId,PickingUp,DroppingOff,TotalPrice," +
            "Pending,Approved,Claimed,Returned,Paid,RequesterId,ApproverId,RecordCreatedOn,")]
            Request requestFormData)
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

            RequestCreateViewModel requestCreateData = new RequestCreateViewModel();

            requestCreateData.CarData = carData;
            requestCreateData.CarId = carData.Id;

            
            requestCreateData.RequesterId = loggedInUser.Id;

            requestCreateData.PickingUp = requestFormData.PickingUp;
            requestCreateData.DroppingOff = requestFormData.DroppingOff;
            requestCreateData.Pending = requestFormData.Pending;
            requestCreateData.Approved = requestFormData.Approved;
            requestCreateData.Claimed = requestFormData.Claimed;
            requestCreateData.Returned = requestFormData.Returned;
            requestCreateData.Paid = requestFormData.Paid;

            if (ModelState.IsValid)
            {
                requestCreateData.Pending = 1;
                requestCreateData.Approved = 0;
                requestCreateData.Claimed = 0;
                requestCreateData.Returned = 0;
                requestCreateData.Paid = 0;

                requestCreateData.RecordCreatedOn = DateTime.Now;


                var dateUsed = (requestCreateData.DroppingOff - requestCreateData.PickingUp).Value.TotalDays;
                var TotalPrice = dateUsed * requestCreateData.CarData.PricePerDay;

                var previousRecords = _context.Requests.Where(r => r.RequesterId == loggedInUser.Id).Count();

                if((requestCreateData.RequesterId == loggedInUser.Id) && User.IsInRole("Staff"))
                {
                    TotalPrice = TotalPrice - ((25/100)*TotalPrice);
                }
                else
                {
                    if (previousRecords > 3)
                    {
                        TotalPrice = TotalPrice - ((1 / 10) * TotalPrice);
                    }
                }
                requestCreateData.TotalPrice = (float) TotalPrice;

                _context.Add(requestCreateData);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Rental Request Done Successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Rental Request Failed!");
            return View(requestCreateData);
        }

        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Requesters()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var requests = await _context.Requests.ToListAsync();

            var requestViews = new List<RequestCreateViewModel>();

            foreach (var request in requests)
            {
                RequestCreateViewModel requestView = new RequestCreateViewModel();
                InventoryCreateModel carData = new InventoryCreateModel();

                var car = await _context.Inventory.FindAsync(request.CarId);

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

                carData.Id = request.CarId;


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

                RequestCreateViewModel requestCreateData = new RequestCreateViewModel();

                requestCreateData.Id = request.Id;
                requestCreateData.CarData = carData;
                requestCreateData.CarId = carData.Id;

                requestCreateData.RequesterId = request.RequesterId;

                requestCreateData.PickingUp = request.PickingUp;
                requestCreateData.DroppingOff = request.DroppingOff;
                requestCreateData.Pending = request.Pending;
                requestCreateData.Approved = request.Approved;
                requestCreateData.Claimed = request.Claimed;
                requestCreateData.Returned = request.Returned;
                requestCreateData.Paid = request.Paid;

                requestCreateData.RecordCreatedOn = request.RecordCreatedOn;
                requestCreateData.TotalPrice = request.TotalPrice;

                requestViews.Add(requestCreateData);
            }

            return View(requestViews);
        }

        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
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
            [Bind("Id,CarId,PickingUp,DroppingOff,TotalPrice," +
            "Pending,Approved,Claimed,Returned,Paid,RequesterId,ApproverId,RecordCreatedOn,")]
            Request requestFormData)
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

            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                var request = await _context.Requests.FindAsync(requestFormData.Id);

                request.Pending = requestFormData.Pending ?? request.Pending;
                request.Claimed = requestFormData.Claimed ?? request.Claimed;
                request.Approved = requestFormData.Approved ?? request.Approved;
                request.Returned = requestFormData.Returned ?? request.Returned;
                request.Paid = requestFormData.Paid ?? request.Paid;

                if (requestFormData.Approved == 1)
                {
                    request.ApproverId = loggedInUser.Id;
                }

                if(requestFormData.Claimed == 1)
                {
                    car.OutOfDisplay = true;
                    _context.Update(car);
                }

                if (requestFormData.Returned == 1)
                {
                    car.OutOfDisplay = false;
                    _context.Update(car);
                }

                _context.Update(request);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Rental Request Updated Successfully";
                return RedirectToAction(nameof(Requesters));
            }

            ModelState.AddModelError(string.Empty, "Rental Request Update Failed!");
            return View(requestFormData);
        }
    }
}

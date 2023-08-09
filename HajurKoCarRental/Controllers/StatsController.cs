using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HajurKoCarRental.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public StatsController(ApplicationDbContext context,
                                    UserManager<ApplicationUser> userManager,
                                    IWebHostEnvironment appEnvironment
        )
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["brands"] = _context.Brands.ToList();
            ViewData["types"] = _context.Types.ToList();
            ViewData["subTypes"] = _context.SubTypes.ToList();

            ViewData["cars"] = _context.Inventory.ToList();
            ViewData["requests"] = _context.Requests.ToList();
            ViewData["damages"] = _context.Damages.ToList();

            ViewData["users"] = _context.Users.ToList();
            ViewData["roles"] = _context.Roles.ToList();
            ViewData["userRoles"] = _context.UserRoles.ToList();

            return View();
        }

        public IActionResult Cars()
        {
            ViewData["brands"] = _context.Brands.ToList();
            ViewData["types"] = _context.Types.ToList();
            ViewData["subTypes"] = _context.SubTypes.ToList();

            ViewData["cars"] = _context.Inventory.ToList();
            ViewData["requests"] = _context.Requests.ToList();
            ViewData["damages"] = _context.Damages.ToList();

            ViewData["users"] = _context.Users.ToList();
            ViewData["roles"] = _context.Roles.ToList();
            ViewData["userRoles"] = _context.UserRoles.ToList();

            return View();
        }
    }
}

using HajurKoCarRental.Models;
using HajurKoCarRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Drawing;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;

namespace HajurKoCarRental.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager; // For Role selection functionality
        private readonly IWebHostEnvironment _appEnvironment; // For Default image setting functionality

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _roleManager = roleManager; // In order to allow selection of desired role when registering
            _appEnvironment = appEnvironment;
        }

        // GET: UserController
        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string id)
        {
            return Redirect("~/Identity/Account/Manage?handler=other&userId=" + id);
        }

        [Authorize("Admin")]
        // GET: UserController/Create
        public ActionResult Create()
        {
            return Redirect("~/Identity/Account/Register");
        }

        public ActionResult CreateFromList()
        {
            TempData["BackToList"] = true;
            return Redirect("~/Identity/Account/Register");
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            return Redirect("~/Identity/Account/Manage?handler=other&userId=" + id);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(string id)
        {
            return Redirect("~/Identity/Account/Manage/DeletePersonalData?handler=other&userId=" + id);
        }

        [AllowAnonymous]
        public IActionResult Signup()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("Email,Password,ConfirmPassword,FirstName,LastName,PhoneNumber,Address,Picture,VerificationDocuments")] SignUp signup)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = signup.FirstName;
                user.LastName = signup.LastName;
                user.Address = signup.Address;
                user.PhoneNumber = signup.PhoneNumber;
                user.LockoutEnabled = false;

                if (Request.Form.Files.Count > 0) // If user has chosen a file then check which file was provided
                {

                    if(Request.Form.Files.Where(file => file.Name == "VerificationDocuments").Count() > 0) // If VerificationDocuments chosen
                    {
                        IFormFile file = Request.Form.Files.First(file => file.Name == "VerificationDocuments");
                        if ((file.Length / 1024f) / 1024f > 1.5)
                        {
                            ModelState.AddModelError(string.Empty, "Identification Document File Size More Than 1.5MB");
                            return View(signup);
                        }
                        user.VerificationDocumentsType = file.ContentType;
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            user.VerificationDocuments = dataStream.ToArray(); // Set VerificationDocuments value as byte array from chosen document
                        }
                    }

                    if (Request.Form.Files.Where(file => file.Name == "Picture").Count() > 0)
                    {
                        IFormFile file = Request.Form.Files.First(file => file.Name == "Picture");

                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            user.Picture = dataStream.ToArray(); // Set picture value as byte array from chosen image
                        }
                    }
                    else // If user has not chosen a file but it's not Picture, assign a random default profile picture
                    {
                        Random rnd = new Random();
                        int randomNum = rnd.Next(1, 16); // Pick a random number from 1 ro 16 since it is our default avatars
                        user.Picture = (byte[])(new ImageConverter()).ConvertTo(
                            Image.FromFile(_appEnvironment.WebRootPath + "/images/profile_pictures/" + randomNum.ToString() + ".png"),
                            typeof(byte[])
                        );
                    }
                }
                else // If user has not chosen a file, assign a random default profile picture
                {
                    Random rnd = new Random();
                    int randomNum = rnd.Next(1, 16); // Pick a random number from 1 ro 16 since it is our default avatars
                    user.Picture = (byte[])(new ImageConverter()).ConvertTo(
                        Image.FromFile(_appEnvironment.WebRootPath + "/images/profile_pictures/" + randomNum.ToString() + ".png"),
                        typeof(byte[])
                    );
                }

                /*
                StringBuilder s = new StringBuilder();
                if(Request.Form.Files.Where(file => file.Name == "VerificationDocuments").Count() > 0)
                {
                    s.AppendLine("VerificationDocuments Supplied");
                }
                if (Request.Form.Files.Where(file => file.Name == "Picture").Count() > 0)
                {
                    s.AppendLine("Picture Supplied");
                }
                string formData = s.ToString();

                TempData["StatusMessage"] = "Error: " + formData;
                return View(signup);*/


                /*
                if (Request.Form.Files.Count > 0) // If user has chosen a file then save to database
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        user.Picture = dataStream.ToArray(); // Set picture value as byte array from chosen image
                    }
                }
                else // If user has not chosen a file, assign a random default profile picture
                {
                    Random rnd = new Random();
                    int randomNum = rnd.Next(1, 16); // Pick a random number from 1 ro 16 since it is our default avatars
                    user.Picture = (byte[])(new ImageConverter()).ConvertTo(
                        Image.FromFile(_appEnvironment.WebRootPath + "/images/profile_pictures/" + randomNum.ToString() + ".png"),
                        typeof(byte[])
                    );
                }*/

                await _emailStore.SetEmailAsync(user, signup.Email, CancellationToken.None);
                await _emailStore.SetEmailConfirmedAsync(user, true, CancellationToken.None);
                await _userStore.SetUserNameAsync(user, signup.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, signup.Password);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "User"); // If user registered then assign selected role

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //TempData["StatusMessage"] = "User Registered Successfully";
                    return RedirectToAction("Index", "Home");

                }

                foreach (var error in result.Errors)
                {
                    if (error.Description == ("Username '" + signup.Email + "' is already taken."))
                    {
                        error.Description = "Email '" + signup.Email + "' is already taken";
                    }
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(signup);
            }

            TempData["StatusMessage"] = "Error: Couldn't create user";
            return View(signup);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        public async Task<IActionResult> DownloadDocument(string id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user.VerificationDocuments == null)
            {
                return NotFound();
            }

            var fileName = user.FirstName + "_" + user.LastName + "_Document." + user!.VerificationDocumentsType!.Split("/").LastOrDefault();

            return File(user.VerificationDocuments, user!.VerificationDocumentsType!, fileName);
        }
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HajurKoCarRental.Models;
using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Data;

namespace HajurKoCarRental.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly ApplicationDbContext _context;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "You must enter your password to delete your account")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnGetOther(string userId)
        {
            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage/DeletePersonalData");
            }

            if (userId == _userManager.GetUserId(User)) return Redirect("/Identity/Account/Manage/DeletePersonalData");
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Identity/Account/AccessDenied");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Redirect("~/");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            ViewData["userId"] = userId;
            RequirePassword = false;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("~/");
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var deletedUserId = await _userManager.GetUserIdAsync(user);

            /* TODO: Work on this once you implement the requester and reviewer system
             * 
            var requests = _context.Requests.Where(e => e.RequesterId == deletedUserId);

            foreach (var r in requests)
            {
                var review = _context.Reviews.FirstOrDefault(re => re.RequestId == r.Id);
                if (review != null)
                {
                    _context.Reviews.Remove(review);
                }
                _context.Requests.Remove(r);
            }

            bool isUser = await _userManager.IsInRoleAsync(user, "User");
            if (!isUser)
            {
                var req = _context.Requests.Where(r => r.ReviewerId == deletedUserId);
                if (req != null)
                {
                    foreach (var r in req)
                    {
                        if (r.Reviewed == 0)
                        {
                            r.Pending = 1;
                            r.Claimed = 0;
                            r.Reviewing = 0;
                            r.ReviewerId = null;

                            _context.Requests.Update(r);
                        }
                    }
                }
            }*/

            await _context.SaveChangesAsync();

            var userId = await _userManager.GetUserIdAsync(user);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", deletedUserId);

            return Redirect("~/");
        }

        public async Task<IActionResult> OnPostOtherAsync(string userId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Identity/Account/AccessDenied");
            }

            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage/DeletePersonalData");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Redirect("~/");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var deletedUserId = await _userManager.GetUserIdAsync(user);

            /* TODO: Work on this once you implement the requester and reviewer system
             * 
            var requests = _context.Requests.Where(e => e.RequesterId == deletedUserId);

            foreach (var r in requests)
            {
                var review = _context.Reviews.FirstOrDefault(re => re.RequestId == r.Id);
                if (review != null)
                {
                    _context.Reviews.Remove(review);
                }
                _context.Requests.Remove(r);
            }

            bool isUser = await _userManager.IsInRoleAsync(user, "User");
            if (!isUser)
            {
                var req = _context.Requests.Where(r => r.ReviewerId == deletedUserId);
                if (req != null)
                {
                    foreach (var r in req)
                    {
                        if (r.Reviewed == 0)
                        {
                            r.Pending = 1;
                            r.Claimed = 0;
                            r.Reviewing = 0;
                            r.ReviewerId = null;

                            _context.Requests.Update(r);
                        }
                    }
                }
            }*/

            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }
            _logger.LogInformation("User with ID '{UserId}' deleted {DeletedUserId}.", userId, deletedUserId);


            TempData["AdminStatusMessage"] = "Deleted " + user.FirstName + " " + user.LastName + "'s Account Successfully";
            return Redirect("~/Users");
        }
    }
}

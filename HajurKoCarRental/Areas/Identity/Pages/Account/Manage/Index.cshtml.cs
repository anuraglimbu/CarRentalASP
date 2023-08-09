// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HajurKoCarRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HajurKoCarRental.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        public string UserID { get; set; }

        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
            /// 
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            // Custom Implementation from below
            [Display(Name = "Profile Picture")]
            public byte[] Picture { get; set; }

            [Required(ErrorMessage = "First name of the user must be provided")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }


            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Please select a role of the user")]
            [Display(Name = "Role")]
            public string UserRole { get; set; } // For User Roles
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            //var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);

            var roleName = await _userManager.GetRolesAsync(user);
            var roleId = await _roleManager.FindByNameAsync(roleName.FirstOrDefault());

            //System.Diagnostics.Debug.WriteLine(roleId.Id);
            //System.Diagnostics.Debug.WriteLine(_roleManager.Roles.ToList().);

            var firstName = user.FirstName;
            var lastName = user.LastName;
            var address = user.Address;
            var picture = user.Picture;

            UserID = userId;
            Email = email;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Picture = picture,
                UserRole = roleId.Id,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("~/");
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            //var role = await _roleManager.FindByIdAsync(Input.UserRole);
            //ViewData["roleName"] = role.Name;
            if (!User.IsInRole("Admin"))
            {
                //var roles = _roleManager.Roles.ToList();
                List<IdentityRole> roles = new List<IdentityRole>();
                foreach (var role in _roleManager.Roles)
                {
                    if (User.IsInRole("User") && role.Name == "User")
                    {
                        roles.Add(role); // If not admin don't give option to set themself as admin or orgnizer
                    }
                    else if (User.IsInRole("Reviewer") && role.Name == "Reviewer")
                    {
                        roles.Add(role); // If not admin don't give option to set themself as admin or orgnizer
                    }

                    ViewData["roles"] = roles;
                }
            }
            else
            {
                ViewData["roles"] = _roleManager.Roles.ToList(); // Pass the list of all Roles to View
            }
            return Page();
        }

        public async Task<IActionResult> OnGetOtherAsync(string userId)
        {
            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage");
            }

            if (userId == _userManager.GetUserId(User)) return Redirect("/Identity/Account/Manage");

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

            await LoadAsync(user);

            //var role = await _roleManager.FindByIdAsync(Input.UserRole);
            //ViewData["roleName"] = role.Name;

            ViewData["roles"] = _roleManager.Roles.ToList(); // Pass the list of all Roles to View
            ViewData["userId"] = UserID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //ViewData["roles"] = _roleManager.Roles.ToList(); // Pass the list of all Roles to View
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("~/");
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                StatusMessage = "Error: The form data is invalid";
                return Page();
            }

            var firstName = user.FirstName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }

            var lastName = user.LastName;
            if (Input.FirstName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            var address = user.Address;
            if (Input.Address != address)
            {
                user.Address = Input.Address;
                await _userManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0) // If user has chosen a file then save to database
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.Picture = dataStream.ToArray(); // Set picture value as byte array from chosen image
                }
                await _userManager.UpdateAsync(user);
            }   

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            StatusMessage = "Your profile has been updated";
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }


        /// <summary>
        ///  For editing other user's data
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostOtherAsync(string userId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Identity/Account/AccessDenied");
            }

            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage");
            }

            //ViewData["roles"] = _roleManager.Roles.ToList(); // Pass the list of all Roles to View
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Redirect("~/");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var firstName = user.FirstName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }

            var lastName = user.LastName;
            if (Input.FirstName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            var address = user.Address;
            if (Input.Address != address)
            {
                user.Address = Input.Address;
                await _userManager.UpdateAsync(user);
            }

            var oldRole = _userManager.GetRolesAsync(user).Result;
            var newRole = _roleManager.FindByIdAsync(Input.UserRole).Result;
            if (oldRole != newRole)
            {
                await _userManager.RemoveFromRoleAsync(user, oldRole.First());
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }

            if (Request.Form.Files.Count > 0) // If user has chosen a file then save to database
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.Picture = dataStream.ToArray(); // Set picture value as byte array from chosen image
                }
                await _userManager.UpdateAsync(user);
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                ViewData["userId"] = userId;
                return RedirectToPage("./Index", "Other", new { userId = userId });
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage("./Index", "Other", new { userId = userId });
                }
            }

            //await _signInManager.RefreshSignInAsync(User);
            StatusMessage = "The profile has been updated";
            return RedirectToPage("./Index", "Other", new { userId = userId });
        }
    }
}
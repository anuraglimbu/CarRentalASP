// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HajurKoCarRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using static System.Formats.Asn1.AsnWriter;

namespace HajurKoCarRental.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public EmailModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;

            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

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
            [Required(ErrorMessage = "Please provide the new email of the user")]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                //NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
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
            return Page();
        }

        public async Task<IActionResult> OnGetOtherAsync(string userId)
        {
            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage/Email");
            }

            if (userId == _userManager.GetUserId(User)) return Redirect("/Identity/Account/Manage/Email");

            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Identity/Account/AccessDenied");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Redirect("~/User");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            await LoadAsync(user);
            ViewData["userid"] = userId;
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

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                //var userId = await _userManager.GetUserIdAsync(user);

                var existingUser = await _userManager.FindByEmailAsync(Input.NewEmail);
                if (existingUser != null)
                {
                    StatusMessage = "Error because the email already exists";
                    return RedirectToPage();
                }

                await _emailStore.SetEmailAsync(user, Input.NewEmail, CancellationToken.None);
                await _emailStore.SetEmailConfirmedAsync(user, true, CancellationToken.None);
                await _userManager.UpdateSecurityStampAsync(user);
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    StatusMessage = "Error changing email.";
                    return Page();
                }

                // In our UI email and user name are one and the same, so when we update the email
                // we need to update the user name.
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.NewEmail);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Error changing user name.";
                    return Page();
                }

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Email has been changed";
                return RedirectToPage();
            }

            StatusMessage = "Email is unchanghed.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostOtherAsync(string userId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("/Identity/Account/AccessDenied");
            }

            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage/Email");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Redirect("~/User");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                //ViewData["userId"] = userId;
                return RedirectToPage("./Email", "Other", new { userId = userId });
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                //var userId = await _userManager.GetUserIdAsync(user);

                var existingUser = await _userManager.FindByEmailAsync(Input.NewEmail);
                if (existingUser != null)
                {
                    StatusMessage = "Error because the email already exists";
                    return RedirectToPage("./Email", "Other", new { userId = userId });
                }

                await _emailStore.SetEmailAsync(user, Input.NewEmail, CancellationToken.None);
                await _emailStore.SetEmailConfirmedAsync(user, true, CancellationToken.None);
                await _userManager.UpdateSecurityStampAsync(user);
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    StatusMessage = "Error changing email.";
                    return RedirectToPage("./Email", "Other", new { userId = userId });
                }

                // In our UI email and user name are one and the same, so when we update the email
                // we need to update the user name.
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.NewEmail);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Error changing user name.";
                    return RedirectToPage("./Email", "Other", new { userId = userId });
                }


                StatusMessage = "Email has been changed";
                return RedirectToPage("./Email", "Other", new { userId = userId });
            }

            StatusMessage = "Email is unchanghed.";
            return RedirectToPage("./Email", "Other", new { userId = userId });
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}

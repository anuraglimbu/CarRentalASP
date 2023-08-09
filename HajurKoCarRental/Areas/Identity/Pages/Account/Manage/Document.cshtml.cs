// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace HajurKoCarRental.Areas.Identity.Pages.Account.Manage
{
    public class DocumentModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public DocumentModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;

            _userStore = userStore;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public byte[]? IdentificationDocument { get; set; }

        public string? IdentificationDocumentsType { get; set; }

        public string Id { get; set; }


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
            [Display(Name = "New Identification Document")]
            public byte[]? NewIdentificationDocument { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Id = user.Id;
            IdentificationDocument = user.VerificationDocuments;
            IdentificationDocumentsType = user.VerificationDocumentsType;

            Input = new InputModel
            {
                //NewEmail = email,
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
            return Page();
        }

        public async Task<IActionResult> OnGetOtherAsync(string userId)
        {
            if (userId == null)
            {
                return Redirect("/Identity/Account/Manage/Document");
            }

            if (userId == _userManager.GetUserId(User)) return Redirect("/Identity/Account/Manage/Document");

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

            if (Request.Form.Files.Count > 0)
            {
                var wasNull = false;
                string successString = "";
                if(user.VerificationDocuments == null)
                {
                    wasNull = true;
                }

                IFormFile file = Request.Form.Files.FirstOrDefault();
                if ((file.Length / 1024f) / 1024f > 1.5)
                {
                    ModelState.AddModelError(string.Empty, "Identification Document File Size More Than 1.5MB");
                    await LoadAsync(user);
                    return Page();
                }

                user.VerificationDocumentsType = file.ContentType;
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.VerificationDocuments = dataStream.ToArray(); // Set document value as byte array from chosen image
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    successString = (wasNull) ? "Error adding Identification Document" : "Error changing Identification Document";
                    StatusMessage = successString;
                    return Page();
                }

                successString = (wasNull) ? "Identification Document has been added" : "Identification Document has been changed";

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = successString;
                return RedirectToPage();
            }

            StatusMessage = "Identification Document is unchanghed.";
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
                return Redirect("/Identity/Account/Manage/Document");
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
                return RedirectToPage("./Document", "Other", new { userId = userId });
            }

            if (Request.Form.Files.Count > 0)
            {
                var wasNull = false;
                string successString = "";
                if (user.VerificationDocuments == null)
                {
                    wasNull = true;
                }

                IFormFile file = Request.Form.Files.FirstOrDefault();
                user.VerificationDocumentsType = file.ContentType;
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.VerificationDocuments = dataStream.ToArray(); // Set picture value as byte array from chosen image
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    successString = (wasNull) ? "Error adding Identification Document" : "Error changing Identification Document";
                    StatusMessage = successString;
                    return RedirectToPage("./Document", "Other", new { userId = userId });
                }

                successString = (wasNull) ? "Identification Document has been added" : "Identification Document has been changed";
                
                StatusMessage = successString;
                return RedirectToPage("./Document", "Other", new { userId = userId });
            }

            StatusMessage = "Identification Document is unchanghed.";
            return RedirectToPage("./Document", "Other", new { userId = userId });
        }
    }
}

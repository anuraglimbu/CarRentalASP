using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HajurKoCarRental.Models
{
    public class SignUp
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please provide an email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please set a password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Required(ErrorMessage = "First name must be provided")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string? LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        [Required(ErrorMessage = "Please provide a phone number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please provide an address")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Address { get; set; }


        [Display(Name = "Profile Picture")]
        public byte[]? Picture { get; set; }


        [Display(Name = "Identification Document")]
        public byte[]? VerificationDocuments { get; set; }
    }
}

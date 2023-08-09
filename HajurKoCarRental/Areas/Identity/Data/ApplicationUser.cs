using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        public byte[]? Picture { get; set; }    // To Store Profile picture

        public byte[]? VerificationDocuments { get; set; } // To Store License or Citizenship

        public string? VerificationDocumentsType { get; set; } // To Store License or Citizenship extension

        public bool? IsLocked { get; set; } = false;

        public DateTime? LastLoggedIn { get; set; }
    }
}

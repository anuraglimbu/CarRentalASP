using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models
{
    public class Brands
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "You must give the Name of the brand")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}

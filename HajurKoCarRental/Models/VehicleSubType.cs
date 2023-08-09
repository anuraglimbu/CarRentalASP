using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models
{
    public class VehicleSubType
    {
        [Key]
        public int Id { get; set; }

        [HiddenInput]
        public int TypeId { get; set; }

        [Display(Name = "Sub Type")]
        [Required(ErrorMessage = "You must give the name of Sub Type")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}

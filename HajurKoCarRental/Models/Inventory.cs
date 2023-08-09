using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HajurKoCarRental.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "You must give set the brand of the vehicle")]
        public int BrandId { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "You must give set the type of the vehicle")]
        public int TypeId { get; set; }

        [Display(Name = "Sub Type")]
        [Required(ErrorMessage = "You must give set the sub type of the vehicle")]
        public int? SubTypeId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must give the name of the vehicle")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "You must provide the description of the vehicle")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Description { get; set; }


        [Display(Name = "Picture")]
        public byte[]? Picture { get; set; }

        [HiddenInput]
        [DataType(DataType.Text)]
        public string? PictureName { get; set; }

        [HiddenInput]
        [DataType(DataType.Text)]
        public string? PictureExtension { get; set; }

        [Display(Name = "Fuel")]
        [Required(ErrorMessage = "You must select the fuel type of the vehicle")]
        [DataType(DataType.Text)]      
        public string Fuel { get; set; }

        [Display(Name = "Transmission")]
        [Required(ErrorMessage = "You must select the transmission type of the vehicle")]
        [DataType(DataType.Text)]
        public string Transmission { get; set; }

        [Display(Name = "Plate Number")]
        [Required(ErrorMessage = "You must provide the number plate of the vehicle")]
        [DataType(DataType.Text)]
        public string PlateNumber { get; set; }

        [Display(Name = "Price Per Day (NPR)")]
        [Required(ErrorMessage = "You must provide the price per day of the vehicle")]
        public float PricePerDay { get; set; }


        [HiddenInput]
        public bool? OutOfDisplay { get; set; }

        [HiddenInput]
        public string? InserterId { get; set; }

        [HiddenInput]
        [DataType(DataType.DateTime)]
        public DateTime? RecordCreatedOn { get; set; }
    }
}

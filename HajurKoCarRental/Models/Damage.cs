using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models
{
    public class Damage
    {
        [Key]
        public int Id { get; set; }

        [HiddenInput]
        public int CarId { get; set; }

        [Display(Name = "Damage Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [HiddenInput]
        [DataType(DataType.DateTime)]
        public DateTime? RecordCreatedOn { get; set; }

        [Display(Name = "Repair Price")]
        public float? RepairPrice { get; set; }

        [HiddenInput]
        public int? Pending { get; set; }

        [HiddenInput]
        public int? Reviewed { get; set; }

        [HiddenInput]
        public int? Paid { get; set; }

        [HiddenInput]
        public string? RequesterId { get; set; }

        [HiddenInput]
        public string? ApproverId { get; set; }
    }
}

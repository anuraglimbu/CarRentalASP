using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HajurKoCarRental.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [HiddenInput]
        public int CarId { get; set; }

        [Display(Name = "Picking Up")]
        [DataType(DataType.DateTime)]
        public DateTime? PickingUp { get; set; }

        [Display(Name = "Dropping Off")]
        [DataType(DataType.DateTime)]
        public DateTime? DroppingOff { get; set; }

        [HiddenInput]
        [DataType(DataType.DateTime)]
        public DateTime? RecordCreatedOn { get; set; }

        [Display(Name = "Total Price")]
        public float? TotalPrice { get; set; }

        [HiddenInput]
        public int? Pending { get; set; }

        [HiddenInput]
        public int? Approved { get; set; }

        [HiddenInput]
        public int? Claimed { get; set; }

        [HiddenInput]
        public int? Returned { get; set; }

        [HiddenInput]
        public int? Paid { get; set; }

        [HiddenInput]
        public string? RequesterId { get; set; }

        [HiddenInput]
        public string? ApproverId { get; set; }
    }
}

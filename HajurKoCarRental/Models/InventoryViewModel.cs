using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HajurKoCarRental.Models
{
	[NotMapped]
	public class InventoryViewModel : Inventory
	{
		[Display(Name = "Brand")]
		public string? Brand;

		[Display(Name = "Vehicle Type")]
		public string? VehicleType;

		[Display(Name = "Vehicle Sub-Type")]
		public string? VehicleSubType;

		[Display(Name = "Inserter")]
		public string? InserterName;

		[Display(Name = "Inserter Profile")]
		public byte[]? InserterAvatar;
	}
}

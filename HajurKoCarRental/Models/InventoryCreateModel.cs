using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental.Models
{
    [NotMapped]
    public class InventoryCreateModel : Inventory
    {
        public List<Brands> Brands { get; set; }

        public List<VehicleType> VehicleTypes { get; set; }

        public Dictionary<int, List<VehicleSubType>> VehicleTypesAndSubTypes { get; set; }
    }
}

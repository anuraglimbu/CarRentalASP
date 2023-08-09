using Microsoft.AspNetCore.Mvc;

namespace HajurKoCarRental.Models
{
    public class RequestCreateViewModel : Request
    {
        public InventoryCreateModel CarData { get; set; }
    }
}

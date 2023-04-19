using System.ComponentModel.DataAnnotations;

namespace Detyre1PMVC.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public string ShipDesc { get; set; } = string.Empty;
    }
}

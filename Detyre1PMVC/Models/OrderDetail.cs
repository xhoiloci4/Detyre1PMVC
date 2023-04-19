namespace Detyre1PMVC.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int LiberId { get; set; }
        public virtual Libri Liber { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirst.Models
{
    public class Order_Items
    {
        [Key]
        public int Id { get; set; }

        //Globally unique identifier
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int UserId {  get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get;set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Price {  get; set; }

        public int Quantity { get; set; }

        public int TotalAmount {  get; set; }
    }
}

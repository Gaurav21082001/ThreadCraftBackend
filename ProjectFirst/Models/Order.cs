using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirst.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId {  get; set; }

        
        public int UserId {  get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int TotalAmount {  get; set; }

        public int ShippingCharge { get; set; } 

        public int NetAmount {  get; set; }

        public string Status {  get; set; }


        



    }
}

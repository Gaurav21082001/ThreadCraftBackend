using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectFirst.Models
{
    public class Product
    {
        [Key]
        public int ProductId {  get; set; }

        [Required(ErrorMessage ="Please enter product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter product description")]
        public string Description {  get; set; }
        [Required(ErrorMessage = "Please enter product price")]
        public int Price {  get; set; }
        [Required(ErrorMessage = "Please enter product stock")]
        public int Stock {  get; set; }
        [Required(ErrorMessage = "Please enter product category")]
        public int CategoryId {  get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public string ImageUrl {  get; set; }


        public int ActualPrice { get; set; }



    }
}

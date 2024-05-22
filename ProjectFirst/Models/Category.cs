using System.ComponentModel.DataAnnotations;

namespace ProjectFirst.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Please enter category name")]
        public string CategoryName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ProjectFirst.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter mail")]
        [EmailAddress(ErrorMessage ="Please mention valid email")]
        
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Please enter mobile number")]
        [RegularExpression(@"^[9,8,7,6]{1}\d{9}$",ErrorMessage ="Please enter valid mobile number")]

        public string PhoneNumber { get; set; }
        [AllowNull]
        public string Address {  get; set; }

        public string Role { get; }

    }
}

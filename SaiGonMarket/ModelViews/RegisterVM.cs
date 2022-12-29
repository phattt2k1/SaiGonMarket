using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaiGonMarket.ModelViews
{
    public class RegisterVM
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "ValidateEmail", controller: "Accounts")]
        public string Email { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "ValidatePhone", controller: "Accounts")]
        public string Phone { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters")]
        public string Password { get; set; }

        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters")]
        [Display(Name = "Re-enter your password")]
        [Compare("Password", ErrorMessage = "Please enter the same password")]
        public string ConfirmPassword { get; set; }
    }
}

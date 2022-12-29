using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaiGonMarket.ModelViews
{
    public class LoginVM
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = "Please enter your Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name ="Email")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters")]
        public string Password { get; set; }
    }
}

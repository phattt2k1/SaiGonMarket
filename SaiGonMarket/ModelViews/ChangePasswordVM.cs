using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaiGonMarket.ModelViews
{
    public class ChangePasswordVM
    {

        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Current password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
        public string PasswordNow { get; set; }

        [Display(Name = "Enter a new password")]
        [Required(ErrorMessage = "Please re-enter your password")]
        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters")]
        public string Password { get; set; }


        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters")]
        [Display(Name = "Enter a new password")]
        [Compare("Password", ErrorMessage = "Please enter the same password")]
        public string ConfirmPassword { get; set; }
    }
}

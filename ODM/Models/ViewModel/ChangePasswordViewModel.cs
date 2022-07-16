using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODM.Models
{
    public class ChangePasswordViewModel
    {

        [DisplayName("Password")]
        [Required]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu cũ phải từ 6 đến 100 kí tự")]
        public string oldPassword { get; set; }

        [DisplayName("New password")]
        [Required]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu phải từ 6 đến 100 kí tự")]
        public string password { get; set; }

        [NotMapped]
        [DisplayName("Confirm password")]
        [Compare("password", ErrorMessage = "Mật khẩu không khớp")]
        public string confirmPassword { get; set; }
    }
}
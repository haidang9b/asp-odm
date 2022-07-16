using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODM.Models
{
    public class LoginViewModel
    {
        [DisplayName("Tên tài khoản")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Độ dài tài khoản phải từ 4 đến 100 kí tự")]
        [Required]
        public string username { get; set; }

        [DisplayName("Mật khẩu")]
        [Required]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu phải từ 6 đến 100 kí tự")]
        public string password { get; set; }
    }
}
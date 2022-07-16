using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODM.Models.ViewModel
{
    public class UpdatePasswordViewModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        [DisplayName("Username")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Độ dài tài khoản phải từ 4 đến 100 kí tự")]
        public string username { get; set; }
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [DisplayName("Fullname")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Độ dài tên phải từ 2 kí tự trở lên")]
        public string fullName { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu phải từ 6 đến 100 kí tự")]
        public string password { get; set; }
        [NotMapped]
        [DisplayName("Confirm password")]
        [Compare("password", ErrorMessage = "Mật khẩu không khớp")]
        public string confirmPassword { get; set; }
        [DefaultValue("user")]
        [DisplayName("Quyền")]
        public string role_id { get; set; }
    }
}
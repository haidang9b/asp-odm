using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODM.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        [DisplayName("Username")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Độ dài tài khoản phải từ 4 đến 100 kí tự")]
        public string username { get; set; }
        [Required(ErrorMessage ="Họ và tên là bắt buộc")]
        [DisplayName("Fullname")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Độ dài tên phải từ 2 kí tự trở lên")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        [DisplayName("Gender")]
        [DefaultValue(true)] // true = nam && false = nữ
        public bool gender { get; set; }
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DisplayName("Date of birth")]
        [StringLength(20, MinimumLength =6)]
        public string birthday { get; set; }
        [Required(ErrorMessage = "Ngày vào công ti là bắt buộc")]
        [DisplayName("Join date")]
        [StringLength(20, MinimumLength = 6)]
        public string started { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc")]
        [DisplayName("Email")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Độ dài email phải từ 4 kí tự trở lên")]
        public string email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu phải từ 6 đến 100 kí tự")]
        public string password { get; set; }

        [NotMapped]
        [DisplayName("Confirm password")]
        [Compare("password", ErrorMessage ="Mật khẩu không khớp")]
        public string confirmPassword { get; set; }
        [DisplayName("Avatar")]
        public string avatar { get; set; }
        
        [DefaultValue("user")]
        [DisplayName("Role")]
        public string role_id { get; set; }
        [ForeignKey("role_id")]
        public Role role { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODM.Models.ViewModel
{
    public class EditUserViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [DisplayName("Fullname")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Độ dài tên phải từ 2 kí tự trở lên")]
        public string fullName { get; set; }
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        [DisplayName("Gender")]
        [DefaultValue(true)] // true = nam && false = nữ
        public bool gender { get; set; }
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DisplayName("Date of birth")]
        [StringLength(20, MinimumLength = 6)]
        public string birthday { get; set; }
        [DisplayName("Join date")]
        public string started { get; set; }
        [DefaultValue("user")]
        [DisplayName("Role")]
        public string role_id { get; set; }
    }
}
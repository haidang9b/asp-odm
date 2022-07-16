using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ODM.Models.ViewModel
{
    public class AccountFilerViewModel
    {
        [DisplayName("Tên tài khoản")]
        public string username { get; set; }
        [DisplayName("Họ và tên")]
        public string fullName { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ODM.Models.ViewModel
{
    public class BorrowViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Username")]
        public string username { get; set; }
        [DisplayName("Fullname")]
        public string fullname { get; set; }
        [DisplayName("Device ID")]
        public int idProduct { get; set; }
        [DisplayName("Device")]
        public string nameProduct { get; set; }
        [DisplayName("Recevied date")]
        public string timeReceive { get; set; }
        [DisplayName("Returned date")]
        public string timeReturn { get; set; }
        [DisplayName("Status")]
        public bool isReturn { get; set; }
    }
}
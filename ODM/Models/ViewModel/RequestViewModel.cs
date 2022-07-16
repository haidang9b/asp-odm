using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ODM.Models.ViewModel
{
    public class RequestViewModel
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
        [DisplayName("Requested date")]
        public string timeRequest { get; set; }

        [DisplayName("Completed date")]
        public string timeCompletion { get; set; }

        [DisplayName("Direction")]
        public bool isReturn { get; set; }

        [DisplayName("Status")]
        public int state { get; set; } // 1 is success and -1 is decline 0 is waiting

    }
}
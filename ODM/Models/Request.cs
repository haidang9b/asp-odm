using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODM.Models
{
    [Table("Request")]
    public class Request
    {
        [Key]
        [DisplayName("Mã yêu cầu")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [DisplayName("Người dùng")]
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual User user { get; set; }

        [DisplayName("Sản phẩm")]
        public int product_id { get; set; }
        [ForeignKey("product_id")]
        public virtual Product product { get; set; }

        [DisplayName("Thời gian yêu cầu")]
        public string timeRequest { get; set; }

        [DisplayName("Thời gian hoàn thành")]
        public string timeCompletion { get; set; }
        public bool isReturn { get; set; } // false khi admin gui cho user -- true khi user tra thiet bi

        [DisplayName("Trạng thái")]
        public int state { get; set; } // 1 is success and -1 is decline 0 is waiting


    }
}
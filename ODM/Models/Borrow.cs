using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODM.Models
{
    [Table("Borrow")]
    public class Borrow
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [DisplayName("User")]
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual User user { get; set; }
        [DisplayName("Device")]
        public int product_id { get; set; }
        [ForeignKey("product_id")]
        public virtual Product product { get; set; }
        [DisplayName("Received date")]
        public String timeReceive { get; set; }
        [DisplayName("Returned date")]
        public String timeReturn { get; set; }
        public bool isReturn { get; set; }
    }
}
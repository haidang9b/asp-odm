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
    [Table("Product")]
    public class Product
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Độ dài tên thiết bị phải từ 3 đến 300 kí tự trở lên")]
        [DisplayName("Device")]
        public string name { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Độ dài mô tả thiết bị phải từ 10 đến 500 kí tự trở lên")]
        [DisplayName("Description")]
        public string description { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Hình ảnh thiết bị từ 3 kí tự trở lên")]
        [DisplayName("Image")]
        public string image { get; set; }

        [DisplayName("Status")]
        public bool status { get; set; }

        [DisplayName("Install date")]
        public string installDay { get; set; }

        [DisplayName("Category")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual Category Category { get; set; }
    }
}
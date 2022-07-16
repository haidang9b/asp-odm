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
    [Table("Category")]
    public class Category
    {
        [Key]
        [DisplayName("Mã loại")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Độ dài tên loại thiết bị phải từ 3 kí tự trở lên")]
        [DisplayName("Tên loại thiết bị")]
        public string name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
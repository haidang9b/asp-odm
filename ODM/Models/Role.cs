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
    [Table("Role")]
    public class Role
    {
        [Key]
        [DisplayName("Mã quyền")]
        public string id { get; set; }
        [DisplayName("Tên quyền")]
        public string name { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
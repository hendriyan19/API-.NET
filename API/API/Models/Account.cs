using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public virtual Profiling Profiling { get; set; }
        public virtual ICollection<AccountRole> accountrole
        {get; set;}


    }
}
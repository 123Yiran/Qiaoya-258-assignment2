using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZQY.Models
{
    public class Season
    {
        [Key]

        public int ID { get; set; }
       /* [Display(Name = "Please Enter The Key Words")]*/
        public string Name { get; set; }
        public virtual ICollection<Every> Everies { get; set; }
    }
}
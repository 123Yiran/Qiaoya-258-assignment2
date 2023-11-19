using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZQY.Models
{
    public class Every
    {
        [Key]
        public int ID { get; set; }
        /*我的属性--标题时长描述*/
      /*  [Display(Name="Please Enter The Key Words")]*/
        public string Name { get; set; }
        public string Length { get; set; }
        public string Description { get; set; }
        public int? SeasonID { get; set; }
        /*这是什么*/
        public virtual Season Season { get; set; }
    }
}
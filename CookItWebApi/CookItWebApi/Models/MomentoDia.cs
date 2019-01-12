using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    //[Table("MomentosDias")]
    public class MomentoDia
    {
        [Key]
        public int _IdMomentoDia { get; set; }
        [Required]
        public string _Nombre { get; set; }

    }
}

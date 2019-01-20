using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    //[Table("Estaciones")]
    public class Estacion
    {                            
            public int _IdEstacion { get; set; }
            [Required]
            public string _Nombre { get; set; }
        
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    //[Table("HistorialRecetas")]
    public class HistorialReceta
    {
       
        

        public string _Email { get; set; }
        public int _IdHistorialReceta { get; set; }

        [JsonIgnore]
        public virtual Usuario _Usuario { get; set; }

        public int _IdReceta { get; set; }
        public virtual Receta _Receta { get; set; }

        [Required]
        public DateTime _FechaHora { get; set; }        
        
    }
}

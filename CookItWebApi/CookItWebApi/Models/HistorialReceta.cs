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
        [JsonIgnore]
        public Usuario _Usuario { get; set; }

        public int _IdReceta { get; set; }
        [JsonIgnore]
        public Receta _Receta { get; set; }        

        public DateTime _FechaHora { get; set; }        
        
    }
}

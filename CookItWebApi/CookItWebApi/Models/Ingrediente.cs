using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CookItWebApi.Models
{
    //[Table("Ingredientes")]
    public class Ingrediente
    {
       
        public enum TipoMedida {
            ml = 1,
            gr = 2,
            un = 3
        }

        
        public int _IdIngrediente { set; get; }
        [Required]
        
        public string _Nombre { set; get; }
        [Required]
        
        public int _Costo { set; get; }

        public TipoMedida _Medida { set; get; }

        [Required]
        
        public int _MedidaPromedio { set; get; }
        [Required]
        
        public int _MedidaPorGramo { set; get; }
        [Required]
        
        public int _CantCaloriasPorMedida { set; get; }
        [Required]
        public bool _AptoCeliacos { set; get; }
        [Required]
        public bool _AptoDiabeticos{ set; get; }
        [Required]
        public bool _AptoVegetarianos { set; get; }
        [Required]
        public bool _AptoVeganos { set; get; }
        [Required]
        public int _IdEstacion { set; get; }
        
        [JsonIgnore]
        public Estacion _Estacion { set; get; }
        [Required]
        public int _IdTipoIngrediente { set; get; }
        
        [JsonIgnore]
        public TipoIngrediente _TipoIngrediente { get; set; }

    }
}

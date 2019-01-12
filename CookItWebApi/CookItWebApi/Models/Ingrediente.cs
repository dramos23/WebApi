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
        //public enum Tipo {
        //    Aceites = 1,
        //    Carnes = 2,
        //    Cereales y Derivados = 3,
        //    Cremas de untar = 4,
        //    Especias y Hierbas = 5,
        //    Fiambres y Embutidos = 6,
        //    Frutas y Verduras = 7,
        //    Frutos secos y Semillas = 8,
        //    Ingredientes para hornear = 9,
        //    Leche y productos lacteos = 10,
        //    Pastas y Fideos = 11,
        //    Pescado y Mariscos = 12,
        //    Salsas = 13
        //}
        //public enum Estacion {
        //    Verano = 1,
        //    Otono = 2,
        //    Invierno = 3,
        //    Primavera = 4,
        //    Varios = 5
        //}
       
        public enum TipoMedida {
            ml = 1,
            gr = 2
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
        public virtual Estacion _Estacion { set; get; }
        [Required]
        public int _IdTipoIngrediente { set; get; }
        
        [JsonIgnore]
        public virtual TipoIngrediente _TipoIngrediente { get; set; }


    }
}

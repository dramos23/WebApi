using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CookItWebApi.Models
{
    [Table("Ingredientes")]
    public class Ingrediente
    {
        public enum Tipo {
            Aceites = 1,
            Carne = 2,
            Cereales_y_Derivados = 3,
            Cremas_de_untar = 4,
            Especias_y_hierbas = 5,
            Fiambres_y_embutidos = 6,
            Frutas_y_Verduras = 7,
            Frutos_secos_y_semillas = 8,
            Ingredientes_para_hornear = 9,
            Leche_y_productos_lacteos = 10,
            Pastas_y_fideos = 11,
            Pescado_y_mariscos = 12,
            Salsas = 13
        }
        public enum Estacion {
            Verano = 1,
            Otono = 2,
            Invierno = 3,
            Primavera = 4,
            Varios = 5
        }
       
        public enum TipoMedida {
            ml = 1,
            gr = 2
        }

        [Key]
        public int _Id { set; get; }
        [Required]
        [StringLength(200)]
        public string _Nombre { set; get; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int _Costo { set; get; }

        public TipoMedida _Medida { set; get; }

        [Required]
        [Range(0, int.MaxValue)]
        public int _MedidaPromedio { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
        public int _MedidaPorGramo { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
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
        public Estacion _Estacion { set; get; }
        [Required]
        public int _IdTipo { set; get; }
        [ForeignKey("_IdTipo")]
        [JsonIgnore]
        public TipoIngrediente _Tipo { get; set; }


    }
}

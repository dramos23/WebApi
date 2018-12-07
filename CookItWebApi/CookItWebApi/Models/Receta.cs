using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CookItWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CookItWebApi.Models
{
    [Table("Recetas")]
    public class Receta
    {
        public enum MomentoDia {
            Desayuno = 1,
            Cena = 2,
            Merienda = 3,
            Almuerzo = 4,
            Desayuno_Merienda = 5,
            Almuerzo_Cena = 6,
            Variado = 7
        }

        [Key]
        public int _IdReceta { get; set; }
        [Required]
        public string _Titulo { get; set; }
        [Required]
        public MomentoDia _MomentoDia { get; set; }
        [Required]
        public Ingrediente.Estacion _Estacion { get; set; }
        [Required]
        public int _Dificultad { get; set; }
        [Required]
        public int _TiempoPreparacion { get; set; }
        [Required]
        public string _Descripcion { get; set; }

        //public Pais _PaisOrigen { get; set; }
        public byte[] _Foto { get; set; }

        [Required]
        public string _EmailCreador { get; set; }
        [ForeignKey("_EmailCreador")]
        [JsonIgnore]
        public UserInfo _Creador { get; set; }
        
        public int _CantPlatos { get; set; }
        public float _Costo { get; set; }
        public DateTime _FechaCreacion { get; set; }
        public float _PuntajeTotal { get; set; }
        public bool _AptoCeliacos { get; set; }
        public bool _AptoDiabeticos { get; set; }
        public bool _AptoVegetarianos { get; set; }
        public bool _AptoVeganos { get; set; }
        public bool _Habilitada { get; set; }
        public List<IngredienteReceta> _IngredientesReceta { set; get; }
        public List<PasoReceta> _Pasos { set; get; }

        public Receta() {

            _IngredientesReceta = new List<IngredienteReceta>();
            _Pasos = new List<PasoReceta>();
        }

        //Constructor para traer de base de datos
        //public Receta(int Id, int MomentoDia,int Estacion, int Dificultad, int TiempoPreparacion, int PaisOrigen, byte[] Foto, 
        //    Usuario Creador, int CantPlatos, float Costo, DateTime FechaCreacion, float PuntajeTotal, bool AptoCeliacos, 
        //    bool AptoDiabeticos, bool AptoVegetarianos, bool AptoVeganos, bool Habilitada, List<IngredienteReceta> Ingredientes, 
        //    List<PasoReceta> Pasos)
        //{
        //    _Id = Id;
        //    _MomentoDia = (MomentoDia)MomentoDia;
        //    _Estacion = (Ingrediente.Estacion)Estacion;
        //    _Dificultad = Dificultad;
        //    _TiempoPreparacion = TiempoPreparacion;
        //    _PaisOrigen = (Pais) PaisOrigen;
        //    _Foto = Foto;
        //    _Creador = Creador;
        //    _CantPlatos = CantPlatos;
        //    _Costo = Costo;
        //    _FechaCreacion = FechaCreacion;
        //    _PuntajeTotal = PuntajeTotal;
        //    _AptoCeliacos = AptoCeliacos;
        //    _AptoDiabeticos = AptoDiabeticos;
        //    _AptoVegetarianos = AptoVegetarianos;
        //    _AptoVeganos = AptoVeganos;
        //    _Habilitada = Habilitada;
        //    _Ingredientes = Ingredientes;
        //    _Pasos = Pasos;           
        //}

        ////Constructor para insercion en base de datos, el resto de los atributos se insertan por trigger en BD.
        //public Receta(int MomentoDia, int Estacion, int Dificultad, int TiempoPreparacion, int PaisOrigen, byte[] Foto, Usuario Creador, 
        //    int CantPlatos, List<IngredienteReceta> Ingredientes, List<PasoReceta> Pasos)
        //{     
        //    _MomentoDia = (MomentoDia) MomentoDia;
        //    _Estacion = (Ingrediente.Estacion) Estacion;
        //    _Dificultad = Dificultad;
        //    _TiempoPreparacion = TiempoPreparacion;
        //    _PaisOrigen = (Pais) PaisOrigen;
        //    _Foto = Foto;
        //    _Creador = Creador;
        //    _CantPlatos = CantPlatos;
        //    _Ingredientes = Ingredientes;
        //    _Pasos = Pasos;
        //}


    }


}

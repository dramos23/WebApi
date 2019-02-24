using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CookItWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CookItWebApi.Models
{
    //[Table("Recetas")]
    public class Receta
    {
        
        public int _IdReceta { get; set; }

        [Required]
        public string _Email { get; set; }     
        [JsonIgnore]
        public virtual Perfil _Perfil { get; set; }

        [Required]
        public string _Titulo { get; set; }

        [Required]
        public string _Descripcion { get; set; }

        [Required]
        public int _IdMomentoDia { get; set; }        
        [JsonIgnore]
        public MomentoDia _MomentoDia { get; set; }

        [Required]
        public int _IdEstacion { get; set; }        
        [JsonIgnore]
        public Estacion _Estacion { get; set; }

        [Required]
        public int _Dificultad { get; set; }

        [Required]
        public int _TiempoPreparacion { get; set; }
        //public Pais _PaisOrigen { get; set; }
        public byte[] _Foto { get; set; }
        public bool _Habilitada { get; set; }


        
        public int _CantPlatos { get; set; }
        public int _CantCalorias { get; set; }
        public float _Costo { get; set; }
        public DateTime _FechaCreacion { get; set; }
        public double _PuntajeTotal { get; set; }
        public bool _AptoCeliacos { get; set; }
        public bool _AptoDiabeticos { get; set; }
        public bool _AptoVegetarianos { get; set; }
        public bool _AptoVeganos { get; set; }
        
        public List<IngredienteReceta> _ListaIngredientesReceta { set; get; }
        public List<PasoReceta> _ListaPasosReceta { set; get; }
        public List<ComentarioReceta> _ListaComentariosReceta { get; set; }        
            

        public Receta() {

            //_ListaIngredientesReceta = new List<IngredienteReceta>();
            //_ListaPasosReceta = new List<PasoReceta>();
            //_ListaComentariosReceta = new List<ComentarioReceta>();
        }

        public void CalcularAtributos() {


            foreach (IngredienteReceta ingredienteReceta in _ListaIngredientesReceta)
            {
                Ingrediente ingrediente = ingredienteReceta._Ingrediente;
                ingredienteReceta._Ingrediente = null;

                var a = ingredienteReceta._Cantidad;
                var b = ingrediente._MedidaPromedio;

                int parteEntera = a / b;
                double parteDecimal = a % b;
                parteEntera += parteDecimal > 0 ? 1 : 0;

                _Costo += ingrediente._Costo * parteEntera;
                _CantCalorias += ingredienteReceta._Cantidad * ingrediente._CantCaloriasPorMedida / ingrediente._MedidaPorGramo;
                _AptoCeliacos = Convert.ToBoolean(Convert.ToInt32(_AptoCeliacos) * Convert.ToInt32(ingrediente._AptoCeliacos));
                _AptoDiabeticos = Convert.ToBoolean(Convert.ToInt32(_AptoDiabeticos) * Convert.ToInt32(ingrediente._AptoDiabeticos));
                _AptoVeganos = Convert.ToBoolean(Convert.ToInt32(_AptoVeganos) * Convert.ToInt32(ingrediente._AptoVeganos));
                _AptoVegetarianos = Convert.ToBoolean(Convert.ToInt32(_AptoVegetarianos) * Convert.ToInt32(ingrediente._AptoVegetarianos));

            };

        }

    }


}

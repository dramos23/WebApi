using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    //[Table("Perfiles")]
    public class Perfil
    {
        
        public string _Email { get; set; }        
        [JsonIgnore]
        public virtual Usuario _Usuario { get; set; }        
        public byte[] _Foto { set; get; }
        [Required]
        public string _NombreUsuario { set; get; }
        [Required]
        public string _Nombre { set; get; }
        [Required]
        public string _Apellido { set; get; }

        [Required]
        public bool _FiltroAutomatico { set; get; }
        [Required]
        public bool _FiltroPrecio { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
        public int _FiltroPrecioMin { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
        public int _FiltroPrecioMax { set; get; }
        [Required]
        public bool _FiltroVegetariano { set; get; }
        [Required]
        public bool _FiltroVegano { set; get; }
        [Required]
        public bool _FiltroDiabetico { set; get; }
        [Required]
        public bool _FiltroCeliaco { set; get; }
        [Required]
        public bool _FiltroCalorias { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
        public int _FiltroCaloriasMin { set; get; }
        [Required]        
        public int _FiltroCaloriasMax { set; get; }
        //[Required]
        //public bool _FiltroPaisOrigen { set; get; }
        //public int _FiltroPaisOrigenId { set; get; }
        [Required]
        public bool _FiltroMomentoDia { set; get; }        
        public int? _FiltroMomentoDiaId { set; get; }
        [Required]
        public bool _FiltroPuntuacion { set; get; }
        [Required]        
        public int _FiltroPuntuacionMin { set; get; }
        [Required]        
        public int _FiltroPuntuacionMax { set; get; }
        [Required]
        public bool _FiltroEstacion { set; get; }
        
        public int? _FiltroEstacionId { set; get; }
        [Required]
        public bool _FiltroDificultad { set; get; }
        [Required]        
        public int _FiltroDificultadMin { set; get; }
        [Required]        
        public int _FiltroDificultadMax { set; get; }
        [Required]
        public bool _FiltroCantPlatos { set; get; }
        [Required]        
        public int _FiltroCantPlatosMin { set; get; }
        [Required]        
        public int _FiltroCantPlatosMax { set; get; }
        [Required]
        public bool _FiltroTiempoPreparacion { set; get; }
        [Required]        
        public int _FiltroTiempoPreparacionMin { set; get; }
        [Required]        
        public int _FiltroTiempoPreparacionMax { set; get; }
        [Required]
        public bool _FiltroIngredientes { set; get; }


        public List<IngredienteUsuario> _ListaIngredientesUsuario { get; set; }
        
        public List<Reto> _ListaRetos { get; set; }
        
        public List<Notificacion> _ListaNotificaciones { get; set; }
        
        public List<RecetaFavorita> _ListaRecetasFavoritas { get; set; }

        public Perfil()
        {
            //_ListaIngredientesUsuario = new List<IngredienteUsuario>();
            //_ListaRetos = new List<Reto>();
            //_ListaNotificaciones = new List<Notificacion>();
            //_ListaRecetasFavoritas = new List<RecetaFavorita>();
        }
    }



}

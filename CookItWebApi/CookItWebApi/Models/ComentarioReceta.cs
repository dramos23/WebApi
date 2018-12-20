using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    [Table("ComentariosRecetas")]
    public class ComentarioReceta
    {

        
        [Key]
        [Column(Order = 1)]
        public int _IdReceta { set; get; }
        [ForeignKey("_IdReceta")]
        [JsonIgnore]
        public Receta _Receta { set; get; }

        [Key]
        [Column(Order = 2)]
        public int _IdComentario { get; set; }

        [Column(Order = 3)]
        public string _EmailUsuario { get; set; }
        [ForeignKey("_EmailUsuario")]
        [JsonIgnore]
        public UserInfo _Creador { get; set; }



        [Required]
        public string _Comentario { get; set; }

        [Required]
        public DateTime _Fecha { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int _Puntaje { get; set; }

        //public ComentarioReceta(int IdComentario, int IdReceta, string EmailUsuario, string Comentario, DateTime Fecha, int Puntaje)
        //{
        //    _IdComentario = IdComentario;
        //    _IdReceta = _IdReceta;
        //    _EmailUsuario = EmailUsuario;
        //    _Comentario = Comentario;
        //    _Fecha = Fecha;
        //    _Puntaje = Puntaje;

        //}

        //public ComentarioReceta(int IdReceta, string EmailUsuario, string Comentario, DateTime Fecha, int Puntaje)
        //{            
        //    _IdReceta = _IdReceta;
        //    _EmailUsuario = EmailUsuario;
        //    _Comentario = Comentario;
        //    _Fecha = Fecha;
        //    _Puntaje = Puntaje;
        //}
    }
}


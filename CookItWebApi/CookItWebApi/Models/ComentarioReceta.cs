using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    //[Table("ComentariosRecetas")]
    public class ComentarioReceta
    {

        

        public int _IdReceta { set; get; }
        public int _IdComentario { get; set; }
        public string _Email { get; set; }

        [JsonIgnore]
        public Receta _Receta { set; get; }                
        
        [JsonIgnore]
        public Usuario _Creador { get; set; }



        [Required]
        public string _Comentario { get; set; }

        [Required]
        public DateTime _Fecha { get; set; }

        [Required, Range(1, 5)]
        public int _Puntaje { get; set; }

    }
}


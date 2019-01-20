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

        public int _IdComentario { get; set; }
        public int _IdReceta { set; get; }
        [JsonIgnore]
        public virtual Receta _Receta { set; get; }
        public string _Email { get; set; }
        [JsonIgnore]
        public virtual Perfil _Perfil { get; set; }
        


        [Required]
        public string _Comentario { get; set; }

        [Required]
        public DateTime _Fecha { get; set; }

        [Required]
        public int _Puntaje { get; set; }

    }
}


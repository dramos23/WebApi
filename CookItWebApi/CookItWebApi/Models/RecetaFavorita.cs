using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class RecetaFavorita
    {
        [Key]
        [Column(Order = 1)]
        public string _EmailUsuario { get; set; }
        [ForeignKey("_EmailUsuario")]
        [JsonIgnore]
        public UserInfo _Creador { get; set; }

        [Key]
        [Column(Order = 2)]
        public int _IdReceta { set; get; }
        [ForeignKey("_IdReceta")]
        public virtual Receta _Receta { set; get; }


        [Column(Order = 4)]
        public DateTime _FechaHora { get; set; }
    }
}

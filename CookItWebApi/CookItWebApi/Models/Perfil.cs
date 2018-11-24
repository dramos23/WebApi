using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Perfil
    {
        [Key]
        public string _Email { get; set; }
        [ForeignKey("_Email")]
        [JsonIgnore]
        public UserInfo _Creador { get; set; }
        
        public byte[] _Foto { set; get; }
        [Required]
        public string _NombreUsuario { set; get; }
        [Required]
        public string _Nombre { set; get; }
        [Required]
        public string _Apellido { set; get; }

    }
}

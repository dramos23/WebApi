using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class IngredienteUsuario : IngredienteConCantidad
    {
        //[Key, Column(Order = 1)]   
        public string _Email { set; get; }        
        
        [JsonIgnore]
        public Perfil _Perfil { set; get; }
    }
}

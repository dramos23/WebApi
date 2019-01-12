using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Text;

namespace CookItWebApi.Models
{
    //[Table("IngredientesRecetas")]
    public class IngredienteReceta : IngredienteConCantidad
    {                
        
        public int _IdReceta { set; get; }        
        
        [JsonIgnore]        
        public virtual Receta _Receta { set; get; }                       
        

    }
}

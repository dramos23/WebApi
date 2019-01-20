using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookItWebApi.Models
{
    //[Table("TiposIngredientes")]

    public class TipoIngrediente
    {        
        public int _IdTipoIngrediente { get; set; }
        [Required]        
        public string _Nombre { get; set; }
    }
}

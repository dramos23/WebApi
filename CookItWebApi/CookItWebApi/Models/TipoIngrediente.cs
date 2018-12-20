using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookItWebApi.Models
{
    [Table("TipoIngrediente")]
    public class TipoIngrediente
    {
        [Key]
        public int _IdTipo { get; set; }
        [Required]
        
        public int _Nombre { get; set; }
    }
}

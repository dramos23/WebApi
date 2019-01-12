
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CookItWebApi.Models
{
    [NotMapped]
    public abstract class IngredienteConCantidad
    {
        public int _IdIngrediente { set; get; }

        public virtual Ingrediente _Ingrediente { set; get; }

        
        [Required, Range(0, Double.PositiveInfinity), Column(Order = 3)]
        public int _Cantidad { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class IngredientesAux
    {
        public List<Ingrediente> ListIng { get; set; }

        public IngredientesAux(List<Ingrediente> listIng)
        {
            ListIng = listIng;
        }
    }


}

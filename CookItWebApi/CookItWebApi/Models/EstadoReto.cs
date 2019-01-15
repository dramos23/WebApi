using System;
using System.Collections.Generic;
using System.Text;

namespace CookItWebApi.Models
{
    public class EstadoReto
    {
        public int _IdEstadoReto { get; set; }
        public string _Estado { get; set; }

        public EstadoReto(int IdEstadoReto, string Estado)
        {
            _IdEstadoReto = IdEstadoReto;
            _Estado = Estado;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Supermercado
    {
        public int _IdSupermercado { get; set; }

        public string _Nombre { get; set; }

        public string _Empresa { get; set; }

        public byte[] _Foto { get; set; }

        public string _Dirección { get; set; }

        public string _Telefono { get; set; }

        public Decimal _Longitud { get; set; }

        public Decimal _Latitud { get; set; }
    }
}


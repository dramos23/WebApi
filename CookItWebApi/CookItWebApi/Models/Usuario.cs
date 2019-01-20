using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Usuario
    {
        public enum Tipo
        {
            Local = 1,
            Google = 2,
            Facebook = 3
        }

        public string _Email { get; set; }
        public System.Guid? _DeviceId { get; set; }
        public Tipo _Tipo { get; set; }
        public string _Password { get; set; }        
        public Perfil _Perfil { get; set; }
        public DateTime _UltimoIngreso { get; set; }
        public List<HistorialReceta> _ListaHistorialRecetas { get; set;}


    }
}

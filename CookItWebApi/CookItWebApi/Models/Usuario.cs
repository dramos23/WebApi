using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Usuario
    {
        public enum TipoCuenta
        {
            Local = 1,
            Google = 2,
            Facebook = 3
        }

        public enum TipoUsuario
        {
            Desarrollador = 0,
            Adminitrador = 1,
            Cliente = 2,            
        }
        
        public string _Email { get; set; }        
        public System.Guid? _DeviceId { get; set; }
        public TipoCuenta _TipoCuenta { get; set; }
        public TipoUsuario _TipoUsuario { get; set; }
        public string _Password { get; set; }        
        public Perfil _Perfil { get; set; }
        public DateTime _UltimoIngreso { get; set; }
        public List<HistorialReceta> _ListaHistorialRecetas { get; set;}

        public bool ValidarEmail()
        {

            Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
            Match match = regex.Match(_Email);

            return match.Success;
        }


    }
}

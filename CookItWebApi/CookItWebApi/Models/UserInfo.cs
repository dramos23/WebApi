using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class UserInfo
    {
        //public string _UserName { get; set; } 
        [Key]
        public string _Email { get; set; }      
        public string _Password { get; set; }



        //[Key]
        //[EmailAddress]
        //public string _Email { set; get; }
        //[Required(ErrorMessage = "La contraseña es un dato requerido.")]
        //[DataType(DataType.Password)]
        //public string _Pass { set; get; }
        //public byte[] _Foto { set; get; }
        //public string _NombreUsuario { set; get; }
        //public string _Nombre { set; get; }
        //public string _Apellido { set; get; }

        //public TipoUsuario _Tipo { get; set; }

        //public enum TipoUsuario { Administrador = 1, Cliente = 2 };

    }
}

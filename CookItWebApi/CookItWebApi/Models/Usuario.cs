using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Usuario
    {
        
        
        public string _Email { get; set; }      
        public string _Password { get; set; }

        public Perfil _Perfil { get; set; }

    }
}

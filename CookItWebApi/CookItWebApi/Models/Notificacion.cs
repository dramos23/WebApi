using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Notificacion
    {
        public enum Estado
        {
            SinLeer = 0,
            Leido = 1,
        }

        public string _NotificacionId { get; set; }

        public string _Email { get; set; }
        [JsonIgnore]
        public virtual Perfil _Perfil { get; set; }

        public DateTime _FechaHora { get; set; }

        public Estado _Estado { get; set; }

        public string _Titulo { get; set; }

        public string _Descripcion { get; set; }

        public string _Pk1 { get; set; }

        public string _Pk2 { get; set; }

        public string _Pk3 { get; set; }

        public string _Pk4 { get; set; }
        public string _Tabla { get; set; }

        public Notificacion()
        {
            //_Claves = new Dictionary<string, int>();
        }
    }
}

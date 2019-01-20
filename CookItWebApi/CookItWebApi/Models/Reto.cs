using CookItWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CookItWebApi.Models
{
    public class Reto
    {
        [Required]
        public string _EmailUsuOri { get; set; }
        [JsonIgnore]
        public Perfil _PerfilUsuOri { get; set; }
        [Required]
        public string _EmialUsuDes { get; set; }
        [JsonIgnore]
        public Perfil _PerfilUsuDes { get; set; }
        [Required]
        public int _RecetaId { get; set; }
        [JsonIgnore]
        public Receta _Receta { get; set; }
        public bool _Cumplido { get; set; }
        [Required]
        public DateTime _Fecha { get; set; }
        [Required]
        public int _IdEstadoReto { get; set; }
        [JsonIgnore]
        public EstadoReto _EstadoReto { get; set; }
        public byte[] _Presentacion { get; set; }

        public int _Puntaje { get; set; }
        [Required]
        public string _ComentarioOrigen { get; set; }
        public string _ComentarioDestino { get; set; }


        public Reto() { }

        public NotificacionAppCenter NotificarReto(Guid? DeviceId)
        {
            
            NotificacionAppCenter notificacion = new NotificacionAppCenter();          

            switch (_IdEstadoReto)
            {
                case 1:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuOri._NombreUsuario + " te ha retado!!!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuDes._NombreUsuario + ". " + _PerfilUsuOri._NombreUsuario 
                        + "te ha retado a preparar está exquicita receta: '" + _Receta._Titulo 
                        + "'. Aprenda hacerla y acumulas " + _Puntaje + " puntos para subir de nivel.";
                    notificacion.Notificacion_content.Notification_target.Devices.Add(DeviceId);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
            }

            return notificacion;

        }

    }
}

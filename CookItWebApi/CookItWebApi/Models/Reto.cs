using CookItWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;

namespace CookItWebApi.Models
{
    public class Reto
    {        
        [Required]
        public int _IdReto { get; set; }
        [Required]
        public string _EmailUsuOri { get; set; }
        [Required]
        public string _NomUsuOri { get; set; }
        [JsonIgnore]
        public Perfil _PerfilUsuOri { get; set; }
        [Required]
        public string _EmailUsuDes { get; set; }
        [Required]
        public string _NomUsuDes { get; set; }
        [JsonIgnore]
        public Perfil _PerfilUsuDes { get; set; }
        [Required]
        public int _RecetaId { get; set; }
        [JsonIgnore]
        public Receta _Receta { get; set; }
        [Required]
        public int _IdEstadoReto { get; set; }        
        [Required]
        public DateTime _Fecha { get; set; }
        [JsonIgnore]
        public EstadoReto _EstadoReto { get; set; }
        public byte[] _Presentacion { get; set; }

        public int _Puntaje { get; set; }
        [Required]
        public string _ComentarioOrigen { get; set; }
        public string _ComentarioDestino { get; set; }


        public Reto() { }

        public NotificacionAppCenter NotificarReto(Guid? DeviceIdOri, Guid? DeviceIdDes)
        {
            
            NotificacionAppCenter notificacion = new NotificacionAppCenter();          

            switch (_IdEstadoReto)
            {
                case 1:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuOri._NombreUsuario + " te ha desafiado!!!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuDes._NombreUsuario + ".\\n" + _PerfilUsuOri._NombreUsuario
                        + " te ha retado a preparar está exquicita receta: '" + _Receta._Titulo
                        + "'.\\nAprenda hacerla y acumulá " + _Puntaje + " puntos para subir de nivel.";
                    notificacion.Notification_target.Devices.Add(DeviceIdDes);
                    notificacion.Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 2:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuDes._NombreUsuario + " ha aceptado el desafío!!!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuOri._NombreUsuario + ".\\nAcepto el desafío propuesto!!.";
                    notificacion.Notification_target.Devices.Add(DeviceIdOri);
                    notificacion.Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 3:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuDes._NombreUsuario + " ha pasado!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuOri._NombreUsuario + ".\\nLo siento pero por está paso.";
                    notificacion.Notification_target.Devices.Add(DeviceIdOri);
                    notificacion.Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 4:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuDes._NombreUsuario + " ha terminado!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuOri._NombreUsuario + ".\\nHe finalizado el desafío propuesto!!";
                    notificacion.Notification_target.Devices.Add(DeviceIdOri);
                    notificacion.Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 5:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuDes._NombreUsuario + " ha terminado!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuDes._NombreUsuario + ".\\nFelicitaciones, desafío cumplido!!";
                    notificacion.Notification_target.Devices.Add(DeviceIdDes);
                    notificacion.Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
                case 6:
                    notificacion.Notificacion_content.Title = "CookIt!: " + _PerfilUsuDes._NombreUsuario + " ha terminado!";
                    notificacion.Notificacion_content.Body = "Hola " + _PerfilUsuDes._NombreUsuario + ".\\nLo sento pero no has logrado cumplir el desafío..";
                    notificacion.Notification_target.Devices.Add(DeviceIdDes);
                    notificacion.Notificacion_content.Custom_Data.Add("Reto", "Reto");
                    break;
            }

            notificacion.Notificacion_content.Body = (notificacion.Notificacion_content.Body as string).Replace("\\n", Environment.NewLine + Environment.NewLine);

            return notificacion;

        }

    }
}

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
        public string _EmailUsuarioOrigen { get; set; }
        [Required]
        public string _EmialUsuarioDestino { get; set; }
        [JsonIgnore]
        public virtual Usuario _Usuario { get; set; }
        [Required]
        public int _RecetaId { get; set; }
        public bool _Cumplido { get; set; }
        [Required]
        public DateTime _Fecha { get; set; }
        [Required]
        public int _IdEstado { get; set; }
        [JsonIgnore]
        public EstadoReto _Estado { get; set; }
        public byte[] _Presentacio { get; set; }

        public int _Puntaje { get; set; }
        [Required]
        public string _ComentarioOrigen { get; set; }
        public string _ComentarioDestino { get; set; }

        public Reto(string EmailUsuarioOrigen, string EmialUsuarioDestino, DateTime Fecha, int IdEstado, 
            EstadoReto Estado, byte[] Presentacio, int Puntaje, int RecetaId, string ComentarioOrigen, string ComentarioDestino)
        {
            
            _EmailUsuarioOrigen = EmailUsuarioOrigen;
            _EmialUsuarioDestino = EmialUsuarioDestino;
            _Fecha = Fecha;
            _IdEstado = IdEstado;
            _Estado = Estado;
            _Presentacio = Presentacio;
            _Puntaje = Puntaje;
            _RecetaId = RecetaId;
            _ComentarioOrigen = ComentarioOrigen;
            _ComentarioDestino = ComentarioDestino;
        }

        public Reto() { }
    }
}

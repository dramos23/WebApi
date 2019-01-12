using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CookItWebApi.Models
{
    //[Table("PasosRecetas")]
    public class PasoReceta
    {

        public int _IdReceta { set; get; }

        [JsonIgnore]
        public Receta _Receta { set; get; }
        [Required]
        public int _IdPasoReceta { set; get; }


        [Required]
        public string _Descripcion { set; get; }
        public int _TiempoReloj { set; get; }
        public string _UrlVideo { set; get; }
        public byte[] _Foto { set; get; }


    }
}

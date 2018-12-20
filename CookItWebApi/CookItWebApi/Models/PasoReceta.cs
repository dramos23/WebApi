﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CookItWebApi.Models
{
    [Table("PasosRecetas")]
    public class PasoReceta
    {

        [Key]
        [Column(Order = 1)]
        public int _IdReceta { set; get; }
        [ForeignKey("_IdReceta")]
        [JsonIgnore]
        public Receta _Receta { set; get; }

        [Key]
        [Column(Order = 2)]
        public int _IdPasoReceta { set; get; }


        [Required]
        [StringLength(200)]
        public string _Texto { set; get; }
        public int _TiempoReloj { set; get; }
        public string _UrlVideo { set; get; }
        public byte[] imagen;


    }
}

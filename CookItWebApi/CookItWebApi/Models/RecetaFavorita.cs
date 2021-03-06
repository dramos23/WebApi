﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    
    public class RecetaFavorita
    {

        public string _Email { get; set; }
        [JsonIgnore]
        public virtual Perfil _Perfil { get; set; }
        public int _IdReceta { set; get; }
        public virtual Receta _Receta { set; get; }
        public DateTime _FechaHora { get; set; }

    }
}

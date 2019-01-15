using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Retos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RetoController : ControllerBase
    {

        private readonly ApplicationDbContext _Context;

        public RetoController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{emailOrigen},{emailDestino},{recetaId},{cumplido}", Name = "RetoCreado")]
        public IActionResult GetbyId(string emailOrigen,string emailDestino, int recetaId, bool cumplido)
        {

            var _Reto = _Context.Retos.FirstOrDefault(x => x._EmailUsuarioOrigen == emailOrigen && x._EmialUsuarioDestino == emailDestino && x._RecetaId == recetaId && x._Cumplido == cumplido);

            if (_Reto == null)
            {

                return Ok(null);
            }

            return Ok(_Reto);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Reto reto)
        {

            if (ModelState.IsValid)
            {

                _Context.Retos.Add(reto);
                _Context.SaveChanges();
                return new CreatedAtRouteResult("PerfilCreado", new { emailOrigen = reto._EmailUsuarioOrigen, emailDestino = reto._EmialUsuarioDestino, recetaId = reto._RecetaId, cumplido = reto._Cumplido },reto);

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{email}")]
        public IActionResult Put([FromBody] Reto reto, string emailOrigen, string emailDestino, int recetaId, bool cumplido)
        {
            
            if (emailOrigen != reto._EmailUsuarioOrigen || emailDestino != reto._EmialUsuarioDestino || recetaId != reto._RecetaId || cumplido != reto._Cumplido)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(reto).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok(reto);

        }
    }
}
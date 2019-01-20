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
    [Route("api/Perfiles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PerfilesController : Controller
    {

        private readonly ApplicationDbContext _Context;

        public PerfilesController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        [Route("ListEmailNombres")]
        public IEnumerable<Perfil> Get()
        {
            return _Context.Perfiles.Select(a => new Perfil { _Email = a._Email, _NombreUsuario = a._NombreUsuario }).ToList();
        }

        [HttpGet("{email}", Name = "PerfilCreado")]
        public IActionResult GetbyId(string email)
        {

            Perfil _Perfil = _Context.Perfiles
                .Include(p => p._ListaIngredientesUsuario)
                .Include(p => p._ListaNotificaciones)
                .Include(p => p._ListaRecetasFavoritas)
                .Include(p => p._ListaRetos)
                .FirstOrDefault(x => x._Email == email);

            //List<IngredienteUsuario> ingredientesUsuario = _Context.IngredienteUsuarios.Where(x => x._Email == email).Include(x => x._Ingrediente).ToList();
            //_Perfil._ListaIngredientesUsuario = ingredientesUsuario;

            if (_Perfil == null)
            {

                return Ok(null);
            }

            return Ok(_Perfil);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Perfil _Perfil)
        {

            if (ModelState.IsValid)
            {

                _Context.Perfiles.Add(_Perfil);
                _Context.SaveChanges();
                return new CreatedAtRouteResult("PerfilCreado", new { email = _Perfil._Email }, _Perfil);

            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Perfil _Perfil)
        {

            if (_Perfil._Email == null)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_Perfil).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok(_Perfil);

        }

    }
}
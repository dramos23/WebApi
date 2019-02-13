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

            Perfil _Perfil = _Context.Perfiles.FirstOrDefault(x => x._Email == email);

            List<IngredienteUsuario> ingredientesUsuario = _Context.IngredienteUsuarios.Where(iu => iu._Email == email).Include(i => i._Ingrediente) .ToList();
            List<Reto> retos = _Context.Retos.Where(r => r._EmailUsuOri == email || r._EmailUsuDes == email).ToList();
            List<Notificacion> notificaciones = _Context.Notificaciones.Where(n => n._Email == email).ToList();
            List<RecetaFavorita> recetaFavoritas = _Context.RecetasFavoritas.Where(rf => rf._Email == email).ToList();

            _Perfil._ListaIngredientesUsuario = ingredientesUsuario;
            _Perfil._ListaRetos = retos;
            _Perfil._ListaNotificaciones = notificaciones;
            _Perfil._ListaRecetasFavoritas = recetaFavoritas;

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

        [HttpGet]
        [Route("Gamificacion/{email}")]
        public IActionResult GetGamificacion([FromRoute] string email)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            Perfil perfil = _Context.Perfiles.Where(p => p._Email == email).Select(p => new Perfil { _Email = p._Email, _Categoria = p._Categoria, _Puntuacion = p._Puntuacion }).Single();

            int cat = Convert.ToInt32(perfil._Categoria);

            dic.Add("Email", perfil._Email);
            dic.Add("Categoria", Convert.ToString(cat));
            dic.Add("Puntuacion", perfil._Puntuacion.ToString());

            return Ok(dic);
            
        }

    }
}
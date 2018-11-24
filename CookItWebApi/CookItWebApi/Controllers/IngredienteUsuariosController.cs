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
    [Route("api/IngredientesUsuario")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IngredienteUsuariosController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public IngredienteUsuariosController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{email}")]
        public IEnumerable<IngredienteUsuario> GetbyEmail(string _Email)
        {

            return _Context.IngredienteUsuarios.Where(x => x._Email == _Email).ToList();

        }

        [HttpGet("{email},{id}", Name = "IngredienteUsuarioCreado")]
        public IActionResult GetbyId(string _Email, int _IdIngrediente)
        {

            var _IngredienteUsuario = _Context.IngredienteUsuarios
                    .Where(x => x._Email == _Email)
                    .Where(x => x._IdIngrediente == _IdIngrediente);

            if (_IngredienteUsuario == null)
            {

                return NotFound();
            }

            return new ObjectResult(_IngredienteUsuario);

        }

        [HttpPost]
        public IActionResult Post([FromBody] IngredienteUsuario _IngredienteUsuario)
        {

            if (ModelState.IsValid)
            {

                IngredienteUsuario aux = _Context.IngredienteUsuarios.Find(_IngredienteUsuario);
                if (aux != null)
                {
                    return new CreatedAtRouteResult("ActualizarIngredienteUsuario", new { email = _IngredienteUsuario._Email, id = _IngredienteUsuario._IdIngrediente }, _IngredienteUsuario);
                }
                else { 

                    _Context.IngredienteUsuarios.Add(_IngredienteUsuario);
                    _Context.SaveChanges();
                    return new CreatedAtRouteResult("IngredienteUsuarioCreado", new {email = _IngredienteUsuario._Email, id = _IngredienteUsuario._IdIngrediente}, _IngredienteUsuario);
                }

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{email},{id}", Name = "ActualizarIngredienteUsuario")]
        public IActionResult Put([FromBody] IngredienteUsuario _IngredienteUsuario, string _Email, int _IdIngrediente)
        {

            if ((_IngredienteUsuario._Email != _Email) || (_IngredienteUsuario._IdIngrediente != _IdIngrediente))
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_IngredienteUsuario).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{email},{id}")]
        public IActionResult Delete(string _Email, int _IdIngrediente)
        {

            var _IngredienteUsuario = (IngredienteUsuario) _Context.IngredienteUsuarios
                                                                .Where(x => x._Email == _Email)
                                                                .Where(x => x._IdIngrediente == _IdIngrediente);

            if (_IngredienteUsuario._Email != _Email || _IngredienteUsuario._IdIngrediente != _IdIngrediente)
            {

                return NotFound();

            }

            _Context.IngredienteUsuarios.Remove(_IngredienteUsuario);
            _Context.SaveChanges();
            return Ok(_IngredienteUsuario);

        }
    }
}
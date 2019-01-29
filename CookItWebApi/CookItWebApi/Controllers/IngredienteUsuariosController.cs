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
        public IEnumerable<IngredienteUsuario> GetbyEmail(string email)
        {
            List<IngredienteUsuario> ingredienteUsuarios = _Context.IngredienteUsuarios.Where(x => x._Email == email).ToList();
            return ingredienteUsuarios;

        }

        [HttpGet("{email},{id}", Name = "IngredienteUsuarioCreado")]
        public IActionResult GetbyId(string _Email, int _IdIngrediente)
        {

            IngredienteUsuario ingredienteUsuario = _Context.IngredienteUsuarios.FirstOrDefault(iu => iu._Email == _Email && iu._IdIngrediente == _IdIngrediente);

            if (ingredienteUsuario == null)
            {

                return NotFound();
            }

            return new ObjectResult(ingredienteUsuario);

        }

        [HttpPost]
        public IActionResult Post([FromBody] IngredienteUsuario IngredienteUsuario)
        {

            if (ModelState.IsValid)
            {

                IngredienteUsuario ingredienteUsuario = _Context.IngredienteUsuarios.FirstOrDefault(iu => iu._Email == IngredienteUsuario._Email && iu._IdIngrediente == IngredienteUsuario._IdIngrediente);
                if (ingredienteUsuario != null)
                {
                    ingredienteUsuario._Cantidad += IngredienteUsuario._Cantidad;
                    _Context.Entry(ingredienteUsuario).State = EntityState.Modified;
                    _Context.SaveChanges();
                    return new CreatedAtRouteResult("IngredienteUsuarioCreado", new { email = ingredienteUsuario._Email, id = ingredienteUsuario._IdIngrediente }, ingredienteUsuario);
                }
                else { 

                    _Context.IngredienteUsuarios.Add(IngredienteUsuario);
                    _Context.SaveChanges();
                    return new CreatedAtRouteResult("IngredienteUsuarioCreado", new {email = IngredienteUsuario._Email, id = IngredienteUsuario._IdIngrediente}, IngredienteUsuario);
                }

            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{email},{id}")]
        public IActionResult Delete(string Email, int Id)
        {

            IngredienteUsuario ingredienteUsuario = _Context.IngredienteUsuarios.FirstOrDefault(iu => iu._Email == Email && iu._IdIngrediente == Id);

            if (ingredienteUsuario == null)
            {

                return NotFound();

            }

            _Context.IngredienteUsuarios.Remove(ingredienteUsuario);
            _Context.SaveChanges();
            return Ok(ingredienteUsuario);

        }
    }
}
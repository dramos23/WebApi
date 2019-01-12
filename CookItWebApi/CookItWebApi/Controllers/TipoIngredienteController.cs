using System.Collections.Generic;
using System.Linq;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/TipoIngrediente")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoIngredienteController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public TipoIngredienteController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        public IEnumerable<TipoIngrediente> Get()
        {

            return _Context.TiposIngredientes.ToList();

        }

        [HttpGet("{id}", Name = "TipoIngredienteCreado")]
        public IActionResult GetbyId(int id)
        {

            var tipoIngrediente = _Context.TiposIngredientes.FirstOrDefault(x => x._IdTipoIngrediente == id);

            if (tipoIngrediente == null)
            {

                return NotFound();
            }

            return Ok(tipoIngrediente);

        }

        [HttpPost]
        public IActionResult Post([FromBody] TipoIngrediente tipoIngrediente)
        {

            if (ModelState.IsValid)
            {

                _Context.TiposIngredientes.Add(tipoIngrediente);
                _Context.SaveChanges();
                return new CreatedAtRouteResult("TipoIngredienteCreado", new { id = tipoIngrediente._IdTipoIngrediente }, tipoIngrediente);

            }

            return BadRequest(ModelState);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookItWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Ingredientes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IngredientesController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public IngredientesController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        public IEnumerable<Ingrediente> Get()
        {

            return _Context.Ingredientes.ToList();

        }

        [HttpGet("{id}", Name = "IngredienteCreado")]
        public IActionResult GetbyId(int id) {

            var Ingrediente = _Context.Ingredientes.FirstOrDefault(x => x._Id == id);

            if (Ingrediente == null) {

                return NotFound();
            }

            return Ok(Ingrediente);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Ingrediente ingrediente) {

            if (ModelState.IsValid) {

                _Context.Ingredientes.Add(ingrediente);
                _Context.SaveChanges();
                return new CreatedAtRouteResult("IngredienteCreado", new { id = ingrediente._Id }, ingrediente);

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Ingrediente ingrediente, int id) {

            if (ingrediente._Id != id)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(ingrediente).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {

            var ingrediente = _Context.Ingredientes.FirstOrDefault(x => x._Id == id);

            if (ingrediente._Id != id)
            {

                return NotFound();

            }

            _Context.Ingredientes.Remove(ingrediente);
            _Context.SaveChanges();
            return Ok(ingrediente);

        }
    }

}
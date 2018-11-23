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
    [Route("api/Recetas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecetasController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public RecetasController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        public IEnumerable<Receta> Get()
        {

            return _Context.Recetas.ToList();

        }

        [HttpGet("{id}", Name = "RecetaCreada")]
        public IActionResult GetbyId(int id)
        {

            var _Receta = _Context.Recetas
                .Where(x => x._IdReceta == id)
                .Include(x => x._Pasos)
                .Include(x => x._Ingredientes);

            if (_Receta == null)
            {

                return NotFound();
            }

            return Ok(_Receta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Receta _Receta)
        {

            if (ModelState.IsValid)
            {

                _Context.Recetas.Add(_Receta);
                _Context.SaveChanges();
                return new CreatedAtRouteResult("RecetaCreada", new { id = _Receta._IdReceta }, _Receta);

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Receta _Receta, int id)
        {

            if (_Receta._IdReceta != id)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_Receta).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var _Receta = _Context.Recetas.FirstOrDefault(x => x._IdReceta == id);

            if (_Receta._IdReceta != id)
            {

                return NotFound();

            }

            _Context.Recetas.Remove(_Receta);
            _Context.SaveChanges();
            return Ok(_Receta);

        }
    }
}
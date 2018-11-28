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
    [Route("api/Recetas/{RecetaId}/PasoRecetas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PasoRecetasController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public PasoRecetasController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        public IEnumerable<PasoReceta> GetAll(int RecetaId)
        {

            return _Context.PasoRecetas.Where(x => x._IdReceta == RecetaId).ToList();

        }

        [HttpGet("{id}", Name = "PasoRecetaCreado")]
        public IActionResult GetbyId(int id)
        {

            var _PasoReceta = _Context.PasoRecetas.FirstOrDefault(x => x._IdReceta == id);

            if (_PasoReceta == null)
            {

                return NotFound();
            }

            return Ok(_PasoReceta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] PasoReceta _PasoReceta, int RecetaId)
        {
            var _Receta = _Context.Recetas.FirstOrDefault(x => x._IdReceta == RecetaId);

            if (_Receta._IdReceta != RecetaId)
            {

                return NotFound();

            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _Context.PasoRecetas.Add(_PasoReceta);
            _Context.SaveChanges();

            return new CreatedAtRouteResult("PasoRecetaCreado", new { id = _PasoReceta._IdPasoReceta }, _PasoReceta);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PasoReceta _PasoReceta, int id)
        {

            if (_PasoReceta._IdPasoReceta != id)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_PasoReceta).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var _PasoReceta = _Context.PasoRecetas.FirstOrDefault(x => x._IdPasoReceta == id);

            if (_PasoReceta._IdPasoReceta != id)
            {

                return NotFound();

            }

            _Context.PasoRecetas.Remove(_PasoReceta);
            _Context.SaveChanges();
            return Ok(_PasoReceta);

        }
    }
}
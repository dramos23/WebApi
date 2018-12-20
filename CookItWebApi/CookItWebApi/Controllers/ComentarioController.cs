using CookItWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Recetas/{RecetaId}/Comentario")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComentarioController : Controller
    {

        private readonly ApplicationDbContext _Context;

        public ComentarioController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        public IEnumerable<ComentarioReceta> GetAll(int RecetaId)
        {

            return _Context.ComentarioRecetas.Where(x => x._IdReceta == RecetaId).ToList();

        }

        [HttpGet("{id}", Name = "ComentarioRecetaCreado")]
        public IActionResult GetbyId(int id, int RecetaId)
        {

            var _ComentarioReceta = _Context.ComentarioRecetas.FirstOrDefault(x => x._IdReceta == RecetaId && x._IdComentario == id);

            if (_ComentarioReceta == null)
            {

                return NotFound();
            }

            return Ok(_ComentarioReceta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] ComentarioReceta _ComentarioReceta, int RecetaId)
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

            _Context.ComentarioRecetas.Add(_ComentarioReceta);
            _Context.SaveChanges();

            return new CreatedAtRouteResult("ComentarioRecetaCreado", new { id = _ComentarioReceta._IdComentario }, _ComentarioReceta);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ComentarioReceta _ComentarioReceta, int id, int RecetaId)
        {

            if (_ComentarioReceta._IdReceta != RecetaId || _ComentarioReceta._IdComentario != id)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_ComentarioReceta).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int RecetaId, int id)
        {

            var _ComentarioReceta = _Context.ComentarioRecetas.FirstOrDefault(x => x._IdReceta == RecetaId && x._IdComentario == id);

            if (_ComentarioReceta._IdReceta != RecetaId || _ComentarioReceta._IdComentario != id)
            {

                return NotFound();

            }

            _Context.ComentarioRecetas.Remove(_ComentarioReceta);
            _Context.SaveChanges();
            return Ok(_ComentarioReceta);

        }

    }
}

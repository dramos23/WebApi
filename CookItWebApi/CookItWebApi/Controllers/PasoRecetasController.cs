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
        public IEnumerable<PasoReceta> GetAll(int IdReceta)
        {

            return _Context.PasoRecetas.Where(x => x._IdReceta == IdReceta).ToList();

        }

        [HttpGet("{id}", Name = "PasoRecetaCreado")]
        public IActionResult GetbyId(int IdReceta)
        {

            var _PasoReceta = _Context.PasoRecetas.FirstOrDefault(x => x._IdReceta == IdReceta);

            if (_PasoReceta == null)
            {

                return NotFound();
            }

            return Ok(_PasoReceta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] PasoReceta _PasoReceta, int _IdReceta)
        {
            _PasoReceta._IdReceta = _IdReceta;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _Context.PasoRecetas.Add(_PasoReceta);
            _Context.SaveChanges();

            return new CreatedAtRouteResult("PasoRecetaCreado", new { id = _PasoReceta._IdPasoReceta }, _PasoReceta);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PasoReceta _PasoReceta, int IdPasoReceta)
        {

            if (_PasoReceta._IdPasoReceta != IdPasoReceta)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_PasoReceta).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int IdPasoReceta)
        {

            var _PasoReceta = _Context.PasoRecetas.FirstOrDefault(x => x._IdPasoReceta == IdPasoReceta);

            if (_PasoReceta._IdPasoReceta != IdPasoReceta)
            {

                return NotFound();

            }

            _Context.PasoRecetas.Remove(_PasoReceta);
            _Context.SaveChanges();
            return Ok(_PasoReceta);

        }
    }
}
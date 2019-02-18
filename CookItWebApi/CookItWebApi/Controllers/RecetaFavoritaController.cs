using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/RecetaFavorita")]
    public class RecetaFavoritaController : Controller
    {

        private readonly ApplicationDbContext _Context;

        public RecetaFavoritaController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{idPerfil}")]
        public IEnumerable<RecetaFavorita> GetAll(string idPerfil)
        {

            var _RecetasFavoritas = _Context.RecetasFavoritas.Where(x => x._Email == idPerfil).ToList();

            foreach (RecetaFavorita x in _RecetasFavoritas)
            {

                var _Receta = _Context.Recetas.Where(r => r._IdReceta == x._IdReceta);

                foreach (Receta r in _Receta)
                {
                    x._Receta = r;
                }

            }

            return _RecetasFavoritas;

        }

        [HttpPost]
        public IActionResult Post([FromBody] RecetaFavorita _RecetaFavorita)
        {

            if (ModelState.IsValid)
            {
                try { 
                    _Context.RecetasFavoritas.Add(_RecetaFavorita);
                    _Context.SaveChanges();
                    return Ok(_RecetaFavorita);
                }
                catch(Exception ex) {
                    
                    return NotFound(ex.Message);
                }

            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{idPerfil},{RecetaId}")]
        public IActionResult Delete(string idPerfil, int RecetaId)
        {

            var _RecetaFavorita = _Context.RecetasFavoritas.FirstOrDefault(x => x._Email == idPerfil && x._IdReceta == RecetaId);

            if (_RecetaFavorita == null)
            {

                return NotFound();

            }

            _Context.RecetasFavoritas.Remove(_RecetaFavorita);
            _Context.SaveChanges();
            return Ok(_RecetaFavorita);

        }

    }
}
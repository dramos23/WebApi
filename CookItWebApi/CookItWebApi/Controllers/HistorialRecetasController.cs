using System.Collections.Generic;
using System.Linq;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/HistorialReceta")]
    public class HistorialRecetasController : Controller
    {

        private readonly ApplicationDbContext _Context;

        public HistorialRecetasController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{EmailUsu}")]
        public IEnumerable<HistorialReceta> GetAll(string EmailUsu)
        {

            var _HistorialReceta = _Context.HistorialRecetas.Where(x => x._Email == EmailUsu);

            foreach (HistorialReceta x in _HistorialReceta)
            {

                var _Receta = _Context.Recetas.Where(r => r._IdReceta == x._IdReceta);

                foreach (Receta r in _Receta)
                {
                    x._Receta = r;
                }

            }

            return _HistorialReceta;

        }

        [HttpGet("{EmailUsu},{Id}", Name = "HistorialRecetaCreado")]
        public IActionResult GetbyId(string EmailUsu, int Id)
        {

            var _HistorialReceta = _Context.HistorialRecetas.Where(x => x._Email == EmailUsu && x._IdHistorialReceta == Id);
            

            foreach (HistorialReceta x in _HistorialReceta) {

                var _Receta = _Context.Recetas.Where(r => r._IdReceta == x._IdReceta);

                foreach (Receta r in _Receta)
                {
                    x._Receta = r;
                }

            }

            if (_HistorialReceta == null)
            {

                return NotFound();
            }

            return Ok(_HistorialReceta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] HistorialReceta _HistorialReceta)
        {            

            if (ModelState.IsValid)
            {                
                _Context.HistorialRecetas.Add(_HistorialReceta);
                _Context.SaveChanges();
                return Ok(_HistorialReceta);

            }

            return BadRequest(ModelState);
        }

    }
}
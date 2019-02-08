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
    [Route("api/Recetas/{RecetaId}/IngredienteRecetas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IngredienteRecetasController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public IngredienteRecetasController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        [Route("ObtenerIngRecetaID")] //ObtenerIngReceta
        public ICollection<IngredienteReceta> GetAll(int RecetaId)
        {

            return _Context.IngredienteRecetas.Where(x => x._Receta._IdReceta == RecetaId)
                                              .Include(x => x._Ingrediente).ToList();

        }

        [HttpGet("{id}", Name = "IngredienteRecetaCreado")]
        public IActionResult GetbyId(int id)
        {

            var _IngredienteReceta = _Context.IngredienteRecetas.FirstOrDefault(x => x._IdReceta == id);            

            if (_IngredienteReceta == null)
            {

                return NotFound();
            }

            return new ObjectResult(_IngredienteReceta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] IngredienteReceta _IngredienteReceta, int RecetaId)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _Context.IngredienteRecetas.Add(_IngredienteReceta);
            _Context.SaveChanges();

            return new CreatedAtRouteResult("IngredienteRecetaCreado", new { id = _IngredienteReceta._IdIngrediente }, _IngredienteReceta);
        }

        [HttpPost]
        [Route("AltaListaIngRec")]
        public IActionResult AltaListaIngRec([FromBody] List<IngredienteReceta> _IngredientesReceta, int RecetaId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (IngredienteReceta ir in _IngredientesReceta) {
                _Context.IngredienteRecetas.Add(ir);                
            }
            _Context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] IngredienteReceta _IngredienteReceta, int id)
        {

            if (_IngredienteReceta._Receta._IdReceta != id)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_IngredienteReceta).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var _IngredienteReceta = _Context.IngredienteRecetas.FirstOrDefault(x => x._IdReceta == id);

            if (_IngredienteReceta._IdReceta != id)
            {

                return NotFound();

            }

            _Context.IngredienteRecetas.Remove(_IngredienteReceta);
            _Context.SaveChanges();
            return Ok(_IngredienteReceta);

        }
    }
}
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

        [HttpGet("{email}")]
        public IEnumerable<Receta> Get(string email)
        {

            List<Receta> recetas = _Context.Recetas.Where(r => r._Habilitada == true || r._Email == email) .ToList();
            foreach (Receta receta in recetas) {

                List<IngredienteReceta> ingredientesReceta = _Context.IngredienteRecetas.Where(x => x._IdReceta == receta._IdReceta).Include(x => x._Ingrediente).ToList();
                List<PasoReceta> pasoRecetas = _Context.PasoRecetas.Where(x => x._IdReceta == receta._IdReceta).ToList();
                List<ComentarioReceta> comentarioRecetas = _Context.ComentarioRecetas.Where(x => x._IdReceta == receta._IdReceta).ToList();

                receta._ListaIngredientesReceta = ingredientesReceta;
                receta._ListaPasosReceta = pasoRecetas;
                receta._ListaComentariosReceta = comentarioRecetas;

            }

            return recetas;

        }

        [HttpGet("{id}", Name = "RecetaCreada")]
        public IActionResult GetbyId(int id)
        {


            Receta receta = _Context.Recetas.FirstOrDefault(r => r._IdReceta == id);
                

            List<IngredienteReceta> ingredientesReceta = _Context.IngredienteRecetas.Where(x => x._IdReceta == id).Include(x => x._Ingrediente).ToList();
            List<PasoReceta> pasoRecetas = _Context.PasoRecetas.Where(x => x._IdReceta == id).ToList();
            List<ComentarioReceta> comentarioRecetas = _Context.ComentarioRecetas.Where(x => x._IdReceta == id).ToList();

            receta._ListaIngredientesReceta = ingredientesReceta;
            receta._ListaPasosReceta = pasoRecetas;
            receta._ListaComentariosReceta = comentarioRecetas;

            if (receta == null)
            {

                return NotFound();
            }

            return Ok(receta);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Receta Receta)
        {

            if (ModelState.IsValid)
            {
                _Context.Recetas.Add(Receta);
                _Context.SaveChanges();
                return Ok(Receta._IdReceta);
                
            }
            
            return BadRequest(-1);

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

            if (_Receta == null)
            {

                return NotFound();

            }

            _Context.Recetas.Remove(_Receta);
            _Context.SaveChanges();
            return Ok(_Receta);

        }
    }
}
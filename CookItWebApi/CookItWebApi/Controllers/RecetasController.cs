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


            Receta receta = _Context.Recetas.Include(r => r._IngredientesReceta).Include(r => r._ComentariosReceta).Include(r => r._Pasos).FirstOrDefault(r => r._IdReceta == id);
            List<IngredienteReceta> ingredientesReceta = _Context.IngredienteRecetas.Where(x => x._IdReceta == id).Include(x => x._Ingrediente).ToList();
            receta._IngredientesReceta = ingredientesReceta;

            if (receta == null)
            {

                return NotFound();
            }

            return Ok(receta);

        }

        [HttpPost]
        [Route("RecetasFiltro")]
        public IActionResult ObtenerRecetasFiltro([FromBody] Dictionary<string, string> obj)
        {

            var _Recetas = _Context.Recetas.ToList();
            _Recetas = (obj.ContainsKey("AptoCeliaco")) ? _Recetas.FindAll(x => x._AptoCeliacos == Boolean.Parse(obj["AptoCeliaco"])) : _Recetas;
            _Recetas = (obj.ContainsKey("AptoDiabetico")) ? _Recetas.FindAll(x => x._AptoDiabeticos == Boolean.Parse(obj["AptoDiabetico"])) : _Recetas;
            _Recetas = (obj.ContainsKey("AptoVegano")) ? _Recetas.FindAll(x => x._AptoVeganos == Boolean.Parse(obj["AptoVegano"])) : _Recetas;
            _Recetas = (obj.ContainsKey("AptoVegetariano")) ? _Recetas.FindAll(x => x._AptoVegetarianos == Boolean.Parse(obj["AptoVegetariano"])) : _Recetas;
            _Recetas = (obj.ContainsKey("MomentoDia")) ? _Recetas.FindAll(x => x._MomentoDia._IdMomentoDia == Convert.ToInt64(obj["MomentoDia"])) : _Recetas;
            _Recetas = (obj.ContainsKey("PuntajeTotal")) ? _Recetas.FindAll(x => x._PuntajeTotal == Convert.ToDouble(obj["PuntajeTotal"])) : _Recetas;
            _Recetas = (obj.ContainsKey("Estacion")) ? _Recetas.FindAll(x => x._Estacion._IdEstacion == Convert.ToInt64(obj["Estacion"])) : _Recetas;
            _Recetas = (obj.ContainsKey("Dificultad")) ? _Recetas.FindAll(x => x._Dificultad == Convert.ToInt64(obj["Dificultad"])) : _Recetas;
            _Recetas = (obj.ContainsKey("CostoMayorIgual")) ? _Recetas.FindAll(x => x._Costo >= Convert.ToInt64(obj["CostoMayorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("CostoMenorIgual")) ? _Recetas.FindAll(x => x._Costo <= Convert.ToInt64(obj["CostoMenorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("PuntajeMayorIgual")) ? _Recetas.FindAll(x => x._Costo >= Convert.ToInt64(obj["PuntajeMayorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("PuntajeMenorIgual")) ? _Recetas.FindAll(x => x._Costo <= Convert.ToInt64(obj["PuntajeMenorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("CaloriasMayorIgual")) ? _Recetas.FindAll(x => x._CantCalorias >= Convert.ToInt64(obj["CaloriasMayorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("CaloriasMenorIgual")) ? _Recetas.FindAll(x => x._CantCalorias <= Convert.ToInt64(obj["CaloriasMenorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("CantPlatosMayorIgual")) ? _Recetas.FindAll(x => x._CantPlatos >= Convert.ToInt64(obj["CantPlatosMayorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("CantPlatosMenorIgual")) ? _Recetas.FindAll(x => x._CantPlatos <= Convert.ToInt64(obj["CantPlatosMenorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("TiempoPreparacionMayorIgual")) ? _Recetas.FindAll(x => x._TiempoPreparacion >= Convert.ToInt64(obj["TiempoPreparacionMayorIgual"])) : _Recetas;
            _Recetas = (obj.ContainsKey("TiempoPreparacionMenorIgual")) ? _Recetas.FindAll(x => x._TiempoPreparacion <= Convert.ToInt64(obj["TiempoPreparacionMenorIgual"])) : _Recetas;


            if (_Recetas == null)
            {

                return NotFound();
            }

            return Ok(_Recetas);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Receta _Receta)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);               
            }

            try
            {
                _Context.Recetas.Add(_Receta);
                var ret =_Context.SaveChanges();
                return Created("Ingresado",_Receta);
                
            }
            catch {
                return NotFound("Error");
            }
            
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
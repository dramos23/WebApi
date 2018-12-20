using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/HistorialReceta")]
    public class HistorialRecetaController : Controller
    {

        private readonly ApplicationDbContext _Context;

        public HistorialRecetaController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{EmailUsu}")]
        public IEnumerable<HistorialReceta> GetAll(string EmailUsu)
        {

            var _HistorialReceta = _Context.HistorialRecetas.Where(x => x._EmailUsuario == EmailUsu);

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

            var _HistorialReceta = _Context.HistorialRecetas.Where(x => x._EmailUsuario == EmailUsu && x._IdHistorialReceta == Id);
            

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

        //[HttpPut("{EmailUsu},{Id}")]
        //public IActionResult Put([FromBody] HistorialReceta _HistorialReceta, string EmailUsu, int Id)
        //{


        //    if (_HistorialReceta._EmailUsuario != EmailUsu || _HistorialReceta._IdHistorialReceta != Id)
        //    {

        //        return BadRequest(ModelState);

        //    }

        //    _Context.Entry(_HistorialReceta).State = EntityState.Modified;
        //    _Context.SaveChanges();
        //    return Ok();

        //}

        //[HttpDelete("{EmailUsu},{Id}")]
        //public IActionResult Delete(string EmailUsu, int Id)
        //{

        //    var _HistorialReceta = _Context.HistorialRecetas.FirstOrDefault(x => x._EmailUsuario == EmailUsu && x._IdHistorialReceta == Id);

        //    if (_HistorialReceta._EmailUsuario != EmailUsu || _HistorialReceta._IdHistorialReceta != Id)
        //    {

        //        return NotFound();

        //    }

        //    _Context.HistorialRecetas.Remove(_HistorialReceta);
        //    _Context.SaveChanges();
        //    return Ok(_HistorialReceta);

        //}

    }
}
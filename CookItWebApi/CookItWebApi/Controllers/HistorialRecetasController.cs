using System;
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

        [HttpGet("{Email}")]
        public IEnumerable<HistorialReceta> GetAll(string Email)
        {

            var _HistorialReceta = _Context.HistorialRecetas.Where(x => x._Email == Email).ToList();

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

        [HttpGet("{Email},{RecetaId},{FechaHora}", Name = "HistorialRecetaCreado")]
        public IActionResult GetbyId(string Email, int RecetaId, DateTime FechaHora)
        {

            HistorialReceta _HistorialReceta = _Context.HistorialRecetas.FirstOrDefault(x => x._Email == Email && x._IdReceta == RecetaId && x._FechaHora == FechaHora);


            if (_HistorialReceta != null)
            {

                Receta _Receta = _Context.Recetas.FirstOrDefault(r => r._IdReceta == _HistorialReceta._IdReceta);
                _HistorialReceta._Receta = _Receta;
                return Ok(_HistorialReceta);

            }

            return NotFound();
            

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
﻿using System;
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

        [HttpGet("{idPerfil}")]
        public IEnumerable<HistorialReceta> GetAll(string idPerfil)
        {

            var _HistorialReceta = _Context.HistorialRecetas.Where(x => x._IdPerfil == idPerfil).ToList();
            return _HistorialReceta;

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
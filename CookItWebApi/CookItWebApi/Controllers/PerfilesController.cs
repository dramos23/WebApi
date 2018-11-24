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
    [Route("api/Perfiles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PerfilesController : Controller
    {

        private readonly ApplicationDbContext _Context;

        public PerfilesController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{email}", Name = "PerfilCreado")]
        public IActionResult GetbyId(string email)
        {

            var _Perfil = _Context.Perfiles.Where(x => x._Email == email);

            if (_Perfil == null)
            {

                return NotFound();
            }

            return Ok(_Perfil);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Perfil _Perfil)
        {

            if (ModelState.IsValid)
            {

                _Context.Perfiles.Add(_Perfil);
                _Context.SaveChanges();
                return new CreatedAtRouteResult("PerfilCreado", new { email = _Perfil._Email }, _Perfil);

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{email}")]
        public IActionResult Put([FromBody] Perfil _Perfil, string email)
        {

            if (_Perfil._Email != email)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(_Perfil).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok();

        }

    }
}
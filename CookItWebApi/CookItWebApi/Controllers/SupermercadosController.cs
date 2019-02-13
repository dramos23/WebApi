using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookItWebApi.Controllers
{
    
    

    [Produces("application/json")]
    [Route("api/Supermercados")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SupermercadosController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public SupermercadosController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        // GET: api/Supermercados
        [HttpGet]
        public IEnumerable<Supermercado> Get()
        {
            return _Context.Supermercados.ToList();
        }

        // GET: api/Supermercados/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Supermercado supermercado = _Context.Supermercados.FirstOrDefault(s => s._IdSupermercado == id);
            return Ok(supermercado);
        }


    }
}

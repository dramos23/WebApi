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
    [Route("api/Estacion")]
    public class EstacionesController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public EstacionesController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet]
        public IEnumerable<Estacion> Get()
        {

            return _Context.Estaciones.ToList();
        }
    }
}
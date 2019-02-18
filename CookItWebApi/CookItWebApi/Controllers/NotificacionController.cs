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
    [Route("api/Notificacion")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificacionController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public NotificacionController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        //[HttpGet]
        //[Route("ObtenerNotificacion/{email}")]
        //public IActionResult GetActionResult(string email)
        //{
        //    var noti = _Context.Notificaciones.FirstOrDefault(n => n._Email == email && n._FechaHora == (_Context.Notificaciones.Where(m => m._Email == email).Max(m => m._FechaHora)));

        //    if (noti != null)
        //    {
        //        return Ok(noti);
        //    }

        //    return NotFound(); ;
        //}

        [HttpGet]
        [Route("ObtenerNotificacion/{id}")]
        public Notificacion GetbyId(int id)
        {
            return _Context.Notificaciones.FirstOrDefault(n => n._NotificacionId == id);            

        }

        [HttpPut]
        [Route("CambioEstado/{id}")]
        public IActionResult Put(int id)
        {
            var noti = _Context.Notificaciones.FirstOrDefault(n => n._NotificacionId == id);

            if (noti != null)
            {
                noti._Estado = Notificacion.Estado.Leido;
                _Context.Entry(noti).State = EntityState.Modified;
                _Context.SaveChanges();
                return Ok();
            }

            return NotFound();



        }

        [HttpPost]
        public IActionResult Post([FromBody] Notificacion notificacion) {

            if (ModelState.IsValid)
            {
                _Context.Notificaciones.Add(notificacion);
                _Context.SaveChanges();

                return Ok(notificacion._NotificacionId);                

            }

            return NotFound();


        }



    }
}
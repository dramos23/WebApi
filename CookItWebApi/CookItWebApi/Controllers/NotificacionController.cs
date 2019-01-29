﻿using System;
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
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        public NotificacionController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpPut]
        [Route("CambioEstado")]
        public IActionResult Put([FromBody] Notificacion  notificacion)
        {
            var noti = _Context.Notificaciones.FirstOrDefault(n => n._NotificacionId == notificacion._NotificacionId);            

            if (noti != null)
            {
                noti._Estado = notificacion._Estado;
                _Context.Entry(noti).State = EntityState.Modified;
                _Context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);

            

        }
    }
}
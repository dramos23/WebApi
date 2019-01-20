using System;
using System.Linq;
using CookItWebApi.Models;
using CookItWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Retos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RetoController : ControllerBase
    {

        private readonly ApplicationDbContext _Context;

        public RetoController(ApplicationDbContext context)
        {

            this._Context = context;

        }

        [HttpGet("{emailOrigen},{emailDestino},{recetaId},{cumplido}", Name = "RetoCreado")]
        public IActionResult GetbyId(string emailOrigen, string emailDestino, int recetaId, bool cumplido)
        {

            var _Reto = _Context.Retos.FirstOrDefault(x => x._EmailUsuOri == emailOrigen && x._EmialUsuDes == emailDestino && x._RecetaId == recetaId && x._Cumplido == cumplido);

            if (_Reto == null)
            {

                return Ok(null);
            }

            return Ok(_Reto);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Reto reto)
        {

            if (ModelState.IsValid)
            {
                Reto r = _Context.Retos.FirstOrDefault(x => x._EmailUsuOri == reto._EmailUsuOri && 
                x._EmialUsuDes == reto._EmialUsuDes && x._RecetaId == reto._RecetaId && x._Cumplido == reto._Cumplido);

                if (r == null)
                {
                    _Context.Retos.Add(reto);
                    _Context.SaveChanges();

                    GenerarNotificacion(reto);

                    return Ok(reto);
                }
                else {
                    return Conflict();
                }

            }

            return BadRequest(ModelState);
        }

        private async void GenerarNotificacion(Reto reto)
        {
            reto._PerfilUsuOri = _Context.Perfiles.Where(p => p._Email == reto._EmailUsuOri).FirstOrDefault();
            reto._PerfilUsuDes = _Context.Perfiles.Where(p => p._Email == reto._EmialUsuDes).FirstOrDefault();
            reto._Receta = _Context.Recetas.Where(r => r._IdReceta == reto._RecetaId).FirstOrDefault();

            Guid? DeviceId = _Context.Usuarios.FirstOrDefault(u => u._Email == reto._PerfilUsuDes._Email)._DeviceId;


            NotificacionAppCenter notificacionAppCenter = reto.NotificarReto(DeviceId);
            NotificacionService notificacionService = new NotificacionService();
            NotificacionAppCenter appCenter = await notificacionService.Alta(notificacionAppCenter);
            if (appCenter != null)
            {
                Notificacion notificacion = new Notificacion()
                {
                    _NotificacionId = appCenter.Notification_id,
                    _Email = reto._PerfilUsuDes._Email,
                    _Estado = Notificacion.Estado.SinLeer,
                    _FechaHora = DateTime.Now,
                    _Titulo = notificacionAppCenter.Notificacion_content.Title,
                    _Descripción = notificacionAppCenter.Notificacion_content.Body
                };

                _Context.Notificaciones.Add(notificacion);
                _Context.SaveChanges();
            }
        }



        [HttpPut("{email}")]
        public IActionResult Put([FromBody] Reto reto, string emailOrigen, string emailDestino, int recetaId, bool cumplido)
        {

            if (emailOrigen != reto._EmailUsuOri || emailDestino != reto._EmialUsuDes || recetaId != reto._RecetaId || cumplido != reto._Cumplido)
            {

                return BadRequest(ModelState);

            }

            _Context.Entry(reto).State = EntityState.Modified;
            _Context.SaveChanges();
            return Ok(reto);

        }
    }
}
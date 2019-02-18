using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        [Route("ObtenerRetos/{email}")]
        public IEnumerable<Reto> Get(string email)
        {
            return _Context.Retos.Where(r => r._EmailUsuOri == email || r._EmailUsuDes == email).ToList();
            

        }

        [HttpGet] 
        [Route("ObtenerReto/{idReto}")]
        public IActionResult ObtenerReto(int idReto)
        {
            var reto = _Context.Retos.FirstOrDefault(r => r._IdReto == idReto);

            if (reto != null)
            {
                return Ok(reto);
            }

            return NotFound(); ;



        }

        [HttpPost]
        public IActionResult Post([FromBody] Reto reto)
        {

            if (ModelState.IsValid)
            {
                Reto r = _Context.Retos.FirstOrDefault(x => x._EmailUsuOri == reto._EmailUsuOri && 
                                                            x._EmailUsuDes == reto._EmailUsuDes && 
                                                            x._RecetaId == reto._RecetaId && 
                                                            x._IdEstadoReto < 6);

                if (r == null)
                {
                    reto._IdReto = 0;
                    _Context.Retos.Add(reto);
                    _Context.SaveChanges();

                    //await GenerarNotificacion(reto);
                    
                    return Ok(reto._IdReto);
                }
                else
                {
                    return Conflict(-1);
                }

            }

            return BadRequest(ModelState);
        }

        private async Task<IActionResult> GenerarNotificacion(Reto reto)
        {
            reto._PerfilUsuOri = _Context.Perfiles.Where(p => p._Email == reto._EmailUsuOri).FirstOrDefault();
            reto._PerfilUsuDes = _Context.Perfiles.Where(p => p._Email == reto._EmailUsuDes).FirstOrDefault();
            reto._Receta = _Context.Recetas.Where(r => r._IdReceta == reto._RecetaId).FirstOrDefault();

            Guid? DeviceIdOri = _Context.Usuarios.FirstOrDefault(u => u._Email == reto._PerfilUsuOri._Email)._DeviceId;
            Guid? DeviceIdDes = _Context.Usuarios.FirstOrDefault(u => u._Email == reto._PerfilUsuDes._Email)._DeviceId;


            NotificacionAppCenter notificacionAppCenter = reto.NotificarReto(DeviceIdOri, DeviceIdDes);
            NotificacionService notificacionService = new NotificacionService();
            
            Notificacion notificacion = new Notificacion()
            {                   
                _Email = (reto._IdEstadoReto >= 2 && reto._IdEstadoReto <= 4) ? reto._PerfilUsuOri._Email : reto._PerfilUsuDes._Email,
                _Estado = Notificacion.Estado.SinLeer,
                _FechaHora = DateTime.Now,
                _Titulo = notificacionAppCenter.Notificacion_content.Title,
                _Descripcion = notificacionAppCenter.Notificacion_content.Body,
                _Tabla = "Reto",
                _Pk1 = reto._IdReto.ToString()
            };

            _Context.Notificaciones.Add(notificacion);
            _Context.SaveChanges();

            notificacionAppCenter.Notificacion_content.Custom_Data.Add("NotificacionID", notificacion._NotificacionId.ToString());
            notificacionAppCenter.Notificacion_content.Custom_Data.Add("RetoID", reto._IdReto.ToString());

            await notificacionService.Enviar(notificacionAppCenter);

            return Ok();
            
        }



        [HttpPut]
        public IActionResult Put([FromBody] Reto reto)
        {
            if (ModelState.IsValid)
            {
                Reto r = _Context.Retos.FirstOrDefault(x => x._IdReto == reto._IdReto);

                if (r != null)
                {
                    try
                    {
                        r._IdEstadoReto = reto._IdEstadoReto;
                        r._Presentacion = (reto._IdEstadoReto == 4) ? reto._Presentacion : r._Presentacion;
                        r._ComentarioDestino = reto._ComentarioDestino;

                        _Context.Entry(r).State = EntityState.Modified;
                        int save =  _Context.SaveChanges();

                        if (save == 1 && reto._IdEstadoReto == 5) {

                            Perfil perfil = _Context.Perfiles.FirstOrDefault(p => p._Email == r._EmailUsuDes);
                            perfil.SumarPuntaje(r._Puntaje);
                            perfil.VerificarCategoria();

                            _Context.Entry(perfil).State = EntityState.Modified;
                            _Context.SaveChanges();

                        }

                        //await GenerarNotificacion(r);


                        return Ok();
                    }
                    catch (DbUpdateException ex) {

                        Debug.Print(ex.Message);

                    }
                }
                else
                {
                    return Conflict();
                }

            }

            return BadRequest(ModelState);

        }
    }
}
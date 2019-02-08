using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CookItWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Cuenta")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _Context;

        public UsuariosController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, 
            ApplicationDbContext context) {

            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _Context = context;
        }

        
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario Usuario)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Usuario._Email, Email = Usuario._Email };
                var result = await _userManager.CreateAsync(user, Usuario._Password);
                if (result.Succeeded)
                {
                    _Context.Usuarios.Add(Usuario);
                    _Context.SaveChanges();
                    return BuildToken(Usuario);
                }
                else
                {
                    return BadRequest("Usuario ya existente.");
                }
            }
            else
            {
                return BadRequest(ModelState);        
            }

        }

        [HttpPost]
        [Route("Ingresar")]
        public async Task<IActionResult> Login([FromBody] Usuario Usuario)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(Usuario._Email, Usuario._Password,false,false);
                if (result.Succeeded)
                {

                    _Context.Entry(Usuario).Property("_DeviceId").IsModified = true;
                    _Context.Entry(Usuario).Property("_UltimoIngreso").IsModified = true;
                    _Context.SaveChanges();                   
                    
                    return BuildToken(Usuario);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);        
            }
        }


        [HttpPost]
        [Route("Restablecer/{email}")]
        public async Task<IActionResult> Restablecer(string email)
        {
            var encontrado = await _userManager.FindByEmailAsync(email);

            if (encontrado != null)
            {
                var tokenPass = await _userManager.GeneratePasswordResetTokenAsync(encontrado);
                var newPass = CrearPassword();
                var pass = await _userManager.ResetPasswordAsync(encontrado, tokenPass, newPass);
                
                Usuario usuario = _Context.Usuarios.FirstOrDefault(u => u._Email == email);
                Perfil perfil = _Context.Perfiles.FirstOrDefault(p => p._Email == email);
                usuario._Perfil = perfil;
                EnviarEmail(usuario, newPass);
                return Ok(pass);
            }
            else
            {
                return NotFound();
            }          
        }

        [HttpPut]
        [Route("CambiarCont")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CambiarCont([FromBody] Dictionary<string, string> obj)
        {
            ApplicationUser usuario = await _userManager.FindByEmailAsync(obj["Email"]);

            if (usuario != null)
            {
                var resultado = await _userManager.ChangePasswordAsync(usuario, obj["ContAnterior"], obj["ContNueva"]);

                if (resultado.Succeeded)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound(resultado);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private void EnviarEmail(Usuario usuario, string pass)
        {

            Perfil perfil = usuario._Perfil;
            string nombre = perfil != null ? perfil._Nombre + " " + perfil._Apellido : usuario._Email;            

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CookIt! Admin", "cookit.project.ort@gmail.com"));
            message.To.Add(new MailboxAddress(nombre, usuario._Email));
            message.Subject = "Restablecimiento de contraseña";
            message.Body = new TextPart("plain") { Text = "Hola " + nombre + ", tu nueva contraseña es: " + pass };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("cookit.project.ort@gmail.com", "Integrador2018");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        private IActionResult BuildToken(Usuario Usuario)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, Usuario._Email),                
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime exp = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(            
                issuer: "*",
                audience: "*",
                claims: claims,
                expires: exp,
                signingCredentials: creds
            );

            return Ok(new
            {
                _AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                _ExpireDate = exp
            });

        }

        public string CrearPassword()
        {
            int longitud = 10;
            bool check = true;
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!*@#$%^&+=";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (check)
            {                 
                while (0 < longitud--)
                {
                    res.Append(caracteres[rnd.Next(caracteres.Length)]);
                }
                if (Check(res.ToString()))
                {
                    check = false;
                }
                else
                {
                    longitud = 10;
                    res.Clear();
                }
            }
            return res.ToString();
        }

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }
            
            Regex regex = new Regex(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            Match match = regex.Match(value);

            return match.Success;
        }

    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CookItWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        
        [HttpPost, Route("Registrar")] //Registro]
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

        [HttpPost, Route("Ingresar")]
        public async Task<IActionResult> Login([FromBody] Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Usuario._Email, Usuario._Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {                                       
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
    }
}
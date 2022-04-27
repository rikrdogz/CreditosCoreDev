using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CreditosCore.Controllers.Usuarios
{
    [ApiController]
    [Route("[controller]")]
    public class InicioSesionController : Controller
    {
        InicioSesionService serviceInicioSesion;
        public InicioSesionController()
        {
            serviceInicioSesion = new InicioSesionService();
        }

        [HttpPost]
        public IActionResult iniciar([FromBody] UsuarioPorIniciarViewModel usuarioData)
        {
            IActionResult respuesta = Unauthorized();

            try
            {
                var usuarioValido = serviceInicioSesion.ValidarInicio(usuarioData);

                if (usuarioValido == null)
                {
                    return BadRequest("usuario o contraseña incorrecta");
                }

                usuarioValido.token = serviceInicioSesion.GenerarToken(usuarioData);

                return Ok(usuarioValido);
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo iniciar sesion");
            }

            
        }

        [HttpGet]
        [Authorize]
        public IActionResult Obtener()
        {
            try
            {
                return Ok(serviceInicioSesion.CantidadUsuarios());
            }
            catch (Exception)
            {
                return BadRequest("No se pudo obtener informacion");
            }
        }
        
    }
}

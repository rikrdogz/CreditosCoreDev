using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult iniciar([FromBody] UsuariosModel)
        
    }
}

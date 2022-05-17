using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Clientes
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private ClientesService servicioCliente;

        
        public ClientesController()
        {
            servicioCliente = new ClientesService();
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {

                
                var listaClientes =  servicioCliente.ObtenerListaClientes();
                return Ok(listaClientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult guardarCliente([FromBody] ClientesModel cliente)
        {
            try
            {
                var idCliente = servicioCliente.AgregarCliente(cliente);
                return Ok(idCliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

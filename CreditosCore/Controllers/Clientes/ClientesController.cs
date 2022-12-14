using CreditosCore.Shared;
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

        [HttpGet("info")]
        public IActionResult info()
        {
            try
            {
                return Ok(servicioCliente.ObtenerClientesCredito());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPut]
        public IActionResult ActualizarCliente([FromBody] ClientesModel cliente)
        {
            try
            {
                var clienteGuardado = servicioCliente.ActualizarCliente(cliente);
                return Ok(clienteGuardado);
            }
            catch (CreditoSistemaExcepcion ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("No se pudo guardar el cliente");
            }
        }
    }
}

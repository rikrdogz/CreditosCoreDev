using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    [ApiController]
    [Route("[controller]")]
    public class CreditosController : Controller
    {
        CreditosService creditoServicio;
        public CreditosController()
        {
            creditoServicio = new CreditosService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var listaCreditos = creditoServicio.ObtenerCreditos();
                return Ok(listaCreditos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GuardarCredito([FromBody] CreditoViewModel creditoDatos)
        {
            try
            {
                var idCredito = creditoServicio.GuardarCredito(creditoDatos);

                return Ok(idCredito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("porcliente/{idcliente}")]
        public IActionResult porcliente(int idcliente)
        {
            try
            {
                return Ok(creditoServicio.ObtenerCreditosDelCliente(idcliente));
            }
            catch (Exception)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

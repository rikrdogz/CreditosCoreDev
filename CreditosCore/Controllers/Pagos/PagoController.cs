using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Pagos
{
    [ApiController]
    [Route("[controller]")]
    public class PagoController : Controller
    {
        PagoService servicePago;
        public PagoController()
        {
            servicePago = new PagoService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return Ok(servicePago.ObtenerPagos());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GuardarPago([FromBody] PagosModel pago)
        {
            try
            {
                return Ok(servicePago.AgregarPagoCliente(pago));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

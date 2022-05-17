using CreditosCore.Controllers.Clientes;
using CreditosCore.Database;
using CreditosCore.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Creditos
{
    public class CreditosService
    {
        private SqlDataContext db;
        
        public CreditosService()
        {
            db = new SqlDataContext();
        }

        public List<CreditosModel> ObtenerCreditos()
        {
            var listaCreditos = db.creditos.ToList();
            return listaCreditos;
        }

        public List<CreditosModel> ObtenerCreditosDelCliente(int idCliente)
        {
            var listaCreditos = db.creditos.AsNoTracking().Where(c => c.ClienteId == idCliente).ToList();
            return listaCreditos;
        }

        public CreditosModel BuscarCredito(int idCredito)
        {
            return db.creditos.AsNoTracking().Where(c => c.CreditoId == idCredito).FirstOrDefault();
        }

        

        public int GuardarCredito(CreditoViewModel creditoDatos)
        {
            try
            {
                creditoDatos.cliente.ClienteId = creditoDatos.credito.ClienteId;

                //validacion
                if (creditoDatos.credito.ClienteId == 0)
                {
                    throw new Exception("No se establecio el cliente");
                }

                //Obtener datos del cliente
                var clienteInfo = new ClientesService().ObtenerClientePorId(creditoDatos.cliente.ClienteId);
                 
                if (clienteInfo == null)
                {
                    throw new Exception("No se encontro el cliente enviado");
                }

                //Establecer Campos de fecha
                creditoDatos.credito.FechaCreacion = System.DateTime.Today;

                creditoDatos.credito.FechaModificacion = System.DateTime.Today;

                ValidarNuevoCredito(creditoDatos.credito);

                db.creditos.Add(creditoDatos.credito);
                db.SaveChanges();

                return creditoDatos.credito.CreditoId;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ValidarSiExisteCreditoActualConElCliente(int idCliente)
        {
            var creditoPendiente = db.creditos.Where(c => c.ClienteId == idCliente).FirstOrDefault();

            var pagosRealizados = new pagoservice()
        }

        public bool ValidarNuevoCredito(CreditosModel credito)
        {
            
            if (credito == null)
            {
                throw new CreditoSistemaExcepcion("No se establecio los datos del credito");

            }

            if (credito.ClienteId <=0 )
            {
                throw new CreditoSistemaExcepcion("No se establecio el cliente");
            }

            if (credito.ComisionFaltaPago < 0)
            {
                throw new CreditoSistemaExcepcion("Debe poner una comision por falta de pago mayor o igual a cero");
            }

            if (credito.MontoPago <= 0)
            {
                throw new CreditoSistemaExcepcion("Debe ingresar un monto de pago recurrente");
            }

            if (credito.ComisionFaltaPago > credito.MontoPago)
            {
                throw new CreditoSistemaExcepcion("La Comision por falta de pago no debe ser mayor al monto del pago");
            }


            if (credito.DescuentoPagoFinal <= 0)
            {
                throw new CreditoSistemaExcepcion("Debe ingresar un descuento final mayor o igual a cero");
            }

            if (credito.MontoInteres > credito.MontoPrestamo)
            {
                throw new CreditoSistemaExcepcion("El monto del interes no debe ser mayor a monto prestado");
            }

            if (credito.MontoTotal != (credito.MontoPrestamo + credito.MontoInteres) )
            {
                throw new CreditoSistemaExcepcion("El monto total debe ser la suma de monto prestado + monto interes");
            }

            if (credito.Plazos == 0)
            {
                throw new CreditoSistemaExcepcion("El monto total debe ser la suma de monto prestado + monto interes");
            }

            if (credito.FechaCredito == DateTime.MinValue)
            {
                throw new CreditoSistemaExcepcion("Debe establecer la fecha del credito");
            }

            return true;
           
        }
    }
}

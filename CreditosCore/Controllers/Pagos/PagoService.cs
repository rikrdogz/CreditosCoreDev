using CreditosCore.Controllers.Creditos;
using CreditosCore.Database;
using CreditosCore.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Pagos
{
    public class PagoService
    {
        SqlDataContext db;

        public PagoService()
        {
            db = new SqlDataContext();
        }

        public List<PagosModel> ObtenerPagos()
        {
            try
            {
                return db.pagos.AsNoTracking().ToList();
            }
            catch (Exception)
            {

                throw new Exception("No se pudo obtener los pagos");
            }
        }

        public decimal SumarPagosDelCredito(int idCredito)
        {
            var pagosMonto = db.pagos.Where(p => p.CreditoId == idCredito).Sum(c => c.Monto);

            return pagosMonto;
        }


        /// <summary>
        /// buscar que el monto a pagar sea valido, que no rebase el monto maximo del monto en el credito y que la 
        /// suma de los pagos anteriores no sea mayor al montoTotal
        /// </summary>
        /// <param name="pago"></param>
        /// <returns></returns>
        public bool EsMontoPagoValido(PagosModel pago)
        {
            var credito = new CreditosService().BuscarCredito(pago.CreditoId);
            
            if (credito == null)
            {
                throw new CreditoSistemaExcepcion("No se encontro el credito a pagar");
            }

            var valorMaximo = credito.MontoPago;

            if (pago.Monto > valorMaximo )
            {
                throw new CreditoSistemaExcepcion($"No se puede realizar el pago de monto mayor a {valorMaximo}, favor validar");
            }

            var sumaPagos = SumarPagosDelCredito(pago.CreditoId);

            if ( sumaPagos >= credito.MontoTotal)
            {
                throw new CreditoSistemaExcepcion($"El pago no es posible continuar, ya que el cretido {credito.CreditoId} ya esta pago previamente");
            }

            var totalMasMonto = sumaPagos + pago.Monto;

            var pagoSugerido = credito.MontoTotal - sumaPagos;

            if (totalMasMonto > credito.MontoTotal)
            {
                throw new CreditoSistemaExcepcion($"El monto a pagar supera el maximo para el credito, este monto del pago debe ser {pagoSugerido}");
            }

            return true;

        }

        public int AgregarPagoCliente(PagosModel pago)
        {
            try
            {
                if (ValidacionPagoNuevo(pago))
                {
                    db.Add(pago);
                    db.SaveChanges();
                }
                
            }
            catch (CreditoSistemaExcepcion ex)
            {
                throw ex;
            }
            catch (Exception)
            {

                throw new Exception("No se pudo guardar el pago");
            }
            

            return pago.PagoId;
        }

        bool ValidacionPagoNuevo(PagosModel pago)
        {
            
            if (pago.CreditoId <=0 )
            {
                throw new CreditoSistemaExcepcion("No se estalecio el numero del credito");
            }

            if (pago.EstatusId <=0)
            {
                throw new CreditoSistemaExcepcion("No se establecio el Estatus del pago");
            }

            if (pago.fechaPago == null | pago.fechaPago == DateTime.MinValue)
            {
                throw new CreditoSistemaExcepcion("No se establecio la fecha del pago");
            }

            if (pago.Monto <=0)
            {
                throw new CreditoSistemaExcepcion("No se establecio el monto del pago");
            }

            var exiteCredito = new CreditosService().BuscarCredito(pago.CreditoId);

            if (exiteCredito == null)
            {
                throw new CreditoSistemaExcepcion("No existe credito para realizar el pago");
            }
            
            EsMontoPagoValido(pago);

            return true;
        }


    }
}

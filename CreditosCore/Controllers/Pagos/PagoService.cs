using CreditosCore.Controllers.Creditos;
using CreditosCore.Database;
using CreditosCore.Shared;
using Humanizer;
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
        TimeZoneInfo zoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

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

            if (pago.descuento > credito.DescuentoPagoFinal)
            {
                throw new CreditoSistemaExcepcion($"El monto de descuento no puede ser mayor a {credito.DescuentoPagoFinal} ,establecido en el credito.");
            }

            if (pago.faltaDePago > credito.ComisionFaltaPago)
            {
                throw new CreditoSistemaExcepcion($"El monto de comision por falta de pago no puede ser mayor a {credito.ComisionFaltaPago} ,establecido en el credito.");
            }

            return true;

        }

        public List<PagoCreditoSumViewModel> BuscarCreditosPagadosCompletamente()
        {
            var lista = from credito in db.creditos.AsNoTracking()
                        join pago in db.pagos.AsNoTracking()
                        on credito.CreditoId equals pago.CreditoId
                        group pago by new { idCredito = credito.CreditoId, montoTotalCredito = credito.MontoTotal, montoRecurrente = credito.MontoPago } into grupoPago
                        where grupoPago.Sum(p => p.Monto) == grupoPago.Key.montoTotalCredito
                        select new PagoCreditoSumViewModel() { creditoId = grupoPago.Key.idCredito, sumaMontos = grupoPago.Sum(p => p.Monto), montoPagoRecurrente = grupoPago.Key.montoRecurrente };

            return lista.ToList();
        }

        public List<PagoViewModel> BuscarPorCliente(int idCliente)
        {
            var lista = from pago in db.pagos.AsNoTracking()
                        join credito in db.creditos.AsNoTracking()
                        on new { pago.CreditoId } equals new { credito.CreditoId }
                        join cliente in db.clientes.AsNoTracking()
                        on new { credito.ClienteId } equals new { cliente.ClienteId }
                        where cliente.ClienteId == idCliente
                        orderby pago.PagoId
                        select new PagoViewModel() { 
                            idPago = pago.PagoId, 
                            descuento = pago.descuento, faltaPago = pago.faltaDePago, 
                            fechaPago = pago.fechaPago.ToString("dd/MM/yyyy"), 
                            monto = pago.Monto, 
                            fechaCreacionPago = pago.fechaCreacion.ToString(),
                            nombre = ($"{cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}"), numeroPago = "#" };

            var listaPagos = lista.AsTracking().ToList();

            foreach (var pago in listaPagos)
            {
                pago.fechaCreacionPago = DateTime.Parse(pago.fechaCreacionPago).AddSeconds(30).Humanize(true).ToString();
                

            }

            return listaPagos.ToList();
        }

        public int AgregarPagoCliente(PagosModel pago)
        {
            try
            {
                if (pago?.fechaCreacion != null)
                {
                    pago.fechaCreacion = DateTime.UtcNow.ToUniversalTime();
                }

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
            catch (Exception ex)
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

using CreditosCore.Controllers.Clientes;
using CreditosCore.Controllers.Pagos;
using CreditosCore.Database;
using CreditosCore.Shared;
using Humanizer;
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

        public CreditoActivoViewModel Credito(int idCredito)
        {
            if (idCredito == 0)
            {
                return null;
            }
            var creditos = from grupoCredito in ObtenerQueryCreditoConCliente(0, idCredito: idCredito).ToList()
                           select new
                           {
                               creditoId = grupoCredito.idCredito,
                               cliente = grupoCredito.cliente,
                               idCliente = grupoCredito.idCliente,
                               montoTotal = grupoCredito.montoTotal,
                               montoRecurrente = grupoCredito.montoRecurrente,
                               fechaModificacion = grupoCredito.fechaModificacion.ToString(),
                               pagos = db.pagos.AsNoTracking().Where(p => p.CreditoId == grupoCredito.idCredito).ToList(),
                           }
                              into joinCreditoPago
                           select new CreditoActivoViewModel()
                           {
                               idCredito = joinCreditoPago.creditoId,
                               cliente = joinCreditoPago.cliente,
                               idCliente = joinCreditoPago.idCliente,
                               montoTotal = joinCreditoPago.montoTotal,
                               idPagoUltimo = joinCreditoPago.pagos.ToList().OrderByDescending(p => p.PagoId).FirstOrDefault().PagoId,
                               fechaUltimoPago = joinCreditoPago.pagos.ToList().OrderByDescending(p => p.PagoId).FirstOrDefault().fechaPago.ToString("dd/MM/yyyy"),
                               montoPagado = joinCreditoPago.pagos.ToList().Sum(p => p.Monto),
                               numeroPago = 0,
                               fechaModificacion = joinCreditoPago.fechaModificacion,
                               montoRecurrente = joinCreditoPago.montoRecurrente,
                               pendientePago = joinCreditoPago.montoTotal - joinCreditoPago.pagos.ToList().Sum(p => p.Monto)
                           };

            var creditoEncontrado = creditos.OrderByDescending(c => c.idCredito).FirstOrDefault();
            //Establecer fecha humanizer
            if (creditoEncontrado != null)
            {
                creditoEncontrado.fechaModificacion = DateTime.Parse(creditoEncontrado.fechaModificacion).Humanize(true).ToString();
            }

            return creditoEncontrado;
        }

        public List<CreditosModel> ObtenerCreditosDelCliente(int idCliente)
        {
            var listaCreditos = db.creditos.AsNoTracking().Where(c => c.ClienteId == idCliente).ToList();
            return listaCreditos;
        }

        public CreditoActivoViewModel ObtenerCreditoActivo(int idCliente)
        {
            if (idCliente == 0)
            {
                return null;
            }
            var creditoActivo = from grupoCredito in ObtenerQueryCreditoConCliente(idCliente).ToList()
                                select new
                                {
                                    creditoId = grupoCredito.idCredito,
                                    cliente = grupoCredito.cliente,
                                    idCliente = grupoCredito.idCliente,
                                    montoTotal = grupoCredito.montoTotal,
                                    montoRecurrente = grupoCredito.montoRecurrente,
                                    pagos = db.pagos.AsNoTracking().Where(p => p.CreditoId == grupoCredito.idCredito).ToList(),
                                }
                              into joinCreditoPago
                                select new CreditoActivoViewModel()
                                {
                                    idCredito = joinCreditoPago.creditoId,
                                    cliente = joinCreditoPago.cliente,
                                    idCliente = joinCreditoPago.idCliente,
                                    montoTotal = joinCreditoPago.montoTotal,
                                    idPagoUltimo = joinCreditoPago.pagos.ToList().OrderByDescending(p => p.PagoId).FirstOrDefault().PagoId,
                                    fechaUltimoPago = joinCreditoPago.pagos.ToList().OrderByDescending(p => p.PagoId).FirstOrDefault().fechaPago.ToString("dd/MM/yyyy"),
                                    montoPagado = joinCreditoPago.pagos.ToList().Sum(p => p.Monto),
                                    numeroPago = 0,
                                    montoRecurrente = joinCreditoPago.montoRecurrente,
                                    pendientePago = joinCreditoPago.montoTotal - joinCreditoPago.pagos.ToList().Sum(p => p.Monto)
                                };

            return creditoActivo.OrderByDescending(c=> c.idCredito).Where(c=> c.pendientePago > 0).FirstOrDefault();
        }

        public CreditosModel BuscarCredito(int idCredito)
        {
            return db.creditos.AsNoTracking().Where(c => c.CreditoId == idCredito).FirstOrDefault();
        }


        public List<CreditoActivoViewModel> BuscarCreditosPendientesPago()
        {

            var creditos =  from grupoCredito in ObtenerQueryCreditoConCliente(0).ToList()
                              select new
                              {
                                  creditoId = grupoCredito.idCredito,
                                  idCliente = grupoCredito.idCliente,
                                  cliente = grupoCredito.cliente,
                                  montoTotal = grupoCredito.montoTotal,
                                  montoRecurrente = grupoCredito.montoRecurrente,
                                  pagos = db.pagos.AsNoTracking().Where(p => p.CreditoId == grupoCredito.idCredito).ToList(),
                              }
                              into joinCreditoPago
                              select new CreditoActivoViewModel()
                              {
                                  idCredito = joinCreditoPago.creditoId,
                                  cliente = joinCreditoPago.cliente,
                                  idCliente = joinCreditoPago.idCliente,
                                  montoTotal = joinCreditoPago.montoTotal,
                                  idPagoUltimo = joinCreditoPago.pagos.ToList().OrderByDescending(p => p.PagoId).FirstOrDefault().PagoId,
                                  fechaUltimoPago = joinCreditoPago.pagos.ToList().OrderByDescending(p => p.PagoId).FirstOrDefault().fechaPago.ToString("dd/MM/yyyy"),
                                  montoPagado = joinCreditoPago.pagos.ToList().Sum(p => p.Monto),
                                  numeroPago = 0,
                                  montoRecurrente = joinCreditoPago.montoRecurrente,
                                  pendientePago = joinCreditoPago.montoTotal - joinCreditoPago.pagos.ToList().Sum(p => p.Monto)
                              };


            return creditos.Where(c=> c.pendientePago > 0).ToList();
        }

        public IEnumerable<CreditoActivoViewModel> ObtenerQueryCreditoConCliente(int idCliente, int idCredito = 0)
        {
            return from credito in db.creditos.AsNoTracking()
                   join cliente in db.clientes.AsNoTracking()
                   on credito.ClienteId equals cliente.ClienteId

                   where cliente.ClienteId == (idCliente ==0 ? cliente.ClienteId : idCliente)
                   && credito.CreditoId ==(idCredito == 0 ? credito.CreditoId : idCredito)

                   select new CreditoActivoViewModel()
                   {
                       idCredito = credito.CreditoId,
                       idCliente = cliente.ClienteId,
                       cliente = $"{cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}",
                       montoTotal = credito.MontoTotal,
                       montoRecurrente = credito.MontoPago,
                       fechaModificacion = credito.FechaModificacion.ToString()
                   }
                    into grupoCredito select grupoCredito;
        }

        public int GuardarCredito(CreditosModel creditoDatos)
        {
            try
            {
                //validacion
                if (creditoDatos.ClienteId == 0)
                {
                    throw new Exception("No se establecio el cliente");
                }

                //Obtener datos del cliente
                var clienteInfo = new ClientesService().ObtenerClientePorId(creditoDatos.ClienteId);
                 
                if (clienteInfo == null)
                {
                    throw new Exception("No se encontro el cliente enviado");
                }

                //Establecer Campos de fecha
                creditoDatos.FechaCreacion = DateTime.UtcNow.ToUniversalTime();

                creditoDatos.FechaModificacion = DateTime.UtcNow.ToUniversalTime();

                ValidarNuevoCredito(creditoDatos);

                db.creditos.Add(creditoDatos);
                db.SaveChanges();

                return creditoDatos.CreditoId;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ValidarSiExisteCreditoActualConElCliente(int idCliente)
        {
            var creditoPendiente = db.creditos.Where(c => c.ClienteId == idCliente).FirstOrDefault();

            return (creditoPendiente != null);
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

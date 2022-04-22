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

            return true;
        }


    }
}

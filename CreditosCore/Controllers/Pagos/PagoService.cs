using CreditosCore.Database;
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
        public int AgregarPagoCliente(PagosModel pago)
        {
            db.Add(pago);
            db.SaveChanges();

            return pago.PagoId;
        }
    }
}

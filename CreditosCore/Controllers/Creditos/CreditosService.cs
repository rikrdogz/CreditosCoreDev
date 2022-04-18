using CreditosCore.Controllers.Clientes;
using CreditosCore.Database;
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

                db.creditos.Add(creditoDatos.credito);
                db.SaveChanges();

                return creditoDatos.credito.CreditoId;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

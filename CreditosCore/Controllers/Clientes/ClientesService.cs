using CreditosCore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers.Clientes
{
    public class ClientesService
    {
        private SqlDataContext db;
        public ClientesService()
        {
            db = new SqlDataContext();
        }

        public List<ClientesModel> ObtenerListaClientes()
        {
            try
            {
                return db.clientes.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int AgregarCliente(ClientesModel cliente)
        {
            db.clientes.Add(cliente);
            db.SaveChanges();

            return cliente.ClienteId;
        }
    }
}

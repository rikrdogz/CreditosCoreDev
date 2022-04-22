using CreditosCore.Database;
using Microsoft.EntityFrameworkCore;
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
                return db.clientes.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ClientesModel ObtenerClientePorId(int idCliente)
        {
            try
            {
                return db.clientes.AsNoTracking().Where(c => c.ClienteId == idCliente).FirstOrDefault();
            }
            catch (Exception)
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

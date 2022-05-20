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

        public List<ClientesViewModel> ObtenerClientesCredito()
        {
            try
            {
                var clientes = from cliente in db.clientes.AsNoTracking()
                               select new
                               {
                                   ClienteId = cliente.ClienteId,
                                   creditos = db.creditos.Where(c => c.ClienteId == cliente.ClienteId).ToList()
                               }
                               into grupoClienteCredito
                               select new
                               {
                                   ClienteId = grupoClienteCredito.ClienteId,
                                   numeroCreditos = grupoClienteCredito.creditos.ToList().Count(),
                                   total = grupoClienteCredito.creditos.ToList().Sum(c => c.MontoTotal),
                                   listaPagos = db.pagos.Where(p => grupoClienteCredito.creditos.Select(c => c.CreditoId).Contains(p.CreditoId)).ToList()
                               }
                               into grupoCredito
                               select new ClientesViewModel()
                               {
                                   ClienteId = grupoCredito.ClienteId,
                                   numeroCreditos = grupoCredito.numeroCreditos,
                                   montoPendiente = grupoCredito.total - grupoCredito.listaPagos.Sum(p => p.Monto)
                               };
                               

                return clientes.ToList();
                    

            }
            catch (Exception)
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

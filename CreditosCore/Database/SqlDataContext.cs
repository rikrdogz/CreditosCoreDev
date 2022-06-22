using CreditosCore.Controllers.Clientes;
using CreditosCore.Controllers.Creditos;
using CreditosCore.Controllers.Pagos;
using CreditosCore.Controllers.Usuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Database
{
    public class SqlDataContext: DbContext
    {
        public DbSet<ClientesModel> clientes { get; set; }
        public DbSet<CreditosModel> creditos { get; set; }
        public DbSet<CreditoAmortizacionPagosModel> creditosAmortiguacion { get; set; }

        public DbSet<PagosModel> pagos { get; set; }
        public DbSet<UsuariosModel> usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process));
        }

    }
}

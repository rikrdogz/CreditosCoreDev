using CreditosCore.Controllers.Clientes;
using CreditosCore.Controllers.Creditos;
using CreditosCore.Controllers.Pagos;
using CreditosCore.Controllers.Usuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CreditosCore.Database
{
    public class SqlDataContext : DbContext
    {
        public static bool isDBFileCreated = false;
        public static string fileNameDatabase = "";
        public SqlDataContext()
        {
            if (!isDBFileCreated)
            {
                isDBFileCreated = true;
                string _nameVariableEnv = Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process);

                if (_nameVariableEnv != null)
                {
                    fileNameDatabase = _nameVariableEnv;
                }
            }
        }
        public DbSet<ClientesModel> clientes { get; set; }
        public DbSet<CreditosModel> creditos { get; set; }
        public DbSet<CreditoAmortizacionPagosModel> creditosAmortiguacion { get; set; }

        public DbSet<PagosModel> pagos { get; set; }
        public DbSet<UsuariosModel> usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process));

            var UbicacionArchivoBD = @AppDomain.CurrentDomain.BaseDirectory + fileNameDatabase;

            optionsBuilder.UseSqlite("Filename=" + UbicacionArchivoBD, 
                
                sqliteOptionsAction: op => {

                    op.MigrationsAssembly(

                        Assembly.GetExecutingAssembly().FullName
                    );

                }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                  
                }
            }
        }

        public SqlDataContext(DbContextOptions <SqlDataContext> options):base(options)
        {

        }



    }
}

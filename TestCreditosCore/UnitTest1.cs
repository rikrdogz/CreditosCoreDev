using NUnit.Framework;
using CreditosCore;
using CreditosCore.Controllers.Clientes;
using System.Linq;
using CreditosCore.Controllers.Creditos;
using System.Collections.Generic;
using CreditosCore.Controllers.Pagos;
using CreditosCore.Shared;
using CreditosCore.Database;
using Microsoft.EntityFrameworkCore;

namespace TestCreditosCore
{
    public class Tests
    {
        ClientesService serviceCliente;
        CreditosService serviceCreditos;
        PagoService servicePago; 

        List<ClientesModel> clientesTemporales;
        List<ClientesModel> ClientesAgregados;
        List<CreditosModel> creditoAgregados;

        SqlDataContext dataContext;

        public Tests()
        {
            serviceCliente = new ClientesService();
            serviceCreditos = new CreditosService();
            servicePago = new PagoService();

            ClientesAgregados = new List<ClientesModel>();
            clientesTemporales = new List<ClientesModel>();
            creditoAgregados = new List<CreditosModel>();

            
        }

        [SetUp]
        public void Setup()
        {
            SqlDataContext.fileNameDatabase = "CreditoTest.db";
            dataContext = new SqlDataContext();
            dataContext.Database.Migrate();
        }

   
        [Test, Order(1)]
        public void AgregarCliente()
        {
            var cliente = new ClientesModel()
            {
                ClienteId = 0,
                ApellidoMaterno = Faker.Name.Middle(),
                ApellidoPaterno = Faker.Name.Last(),
                Correo = Faker.Internet.Email(),
                Nombre = Faker.Name.FullName(Faker.NameFormats.Standard)
            };

            clientesTemporales.Add(cliente);

            Assert.AreEqual(cliente.ClienteId, 0, "Cliente creado");
            
        }

        [Test, Order(2)]
        public void AgregarClientesTemporalesMasivos()
        {

            if(clientesTemporales.Count == 0)
            {
                throw new System.Exception("no se encontraron clientes temporales");
            }

            foreach (var cliente in clientesTemporales)
            {
                serviceCliente = new ClientesService();
                serviceCliente.AgregarCliente(cliente);
                Assert.IsTrue(cliente.ClienteId > 0, "No se guardo el cliente");
            }

        }

        [Test, Order(3)]
        public void AgregarClientesGuardadosBD()
        {
            var listaClientes = serviceCliente.ObtenerListaClientes();
            if (listaClientes.Count > 0)
            {
                ClientesAgregados = listaClientes;
            }
        }

        [Test, Order(4)]
        public void AgregarCredito()
        {
            //obtener un cliente por default
            var clienteDefault = serviceCliente.ObtenerListaClientes().FirstOrDefault();

            var credito = new CreditosModel()
            {
                ClienteId = clienteDefault.ClienteId,
                ComisionFaltaPago = 20,
                DescuentoPagoFinal = 30,
                FechaCredito = new System.DateTime(2022, 04, 29),
                Folio = "111",
                MontoInteres = 350,
                MontoPago = 150,
                MontoPrestamo = 1000,
                MontoTotal = 1350,
                Plazos = 9,
                TipoPago = 1
            };


            var idCredito = serviceCreditos.GuardarCredito(credito);

            bool Comparacion = (idCredito > 0);

            Assert.IsTrue(Comparacion, "No se pudo registrar el credito");

            if (Comparacion)
            {
                this.creditoAgregados.Add(credito);
            }

        }

        [Test, Order(5)]
        public void AgregarPago()
        {
            foreach (var credito in creditoAgregados)
            {
                var pago = new PagosModel()
                {
                    CreditoId = credito.CreditoId,
                    fechaCreacion = System.DateTime.UtcNow,
                    EstatusId = 1,
                    idUsuario = 1,
                    Monto = 150,
                    fechaPago = System.DateTime.UtcNow

                };
               
                servicePago.AgregarPagoCliente(pago);

                Assert.IsTrue(pago.PagoId > 0, "No se guardo el pago");

            }
        }
        
        [Test, Order(6)]
        public void AgregarPagoCreditoExistente()
        {
            foreach (var credito in serviceCreditos.BuscarCreditosPendientesPago())
            {
                for (int i = 0; i < 2; i++)
                {
                    var pago = new PagosModel()
                    {
                        CreditoId = credito.idCredito,
                        fechaCreacion = System.DateTime.UtcNow,
                        EstatusId = 1,
                        idUsuario = 1,
                        Monto = credito.montoRecurrente < credito.pendientePago ? credito.montoRecurrente : credito.pendientePago,
                        fechaPago = System.DateTime.UtcNow

                    };

                    servicePago.AgregarPagoCliente(pago);

                    Assert.IsTrue(pago.PagoId > 0, "No se guardo el pago");
                }
                
            }
        }
        
    
        [Test, Order(9)]
        public void PagoNoPasables()
        {
            foreach (var credito in servicePago.BuscarCreditosPagadosCompletamente())
            {
                for (int i = 0; i < 2; i++)
                {
                    var pago = new PagosModel()
                    {
                        CreditoId = credito.creditoId,
                        fechaCreacion = System.DateTime.UtcNow,
                        EstatusId = 1,
                        idUsuario = 1,
                        Monto = 150,
                        fechaPago = System.DateTime.UtcNow

                    };

                    try
                    {
                        servicePago.AgregarPagoCliente(pago);
                    }
                    catch(CreditoSistemaExcepcion ex)
                    {
                        
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }
                    

                    Assert.IsTrue(pago.PagoId == 0, "El pago se aplico, pero no debio pasar");
                }

            }
        }

        [Test, Order(7)]
        public void ObtenerCreditosPendientes()
        {
            var lista = serviceCreditos.BuscarCreditosPendientesPago();

            Assert.IsTrue(lista.Count > 0, "No se encontraron creditos pendientes");

        }
    }
}
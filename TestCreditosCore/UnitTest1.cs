using NUnit.Framework;
using CreditosCore;
using CreditosCore.Controllers.Clientes;
using System.Linq;
using CreditosCore.Controllers.Creditos;
using System.Collections.Generic;
using CreditosCore.Controllers.Pagos;

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
                Nombre = Faker.Name.FullName()
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
            var datosCredito = new CreditoViewModel()
            {
                cliente = clienteDefault,
                credito = new CreditosModel()
                {
                    ClienteId = clienteDefault.ClienteId,
                    ComisionFaltaPago = 20,
                    DescuentoPagoFinal = 30,
                    FechaCreacion = System.DateTime.Today,
                    FechaModificacion = System.DateTime.Today,
                    Folio = "111",
                    MontoInteres = 350,
                    MontoPago = 150,
                    MontoPrestamo = 1000,
                    MontoTotal = 1350,
                    Plazos = 9,
                    TipoPago = 1
                }
            };

            var idCredito = serviceCreditos.GuardarCredito(datosCredito);

            bool Comparacion = (idCredito > 0);

            Assert.IsTrue(Comparacion, "No se pudo registrar el credito");

            if (Comparacion)
            {
                this.creditoAgregados.Add(datosCredito.credito);
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
    }
}
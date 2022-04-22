using NUnit.Framework;
using CreditosCore;
using CreditosCore.Controllers.Clientes;
using System.Linq;
using CreditosCore.Controllers.Creditos;
using System.Collections.Generic;

namespace TestCreditosCore
{
    public class Tests
    {
        ClientesService serviceCliente;
        CreditosService serviceCreditos;

        List<ClientesModel> clientesTemporales;
        List<ClientesModel> ClientesAgregados;

        public Tests()
        {
            serviceCliente = new ClientesService();
            serviceCreditos = new CreditosService();
            ClientesAgregados = new List<ClientesModel>();
            clientesTemporales = new List<ClientesModel>();
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

        [Test]
        public void AgregarCredito()
        {
            //obtener un cliente por default
            var clienteDefault = serviceCliente.ObtenerListaClientes().FirstOrDefault();
            var idCredito = serviceCreditos.GuardarCredito(new CreditoViewModel()
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
            });

            Assert.IsTrue(idCredito > 0, "No se pudo registrar el credito");

        }
    }
}
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

        [SetUp]
        public void Setup()
        {
            serviceCliente = new ClientesService();
            serviceCreditos = new CreditosService();
            ClientesAgregados = new List<ClientesModel>();
            clientesTemporales = new List<ClientesModel>();
        }

   
        [Test]
        public void AgregarCliente()
        {
            var cliente = new ClientesModel()
            {
                ClienteId = 0,
                ApellidoMaterno = "Genovez",
                ApellidoPaterno = Faker.Name.Last(),
                Correo = "luisricardogz@gmail.com",
                Nombre = Faker.Name.FullName()
            };



            Assert.AreEqual(cliente.ClienteId, 0, "Cliente creado");

            clientesTemporales.Add(cliente);
            
        }

        [Test]
        public void AgregarClientesMasivos()
        {

            foreach (var cliente in clientesTemporales)
            {
                serviceCliente = new ClientesService();
                serviceCliente.AgregarCliente(cliente);

                if (cliente.ClienteId > 0)
                {
                    ClientesAgregados.Add(cliente);
               
                }
            }


            Assert.IsTrue(true);
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
                    ClienteId = 0,
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
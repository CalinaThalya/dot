using System.Collections.Generic;
using System.Linq;
using Ecommerce.Cliente.Application.Services;
using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces;
using Ecommerce.Cliente.Domain.Interfaces.Dtos;
using Moq;
using Xunit;

namespace Ecommerce.Cliente.Tests
{
    public class ClienteApplicationServiceTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock;
        private readonly ClienteApplicationService _clienteService;

        public ClienteApplicationServiceTests()
        {
            _repositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void AdicionarCliente_DeveRetornarClienteEntity_QuandoAdicionarComSucesso()
        {
            // Arrange
            var clienteDtoMock = new Mock<IClienteDto>();
            clienteDtoMock.Setup(c => c.Nome).Returns("João");
            clienteDtoMock.Setup(c => c.SobreNome).Returns("Silva");
            clienteDtoMock.Setup(c => c.Email).Returns("joao.silva@example.com");
            clienteDtoMock.Setup(c => c.Idade).Returns(30);

            var clienteEsperado = new ClienteEntity { Nome = "João", SobreNome = "Silva", Email = "joao.silva@example.com", Idade = 30 };
            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<ClienteEntity>())).Returns(clienteEsperado);

            // Act
            var resultado = _clienteService.AdicionarCliente(clienteDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }

        [Fact]
        public void EditarCliente_DeveRetornarClienteEntity_QuandoEditarComSucesso()
        {
            // Arrange
            var clienteDtoMock = new Mock<IClienteDto>();
            clienteDtoMock.Setup(c => c.Nome).Returns("Maria");
            clienteDtoMock.Setup(c => c.SobreNome).Returns("Oliveira");
            clienteDtoMock.Setup(c => c.Email).Returns("maria.oliveira@example.com");
            clienteDtoMock.Setup(c => c.Idade).Returns(25);

            var clienteEsperado = new ClienteEntity { Id = 1, Nome = "Maria", SobreNome = "Oliveira", Email = "maria.oliveira@example.com", Idade = 25 };
            _repositoryMock.Setup(r => r.Editar(It.IsAny<ClienteEntity>())).Returns(clienteEsperado);

            // Act
            var resultado = _clienteService.EditarCliente(1, clienteDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Id, resultado.Id);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }

        [Fact]
        public void ObterClientePorId_DeveRetornarClienteEntity_QuandoClienteExiste()
        {
            // Arrange
            var clienteEsperado = new ClienteEntity { Id = 1, Nome = "Carlos", SobreNome = "Santos", Email = "carlos.santos@example.com", Idade = 40 };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(clienteEsperado);

            // Act
            var resultado = _clienteService.ObterClientePorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Id, resultado.Id);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }

        [Fact]
        public void ObterTodosClientes_DeveRetornarListaDeClientes_QuandoExistiremClientes()
        {
            // Arrange
            var clientesEsperados = new List<ClienteEntity>
            {
                new ClienteEntity { Id = 1, Nome = "Paula", SobreNome = "Martins", Email = "paula.martins@example.com", Idade = 35 },
                new ClienteEntity { Id = 2, Nome = "Rafael", SobreNome = "Ferreira", Email = "rafael.ferreira@example.com", Idade = 28 }
            };
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(clientesEsperados);

            // Act
            var resultado = _clienteService.ObterTodosClientes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clientesEsperados.Count, resultado.Count());
            Assert.Equal(clientesEsperados[0].Nome, resultado.First().Nome);
            Assert.Equal(clientesEsperados[1].Nome, resultado.Last().Nome);
        }

        [Fact]
        public void RemoverCliente_DeveRetornarClienteEntity_QuandoRemoverComSucesso()
        {
            // Arrange
            var clienteEsperado = new ClienteEntity { Id = 1, Nome = "Rogério", SobreNome = "Souza", Email = "rogerio.souza@example.com", Idade = 45 };
            _repositoryMock.Setup(r => r.Remover(1)).Returns(clienteEsperado);

            // Act
            var resultado = _clienteService.RemoverCliente(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Id, resultado.Id);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }
    }
}

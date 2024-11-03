using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndDataTech.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using BackEndDataTech.Controllers;
using Microsoft.AspNetCore.Mvc;
using BackEndDataTech.Services;

namespace BackEndDataTech.Tests
{
    public class ClienteControllerTests
    {
        private readonly DbContextOptions<Contexto> _options;
        private readonly Contexto _context;
        private readonly ClienteController _clienteController;
        private readonly Mock<IAuthenticationService> _authenticationServiceMock;

        public ClienteControllerTests()
        {
            _options = new DbContextOptionsBuilder<Contexto>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new Contexto(_options);
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _clienteController = new ClienteController(_context, _authenticationServiceMock.Object);
        }

        [Fact]
        public async Task Index_DeveRetornarListaDeClientes()
        {
            // Arrange
            _context.Cliente.Add(new Cliente { Nome = "Cliente1", Email = "cliente1@example.com" });
            _context.Cliente.Add(new Cliente { Nome = "Cliente2", Email = "cliente2@example.com" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _clienteController.Index() as ViewResult;
            var clientes = result?.Model as List<Cliente>;

            // Assert
            Assert.NotNull(clientes);
            Assert.Equal(2, clientes.Count);
        }

        [Fact]
        public async Task Details_DeveRetornarClienteQuandoIdExiste()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Cliente Teste", Email = "test@example.com", Senha = "password" };
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _clienteController.Details(cliente.id) as ViewResult;
            var clienteResult = result?.Model as Cliente;

            // Assert
            Assert.NotNull(clienteResult);
            Assert.Equal(cliente.Nome, clienteResult.Nome);
        }

        [Fact]
        public async Task Create_PostDeveAdicionarClienteQuandoModeloEhValido()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Novo Cliente", Email = "novo@example.com", Senha = "1234" };

            // Act
            var result = await _clienteController.Create(cliente) as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, _context.Cliente.Count());
        }

        [Fact]
        public async Task Edit_DeveAtualizarClienteQuandoIdEModeloSaoValidos()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Cliente Original", Email = "original@example.com", Senha = "senha" };
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            cliente.Nome = "Cliente Atualizado";

            // Act
            var result = await _clienteController.Edit(cliente.id, cliente) as RedirectToActionResult;
            var clienteAtualizado = await _context.Cliente.FindAsync(cliente.id);

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Cliente Atualizado", clienteAtualizado.Nome);
        }

        [Fact]
        public async Task DeleteConfirmed_DeveRemoverClienteQuandoIdEhValido()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Cliente a Deletar", Email = "delete@example.com", Senha = "senha" };
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _clienteController.DeleteConfirmed(cliente.id) as RedirectToActionResult;
            var clienteNoDb = await _context.Cliente.FindAsync(cliente.id);

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Null(clienteNoDb);
        }

        [Fact]
        public async Task Login_DeveRedirecionarParaIndex_QuandoAutenticacaoForBemSucedida()
        {
            // Arrange
            _authenticationServiceMock
                .Setup(service => service.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _clienteController.Login("cliente@example.com", "senha123") as RedirectToActionResult;

            // Assert
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Login_DeveAdicionarErroDeModelo_QuandoAutenticacaoFalha()
        {
            // Arrange
            _authenticationServiceMock
                .Setup(service => service.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _clienteController.Login("cliente@example.com", "senhaErrada") as ViewResult;

            // Assert
            Assert.False(_clienteController.ModelState.IsValid);
            Assert.Contains("Invalid login attempt.", _clienteController.ModelState[""].Errors[0].ErrorMessage);
        }
    }
}

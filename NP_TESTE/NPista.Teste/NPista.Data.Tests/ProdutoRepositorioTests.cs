using Microsoft.EntityFrameworkCore;
using NPista.Core.Models;
using NPista.Data.EFCore.Context;
using NPista.Data.EFCore.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NPista.Teste.NPista.Data.Tests
{

    public class ProdutoRepositorioTests
    {
        private readonly Contexto _context;
        private readonly ProdutoRepositorio _repository;

        public ProdutoRepositorioTests()
        {
            var options = new DbContextOptionsBuilder<Contexto>()
               .UseInMemoryDatabase(databaseName: "NPista")
               .Options;

            _context = new Contexto(options);

            _repository = new ProdutoRepositorio(_context);
        }

        //GetProdutoDetailsByIdAsync
        [Fact]
        public async Task TestaABuscaDeUmProdutoDetalhadoPeloId()
        {
            //Arrange
            var produtoMock = new Produto() { Id = 100, Nome = "Livro", ValorUnitario = 200, QtdeEstoque = 10 };
            var idProduto = 100;

            await _context.Produtos.AddAsync(produtoMock);
            await _context.SaveChangesAsync();

            //Act
            var produto = await _repository.GetProdutoDetailsByIdAsync(idProduto);

            //Asseert
            Assert.NotNull(produto);
            Assert.Equal(produtoMock.Nome, produto.Nome);
            Assert.Equal(produtoMock.ValorUnitario, produto.ValorUnitario);
            Assert.Equal(produtoMock.QtdeEstoque, produto.QtdeEstoque);
        }

        //GetAllProdutosAsync()
        [Fact]
        public async Task TestaABuscaDeTodosProdutos()
        {
            //Arrange
            var data = new List<Produto>()
            {
                new Produto() { Nome =  "Cadeira", ValorUnitario = 200, QtdeEstoque = 10},
                new Produto() { Nome =  "Mesa", ValorUnitario = 300, QtdeEstoque = 50},
                new Produto() { Nome =  "Prato", ValorUnitario = 100, QtdeEstoque = 1000},
                new Produto() { Nome =  "Fogão", ValorUnitario = 800, QtdeEstoque = 30},
            }.AsQueryable();

            await _context.Produtos.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            //Act
            var produtos = await _repository.GetAllProdutosAsync();

            //Assert
            Assert.True(produtos.Count() > 0);

        }
        //ExcluirProduto
        [Fact]
        public async Task TestaAExclusaoDeProdutoPeloId()
        {
            //Arrange
            var produtoMock = new Produto() { Id = 500, Nome = "Capacete", ValorUnitario = 200, QtdeEstoque = 10 };
            var idProduto = 300;

            await _context.Produtos.AddAsync(produtoMock);
            await _context.SaveChangesAsync();

            //Act
            var record = Record.ExceptionAsync(async() => await _repository.ExcluirProduto(idProduto));

            //Asseert
            Assert.Null(record.Exception);
            
        }

        [Fact]
        public async Task TestaProdutoBaixaDoEstoque()
        {
            //Arrange
            var produtoMock = new Produto() { Id = 600, Nome = "Espada", ValorUnitario = 200, QtdeEstoque = 10 };
            var idProduto = 300;
            var valor = 5;

            await _context.Produtos.AddAsync(produtoMock);
            await _context.SaveChangesAsync();

            //Act
            var record = Record.ExceptionAsync(async () => await _repository.BaixaEstoqueByIdAsync(idProduto, valor));

            //Asseert
            Assert.Null(record.Exception);

        }

        //GetProdutoByIdAsync
        [Fact]
        public async Task TestaABuscaDeUmProdutoPeloId()
        {
            //Arrange
            var produtoMock = new Produto() { Id = 200, Nome = "Livro 2", ValorUnitario = 200, QtdeEstoque = 10 };
            var idProduto = 200;

            await _context.Produtos.AddAsync(produtoMock);
            await _context.SaveChangesAsync();

            //Act
            var produto = await _repository.GetProdutoByIdAsync(idProduto);

            //Asseert
            Assert.NotNull(produto);
            Assert.Equal(produtoMock.Nome, produto.Nome);
            Assert.Equal(produtoMock.ValorUnitario, produto.ValorUnitario);
            Assert.Equal(produtoMock.QtdeEstoque, produto.QtdeEstoque);
        }


    }
}

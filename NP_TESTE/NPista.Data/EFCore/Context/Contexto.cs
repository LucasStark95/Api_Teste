using Microsoft.EntityFrameworkCore;
using NPista.Core.Models;
using System;

namespace NPista.Data.EFCore.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<Venda>()
                .Property(c => c.DataCompra)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Vendas)
                .WithOne(c => c.Produto)
                .HasForeignKey(c => c.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cartao>()
                .HasMany(c => c.Compras)
                .WithOne(ct => ct.Cartao)
                .HasForeignKey(c => c.CartaoId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    
    }
}

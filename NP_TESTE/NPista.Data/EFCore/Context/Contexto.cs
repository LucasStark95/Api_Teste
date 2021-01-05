using Microsoft.EntityFrameworkCore;
using NPista.Core.Models;
using System;

namespace NPista.Data.EFCore.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Compra> Compras { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<Compra>()
                .Property(c => c.DataCompra)
                .HasDefaultValue(DateTime.Now);
            
            base.OnModelCreating(modelBuilder);
        }
    
    }
}

using Microsoft.EntityFrameworkCore;
using NPista.Core.Models;

namespace NPista.Data.EFCore.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            base.OnModelCreating(modelBuilder);
        }
    
    }
}

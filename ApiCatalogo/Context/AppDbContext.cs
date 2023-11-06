using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context;

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Produto>? Produtos {get; set; }
        public DbSet<Categoria>? Categorias {get; set; }

        protected override void OnModelCreating(ModelBuilder mB)
        {
            //Categoria
            mB.Entity<Categoria>().HasKey(c => c.CategoriaID);
            mB.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            mB.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(150).IsRequired();
            
            //Produto
            mB.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100).IsRequired();
            mB.Entity<Produto>().Property(p => p.Preco).HasPrecision(14,2);
            mB.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(150).IsRequired();
            mB.Entity<Produto>().Property(p => p.Imagem).HasMaxLength(150).IsRequired();

            //Relacionamento
            mB.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

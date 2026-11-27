using Microsoft.EntityFrameworkCore;
using Tienda_Refaciones_computadora.Models;

namespace Tienda_Refaciones_computadora.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Memorias" },
                new Categoria { Id = 2, Nombre = "Almacenamiento" },
                new Categoria { Id = 3, Nombre = "Fuentes" },
                new Categoria { Id = 4, Nombre = "Periféricos" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Memoria RAM 8GB DDR4", Marca = "Kingston", Precio = 1800m, Stock = 15, CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "SSD 500GB NVMe", Marca = "Samsung", Precio = 3500m, Stock = 10, CategoriaId = 2 },
                new Producto { Id = 3, Nombre = "Fuente de poder 600W", Marca = "Corsair", Precio = 4200m, Stock = 6, CategoriaId = 3 },
                new Producto { Id = 4, Nombre = "Teclado mecánico", Marca = "Redragon", Precio = 2300m, Stock = 12, CategoriaId = 4 }
            );
        }
    }
}
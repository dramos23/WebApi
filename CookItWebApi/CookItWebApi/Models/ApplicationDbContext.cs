using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<PasoReceta> PasoRecetas { get; set; }
        public DbSet<UserInfo> Usuarios { get; set; }
        public DbSet<IngredienteReceta> IngredienteRecetas { get; set; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Cliente>();


        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Ingrediente>()
                .HasIndex(u => u._Nombre).IsUnique();
            modelBuilder.Entity<IngredienteReceta>()
                .ToTable("IngredientesRecetas");
            modelBuilder.Entity<IngredienteReceta>()
                .HasKey(c => new { c._IdIngrediente, c._IdReceta });
            modelBuilder.Entity<PasoReceta>()
                .HasKey(c => new { c._IdPasoReceta, c._IdReceta });
            base.OnModelCreating(modelBuilder);
        }
    }


}

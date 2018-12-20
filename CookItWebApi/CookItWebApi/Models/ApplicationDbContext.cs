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
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<IngredienteUsuario> IngredienteUsuarios { get; set; }
        public DbSet<ComentarioReceta> ComentarioRecetas { get; set; }
        public DbSet<HistorialReceta> HistorialRecetas { get; set; }
        public DbSet<RecetaFavorita> RecetasFavoritas { get; set; }

        public DbSet<TipoIngrediente> TiposIngredientes { get; set; }


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
                .HasKey(c => new { c._IdReceta, c._IdIngrediente });
            modelBuilder.Entity<PasoReceta>()
                .HasKey(c => new { c._IdPasoReceta, c._IdReceta });
            modelBuilder.Entity<PasoReceta>()
                .Property(f => f._IdPasoReceta).ValueGeneratedOnAdd();
            modelBuilder.Entity<IngredienteUsuario>()
                .ToTable("IngredientesUsuarios");
            modelBuilder.Entity<IngredienteUsuario>()
                .HasKey(c => new { c._IdIngrediente, c._Email });
            modelBuilder.Entity<ComentarioReceta>()
                .HasKey(c => new { c._IdReceta, c._IdComentario });
            modelBuilder.Entity<ComentarioReceta>()
                .Property(f => f._IdComentario).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistorialReceta>()
                .HasKey(c => new { c._IdHistorialReceta, c._EmailUsuario });
            modelBuilder.Entity<HistorialReceta>()
                .Property(f => f._IdHistorialReceta).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistorialReceta>()
                .Ignore(p => p._Receta);
            modelBuilder.Entity<RecetaFavorita>()
                .HasKey(c => new { c._EmailUsuario, c._IdReceta });
            modelBuilder.Entity<RecetaFavorita>()
                .Ignore(p => p._Receta);
            modelBuilder.Entity<TipoIngrediente>()
                .Property(f => f._IdTipo).ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);

        }
    }


}

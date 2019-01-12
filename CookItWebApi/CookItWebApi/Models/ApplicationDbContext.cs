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
        public DbSet<MomentoDia> MomentosDias { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<PasoReceta> PasoRecetas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<IngredienteReceta> IngredienteRecetas { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<IngredienteUsuario> IngredienteUsuarios { get; set; }
        public DbSet<ComentarioReceta> ComentarioRecetas { get; set; }
        public DbSet<HistorialReceta> HistorialRecetas { get; set; }
        public DbSet<RecetaFavorita> RecetasFavoritas { get; set; }    
        public DbSet<TipoIngrediente> TiposIngredientes { get; set; }
        public DbSet<Estacion> Estaciones { get; set; }



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
                .Property(f => f._IdIngrediente)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Ingrediente>()
                .HasKey(r => r._IdIngrediente);
            modelBuilder.Entity<Ingrediente>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<Receta>()
                .HasKey(r => r._IdReceta);
            modelBuilder.Entity<Receta>()
                .Property(r => r._IdReceta)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<IngredienteReceta>()
                .ToTable("IngredientesRecetas");                           
            modelBuilder.Entity<IngredienteReceta>()                                
                .HasKey(c => new { c._IdReceta , c._IdIngrediente });
            modelBuilder.Entity<IngredienteReceta>()
                .HasOne(c => c._Receta)
                .WithMany(r => r._IngredientesReceta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PasoReceta>()
                .HasKey(c => new { c._IdPasoReceta, c._IdReceta });
            modelBuilder.Entity<PasoReceta>()
                .Property(r => r._IdPasoReceta)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<IngredienteUsuario>()
                .ToTable("IngredientesUsuarios");
            modelBuilder.Entity<IngredienteUsuario>()
                .HasKey(c => new { c._IdIngrediente, c._Email });


            modelBuilder.Entity<ComentarioReceta>()
                .HasKey(c => new { c._IdReceta, c._IdComentario });
            modelBuilder.Entity<ComentarioReceta>()
                .Property(f => f._IdComentario)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<HistorialReceta>()
                .Property(f => f._IdHistorialReceta).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistorialReceta>()
                .HasKey(c => new { c._Email, c._IdReceta, c._IdHistorialReceta });
            modelBuilder.Entity<HistorialReceta>()
                .HasOne(a => a._Receta)
                .WithOne().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistorialReceta>()
                .HasOne(a => a._Usuario)
                .WithOne().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecetaFavorita>()
                .HasKey(c => new { c._Email, c._IdReceta });
            modelBuilder.Entity<RecetaFavorita>()
                .HasOne(a => a._Receta)
                .WithOne().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecetaFavorita>()
                .HasOne(a => a._Usuario)
                .WithOne().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoIngrediente>()
                .Property(f => f._IdTipoIngrediente)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TipoIngrediente>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<MomentoDia>()
                .Property(f => f._IdMomentoDia)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<MomentoDia>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<Estacion>()
                .Property(f => f._IdEstacion)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Estacion>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasKey(c => c._Email);

            //modelBuilder.Entity<Receta>()
            //    .HasOne(a => a._MomentoDia)
            //    .WithOne().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Receta>()
            //    .HasOne(a => a._Estacion)
            //    .WithOne().OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);

        }


    }


}

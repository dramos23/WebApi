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
        public DbSet<EstadoReto> EstadosRetos { get; set; }
        public DbSet<Reto> Retos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

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
                .UseSqlServerIdentityColumn();
            modelBuilder.Entity<Ingrediente>()
                .HasKey(r => r._IdIngrediente);
            modelBuilder.Entity<Ingrediente>()
                .HasIndex(u => u._Nombre).IsUnique();
            modelBuilder.Entity<Ingrediente>()
                .HasOne(a => a._Estacion)
                .WithMany()
                .HasForeignKey(c => c._IdEstacion)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ingrediente>()
                .HasOne(a => a._TipoIngrediente)
                .WithMany()
                .HasForeignKey(c => c._IdTipoIngrediente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receta>()
                .HasKey(r => r._IdReceta);
            modelBuilder.Entity<Receta>()
                .Property(r => r._IdReceta)
                .UseSqlServerIdentityColumn();
            modelBuilder.Entity<Receta>()
                .HasOne(a => a._Perfil)
                .WithMany()
                .HasForeignKey(a => a._Email)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Receta>()
                .HasOne(a => a._MomentoDia)
                .WithMany()
                .HasForeignKey(a => a._IdMomentoDia)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Receta>()
                .HasOne(a => a._Estacion)
                .WithMany()
                .HasForeignKey(a => a._IdEstacion)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Receta>()
                .Ignore(p => p._ListaComentariosReceta);
            modelBuilder.Entity<Receta>()
                .Ignore(p => p._ListaIngredientesReceta);
            modelBuilder.Entity<Receta>()
                .Ignore(p => p._ListaPasosReceta);


            modelBuilder.Entity<IngredienteReceta>()
                .ToTable("IngredientesRecetas");                           
            modelBuilder.Entity<IngredienteReceta>()                                
                .HasKey(c => new { c._IdReceta , c._IdIngrediente });
            modelBuilder.Entity<IngredienteReceta>()
                .HasOne(c => c._Receta)
                .WithMany(r => r._ListaIngredientesReceta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IngredienteUsuario>()
                .ToTable("IngredientesUsuarios");
            modelBuilder.Entity<IngredienteUsuario>()
                .HasKey(c => new { c._Email, c._IdIngrediente });
            modelBuilder.Entity<IngredienteUsuario>()
                .HasOne(c => c._Perfil)                
                .WithMany(c => c._ListaIngredientesUsuario)
                .HasForeignKey(c => c._Email)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<IngredienteUsuario>()
                .HasOne(c => c._Ingrediente)
                .WithMany()
                .HasForeignKey(c => c._IdIngrediente)
                .OnDelete(DeleteBehavior.Restrict);            

            modelBuilder.Entity<PasoReceta>()
                .HasKey(c => new { c._IdPasoReceta, c._IdReceta });
            modelBuilder.Entity<PasoReceta>()
                .Property(r => r._IdPasoReceta)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<PasoReceta>()
                .HasOne(c => c._Receta)
                .WithMany(c => c._ListaPasosReceta)
                .HasForeignKey(c => c._IdReceta)
                .OnDelete(DeleteBehavior.Restrict);            

            modelBuilder.Entity<ComentarioReceta>()
                .HasKey(c => c._IdComentario);
            modelBuilder.Entity<ComentarioReceta>()
                .Property(f => f._IdComentario)
                .UseSqlServerIdentityColumn();
            modelBuilder.Entity<ComentarioReceta>()
                .HasOne(c => c._Receta)
                .WithMany(c => c._ListaComentariosReceta)
                .HasForeignKey(a => a._IdReceta)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ComentarioReceta>()
                .HasOne(c => c._Perfil)
                .WithMany()
                .HasForeignKey(a => a._Email)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<HistorialReceta>()
                .HasKey(c => new { c._Email, c._IdReceta, c._FechaHora });
            modelBuilder.Entity<HistorialReceta>()
                .HasOne(c => c._Receta)
                .WithMany()
                .HasForeignKey(a => a._IdReceta)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistorialReceta>()
                .HasOne(c => c._Usuario)
                .WithMany(c => c._ListaHistorialRecetas)
                .HasForeignKey(a => a._Email)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecetaFavorita>()
                .HasKey(c => new { c._Email, c._IdReceta });
            modelBuilder.Entity<RecetaFavorita>()
                .HasOne(a => a._Receta)
                .WithOne().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecetaFavorita>()
                .HasOne(a => a._Perfil)
                .WithMany(a => a._ListaRecetasFavoritas)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoIngrediente>()
                .Property(f => f._IdTipoIngrediente)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TipoIngrediente>()
                .HasKey(c => c._IdTipoIngrediente);
            modelBuilder.Entity<TipoIngrediente>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<MomentoDia>()
                .Property(f => f._IdMomentoDia)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<MomentoDia>()
                .HasKey(c => c._IdMomentoDia);
            modelBuilder.Entity<MomentoDia>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<Estacion>()
                .Property(f => f._IdEstacion)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Estacion>()
                .HasKey(c => c._IdEstacion);
            modelBuilder.Entity<Estacion>()
                .HasIndex(u => u._Nombre).IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasKey(c => c._Email);
            modelBuilder.Entity<Usuario>()
                .Ignore(p => p._Perfil);
            modelBuilder.Entity<Usuario>()
                .Ignore(p => p._Password);
            modelBuilder.Entity<Usuario>()
                .Ignore(p => p._ListaHistorialRecetas);


            modelBuilder.Entity<Perfil>()
                .HasKey(c => c._Email);            
            modelBuilder.Entity<Perfil>()
                .HasOne(c => c._Usuario) 
                .WithMany()
                .HasForeignKey(c => c._Email)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Perfil>()
                .Ignore(p => p._ListaIngredientesUsuario);
            modelBuilder.Entity<Perfil>()
                .Ignore(p => p._ListaNotificaciones);
            modelBuilder.Entity<Perfil>()
                .Ignore(p => p._ListaRetos);
            modelBuilder.Entity<Perfil>()
                .Ignore(p => p._ListaRecetasFavoritas);


            modelBuilder.Entity<EstadoReto>()
                .Property(f => f._IdEstadoReto)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<EstadoReto>()
                .HasKey(u => u._IdEstadoReto);
            modelBuilder.Entity<EstadoReto>()
                .HasIndex(u => u._Estado).IsUnique();

            modelBuilder.Entity<Reto>()
                .HasKey(c => new { c._EmailUsuOri, c._EmialUsuDes, c._RecetaId, c._Cumplido });
            modelBuilder.Entity<Reto>()
                .HasOne(c => c._PerfilUsuOri)
                .WithMany(c => c._ListaRetos)
                .HasForeignKey(c => c._EmailUsuOri)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Reto>()
                .HasOne(c => c._PerfilUsuDes)
                .WithMany()
                .HasForeignKey(c => c._EmialUsuDes)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Reto>()
                .HasOne(a => a._EstadoReto)
                .WithMany()
                .HasForeignKey(c => c._IdEstadoReto)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Reto>()
                .HasOne(a => a._Receta)
                .WithMany()
                .HasForeignKey(c => c._RecetaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notificacion>()
                .HasKey(n => n._NotificacionId);
            modelBuilder.Entity<Notificacion>()
                .HasOne(a => a._Perfil)
                .WithMany(a => a._ListaNotificaciones)
                .HasForeignKey(c => c._Email)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);

        }


    }


}

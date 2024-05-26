using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PIA_LMP_API.Data.Models;

namespace PIA_LMP_API.Data
{
    public partial class PIALMPContext : DbContext
    {
        public PIALMPContext()
        {
        }

        public PIALMPContext(DbContextOptions<PIALMPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MascotaPerdidum> MascotaPerdida { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MascotaPerdidum>(entity =>
            {
                entity.HasKey(e => e.IdMascota)
                    .HasName("PK__MascotaP__8E068112C9BB78DD");

                entity.Property(e => e.IdMascota).HasColumnName("idMascota");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(10, 5)")
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasColumnType("decimal(10, 5)")
                    .HasColumnName("longitud");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Recompensa)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("recompensa");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A612E44E37");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

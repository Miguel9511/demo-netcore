using System;
using System.Collections.Generic;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.DataContext;

public partial class TrabajadoresPruebaContext : DbContext
{
    public TrabajadoresPruebaContext()
    {
    }

    public TrabajadoresPruebaContext(DbContextOptions<TrabajadoresPruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamento { get; set; }

    public virtual DbSet<Distrito> Distrito { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Trabajadores> Trabajadores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC0702323FD4");

            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distrito__3214EC075D50A7E2");

            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Distrito)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Distrito__IdProv__2A4B4B5E");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provinci__3214EC07FFECD8B1");

            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Provincia__IdDep__2B3F6F97");
        });

        modelBuilder.Entity<Trabajadores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trabajad__3214EC07AE6EBB77");

            entity.Property(e => e.Nombres)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Trabajado__IdDep__2C3393D0");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdDistrito)
                .HasConstraintName("FK__Trabajado__IdDis__2D27B809");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Trabajado__IdPro__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

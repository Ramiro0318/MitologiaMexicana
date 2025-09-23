using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Mitologia.Models.Entities;

public partial class MitologiaMexicanaContext : DbContext
{
    public MitologiaMexicanaContext()
    {
    }

    public MitologiaMexicanaContext(DbContextOptions<MitologiaMexicanaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CivilizacionDios> CivilizacionDios { get; set; }

    public virtual DbSet<Civilizaciones> Civilizaciones { get; set; }

    public virtual DbSet<Dioses> Dioses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=mitologia_mexicana;password=root;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CivilizacionDios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("civilizacion_dios");

            entity.HasIndex(e => e.IdCivilizacion, "civilizacion_dios_ibfk_1");

            entity.HasIndex(e => e.IdDios, "fkdos_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCivilizacion).HasColumnName("id_civilizacion");
            entity.Property(e => e.IdDios).HasColumnName("id_dios");
            entity.Property(e => e.NombreLocal)
                .HasMaxLength(100)
                .HasColumnName("nombre_local");

            entity.HasOne(d => d.IdCivilizacionNavigation).WithMany(p => p.CivilizacionDios)
                .HasForeignKey(d => d.IdCivilizacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("civilizacion_dios_ibfk_1");

            entity.HasOne(d => d.IdDiosNavigation).WithMany(p => p.CivilizacionDios)
                .HasForeignKey(d => d.IdDios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkdos");
        });

        modelBuilder.Entity<Civilizaciones>(entity =>
        {
            entity.HasKey(e => e.IdCivilizacion).HasName("PRIMARY");

            entity.ToTable("civilizaciones");

            entity.Property(e => e.IdCivilizacion).HasColumnName("id_civilizacion");
            entity.Property(e => e.Capital)
                .HasMaxLength(255)
                .HasColumnName("capital");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Lengua)
                .HasMaxLength(100)
                .HasColumnName("lengua");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PeriodoFin).HasColumnName("periodo_fin");
            entity.Property(e => e.PeriodoInicio).HasColumnName("periodo_inicio");
            entity.Property(e => e.Region)
                .HasMaxLength(255)
                .HasColumnName("region");
        });

        modelBuilder.Entity<Dioses>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dioses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Dominio)
                .HasMaxLength(150)
                .HasColumnName("dominio");
            entity.Property(e => e.Genero)
                .HasColumnType("enum('Masculino','Femenino','Dual','Otro')")
                .HasColumnName("genero");
            entity.Property(e => e.NombreGeneral)
                .HasMaxLength(100)
                .HasColumnName("nombre_general");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

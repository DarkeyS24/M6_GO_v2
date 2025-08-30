using System;
using System.Collections.Generic;
using API_M6_GO.Models;
using Microsoft.EntityFrameworkCore;

namespace API_M6_GO.Context;

public partial class Sessao6 : DbContext
{
    public Sessao6()
    {
    }

    public Sessao6(DbContextOptions<Sessao6> options)
        : base(options)
    {
    }

    public virtual DbSet<Atendimento> Atendimentos { get; set; }

    public virtual DbSet<Avaliaco> Avaliacoes { get; set; }

    public virtual DbSet<ContatoEmergencium> ContatoEmergencia { get; set; }

    public virtual DbSet<Cuidador> Cuidadors { get; set; }

    public virtual DbSet<Idoso> Idosos { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<MedicamentoAplicado> MedicamentoAplicados { get; set; }

    public virtual DbSet<Procedimento> Procedimentos { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Sessao6;User Id=sa;Password=sql;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atendimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("atendimento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.Observacoes).HasColumnName("observacoes");
            entity.Property(e => e.ProcedimentoId).HasColumnName("procedimentoId");
            entity.Property(e => e.StatusId).HasColumnName("statusId");

            entity.HasOne(d => d.Cuidador).WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.CuidadorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_atendimento_cuidador");

            entity.HasOne(d => d.Idoso).WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.IdosoId)
                .HasConstraintName("FK_atendimento_idoso");

            entity.HasOne(d => d.Procedimento).WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.ProcedimentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_atendimento_Procedimentos");

            entity.HasOne(d => d.Status).WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_atendimento_Status");
        });

        modelBuilder.Entity<Avaliaco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Avaliaco)
                .HasForeignKey<Avaliaco>(d => d.Id)
                .HasConstraintName("FK_Avaliacoes_atendimento");
        });

        modelBuilder.Entity<ContatoEmergencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("contato_emergencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");

            entity.HasOne(d => d.Idoso).WithMany(p => p.ContatoEmergencia)
                .HasForeignKey(d => d.IdosoId)
                .HasConstraintName("FK_contato_idoso");
        });

        modelBuilder.Entity<Cuidador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("Cuidador");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Cuidador)
                .HasForeignKey<Cuidador>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuidador_Usuarios");
        });

        modelBuilder.Entity<Idoso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("idoso");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nascimento).HasColumnName("nascimento");
            entity.Property(e => e.Observacoes).HasColumnName("observacoes");
            entity.Property(e => e.PesoKg)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("peso_kg");
            entity.Property(e => e.Resumo).HasColumnName("resumo");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Idoso)
                .HasForeignKey<Idoso>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idoso_Usuarios");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicamentoAplicado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("medicamento_aplicado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.Dose)
                .HasMaxLength(50)
                .HasColumnName("dose");
            entity.Property(e => e.Observacoes).HasColumnName("observacoes");

            entity.HasOne(d => d.Atendimento).WithMany(p => p.MedicamentoAplicados)
                .HasForeignKey(d => d.AtendimentoId)
                .HasConstraintName("FK_medicamento_aplicado_atendimento");

            entity.HasOne(d => d.Medicamento).WithMany(p => p.MedicamentoAplicados)
                .HasForeignKey(d => d.MedicamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_medicamento_aplicado_Medicamentos");
        });

        modelBuilder.Entity<Procedimento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ToTable("Status");

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053425EFB504").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FotoPath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Foto_Path");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

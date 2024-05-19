using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace src.MVVM.Model;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Biodata> Biodata { get; set; }

    public virtual DbSet<SidikJari> SidikJaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=Database/database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Biodata>(entity =>
        {
            entity.HasKey(e => e.Nik);

            entity.ToTable("biodata");

            entity.Property(e => e.Nik)
                .HasColumnType("VARCHAR(16)")
                .HasColumnName("NIK");
            entity.Property(e => e.Agama)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("agama");
            entity.Property(e => e.Alamat)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("alamat");
            entity.Property(e => e.GolonganDarah)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(5)")
                .HasColumnName("golongan_darah");
            entity.Property(e => e.JenisKelamin)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("jenis_kelamin");
            entity.Property(e => e.Kewarganegaraan)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("kewarganegaraan");
            entity.Property(e => e.Nama)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("nama");
            entity.Property(e => e.Pekerjaan)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("pekerjaan");
            entity.Property(e => e.StatusPerkawinan)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("status_perkawinan");
            entity.Property(e => e.TanggalLahir)
                .HasDefaultValueSql("NULL")
                .HasColumnType("DATE")
                .HasColumnName("tanggal_lahir");
            entity.Property(e => e.TempatLahir)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("tempat_lahir");
        });

        modelBuilder.Entity<SidikJari>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sidik_jari");

            entity.Property(e => e.BerkasCitra).HasColumnName("berkas_citra");
            entity.Property(e => e.Nama)
                .HasDefaultValueSql("NULL")
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("nama");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

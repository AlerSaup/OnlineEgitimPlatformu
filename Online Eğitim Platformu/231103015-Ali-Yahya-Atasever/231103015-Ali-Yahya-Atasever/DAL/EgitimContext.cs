using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class EgitimContext : DbContext
{
    public EgitimContext()
    {
    }

    public EgitimContext(DbContextOptions<EgitimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bolum> Bolums { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Izlenme> Izlenmes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Ogrencibilgi> Ogrencibilgis { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Sinif> Sinifs { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=egitim;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bolum>(entity =>
        {
            entity.ToTable("Bolum");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.HasOne(d => d.Bolum).WithMany(p => p.Categories)
                .HasForeignKey(d => d.BolumId)
                .HasConstraintName("FK_Category_Bolum");
        });

        modelBuilder.Entity<Izlenme>(entity =>
        {
            entity.ToTable("izlenme");

            entity.Property(e => e.Izlenmemiktari).HasColumnName("izlenmemiktari");
            entity.Property(e => e.OgrencibilgiId).HasColumnName("ogrencibilgiId");
            entity.Property(e => e.VideoId).HasColumnName("videoId");

            entity.HasOne(d => d.Ogrencibilgi).WithMany(p => p.Izlenmes)
                .HasForeignKey(d => d.OgrencibilgiId)
                .HasConstraintName("FK_Izlenme_Ogrencibilgi");

            entity.HasOne(d => d.Video).WithMany(p => p.Izlenmes)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK_Izlenme_Video");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("login");

            entity.Property(e => e.OgrencibilgiId).HasColumnName("ogrencibilgiId");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Ogrencibilgi).WithMany(p => p.Logins)
                .HasForeignKey(d => d.OgrencibilgiId)
                .HasConstraintName("FK_login_ogrencibilgi");

            entity.HasOne(d => d.Rank).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RankId)
                .HasConstraintName("FK_Login_Rank");
        });

        modelBuilder.Entity<Ogrencibilgi>(entity =>
        {
            entity.ToTable("ogrencibilgi");

            entity.HasOne(d => d.Bolum).WithMany(p => p.Ogrencibilgis)
                .HasForeignKey(d => d.BolumId)
                .HasConstraintName("FK_ogrencibilgi_Bolum");

            entity.HasOne(d => d.Sinif).WithMany(p => p.Ogrencibilgis)
                .HasForeignKey(d => d.SinifId)
                .HasConstraintName("FK_ogrencibilgi_Sinif");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rank__3214EC0794E072FF");

            entity.ToTable("Rank");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Sinif>(entity =>
        {
            entity.ToTable("Sinif");

            entity.Property(e => e.Sinif1).HasColumnName("Sinif");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.ToTable("video");

            entity.Property(e => e.Videolink).HasColumnName("videolink");
            entity.Property(e => e.Videotitle).HasColumnName("videotitle");

            entity.HasOne(d => d.Category).WithMany(p => p.Videos)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_video_category");

            entity.HasOne(d => d.Sinif).WithMany(p => p.Videos)
                .HasForeignKey(d => d.SinifId)
                .HasConstraintName("FK_Video_Sinif");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

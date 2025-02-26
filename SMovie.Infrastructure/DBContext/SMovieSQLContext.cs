﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMovie.Domain.Entity;
namespace SMovie.Infrastructure.DBContext
{
    public partial class SMovieSQLContext : DbContext
    {

        public SMovieSQLContext()
        {
        }

        public SMovieSQLContext(DbContextOptions<SMovieSQLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<Cast> Casts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Episode> Episodes { get; set; } = null!;
        public virtual DbSet<FeatureFilm> FeatureFilms { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieCategory> MovieCategories { get; set; } = null!;
        public virtual DbSet<Nation> Nations { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetConnectionString());
            }
        }

        private static string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

            .AddJsonFile("appsettings.json", true, true)

            .Build();

            return config["ConnectionStrings:MyCnn"]!;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .HasMaxLength(255);

                entity.Property(e => e.Username).HasColumnName("UserName")
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(255);

                entity.Property(e => e.Avatar);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PersonID");

                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.Role);

                entity.Property(e => e.Thumbnail)
                    .HasColumnName("Image")
                    .HasMaxLength(255);

                entity.Property(e => e.NamePerson)
                    .HasMaxLength(255);

                entity.Property(e => e.NationId)
                    .HasMaxLength(255)
                    .HasColumnName("NationID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.Nation)
                    .WithMany()
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK__Actor__NationID__4E88ABD4");
            });

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.MovieId })
                    .HasName("PK__Cast__E30EC3682DF2123D");

                entity.ToTable("Cast");

                entity.Property(e => e.ActorId).HasColumnName("PersonID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.CharacterName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cast__ActorID__5CD6CB2B");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cast__MovieID__5DCAEF64");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.ToTable("Episode");

                entity.HasKey(e => e.EpisodeId);

                entity.Property(e => e.EpisodeId).HasColumnName("EpisodeID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Video).HasColumnName("Video");

                entity.Property(e => e.Name)
                    .HasMaxLength(255);

                entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Episode__SeasonI__6383C8BA");
            });

            modelBuilder.Entity<FeatureFilm>(entity =>
            {
                entity.HasKey(e => e.FeatureId)
                    .HasName("PK__featuref__82230BC90FB02B10");

                entity.Property(e => e.FeatureId).HasColumnName("FeatureID");

                entity.ToTable("FeatureFilm");

                entity.Property(e => e.Name)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.DateDeleted).HasColumnType("datetime");

                entity.Property(e => e.Description);

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(255);

                entity.Property(e => e.Trailer)
                    .HasMaxLength(255);

                entity.Property(e => e.NationId)
                    .HasMaxLength(255)
                    .HasColumnName("NationID");

                entity.Property(e => e.FeatureId)
                    .HasMaxLength(255)
                    .HasColumnName("FeatureID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255);

                entity.Property(e => e.ProducedDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.VietnamName)
                    .HasMaxLength(255);

                entity.HasOne(d => d.Feature)
                    .WithMany()
                    .HasForeignKey(d => d.FeatureId)
                    .HasConstraintName("FK__Movies__FeatureI__5629CD9C");

                entity.HasOne(d => d.Nation)
                    .WithMany()
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK__Movies__NationID__571DF1D5");
            });

            modelBuilder.Entity<MovieCategory>(entity =>
            {
                entity.HasKey(mc => new { mc.MovieId, mc.CategoryId });

                entity.ToTable("MovieCategory");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__movie_cat__Categ__59063A47");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieCategories)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__movie_cat__Movie__59FA5E80");
            });

            modelBuilder.Entity<Nation>(entity =>
            {
                entity.ToTable("Nation");

                entity.Property(e => e.NationId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("Season");

                entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .HasMaxLength(255);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Season__MovieID__60A75C0F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Maksgogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Maksgogo
{
    public partial class MaksgogoContext : DbContext
    {
        public MaksgogoContext()
        {
        }

        public MaksgogoContext(DbContextOptions<MaksgogoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<OrderCart> OrderCarts { get; set; } = null!;
        public virtual DbSet<Studio> Studios { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<User_has_film> User_has_films { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= TEMP1; Database=Maksgogo; Trusted_Connection= True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.IdCartItems);

                entity.Property(e => e.IdCartItems).HasColumnName("idCartItems");

                entity.Property(e => e.IdFilm).HasColumnName("idFilm");

                entity.Property(e => e.Session).HasColumnName("session");

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_CartItems_Film");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.IdFilm);

                entity.ToTable("Film");

                entity.Property(e => e.IdFilm).HasColumnName("idFilm");

                entity.Property(e => e.AmountBougth).HasColumnName("amountBougth");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Descr)
                    .HasMaxLength(100)
                    .HasColumnName("descr");

                entity.Property(e => e.IdGenre).HasColumnName("idGenre");

                entity.Property(e => e.IdStudio).HasColumnName("idStudio");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.IsFav).HasColumnName("isFav");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("releaseDate");

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.IdGenre)
                    .HasConstraintName("FK_Film_Genre");

                entity.HasOne(d => d.IdStudioNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.IdStudio)
                    .HasConstraintName("FK_Film_Studio");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.IdGenre);

                entity.ToTable("Genre");

                entity.Property(e => e.IdGenre).HasColumnName("idGenre");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(50)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<OrderCart>(entity =>
            {
                entity.HasKey(e => e.IdOrderCart);

                entity.ToTable("OrderCart");

                entity.Property(e => e.IdOrderCart).HasColumnName("idOrderCart");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.OrderCarts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_OrderCart_User");
            });

            modelBuilder.Entity<Studio>(entity =>
            {
                entity.HasKey(e => e.IdStudio);

                entity.ToTable("Studio");

                entity.Property(e => e.IdStudio).HasColumnName("idStudio");

                entity.Property(e => e.StudioName)
                    .HasMaxLength(50)
                    .HasColumnName("studio_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Session).HasColumnName("session");
            });

            modelBuilder.Entity<User_has_film>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("User_has_film");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdUser).HasColumnName("idUser");
                entity.Property(e => e.IdFilm).HasColumnName("idFilm");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.User_has_films)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_User_has_film_User");

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany(p => p.User_has_films)
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_User_has_film_Film");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;

namespace LibraryManagementApi.Data;

using Microsoft.EntityFrameworkCore;
using LibraryManagementApi.Models;


    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        // DbSet Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<ReadingProgress> ReadingProgresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Lending> Lendings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            });

            // Book Configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookId);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Subtitle).HasMaxLength(200);
                entity.Property(b => b.Description).HasColumnType("text");
                entity.HasOne(b => b.AddedBy)
                      .WithMany(u => u.AddedBooks)
                      .HasForeignKey(b => b.AddedByUserId);
            });

            // Author Configuration
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.AuthorId);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(150);
            });

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            // BookAuthor Configuration
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(ba => ba.BookAuthorId);
                entity.HasOne(ba => ba.Book)
                      .WithMany(b => b.BookAuthors)
                      .HasForeignKey(ba => ba.BookId);

                entity.HasOne(ba => ba.Author)
                      .WithMany(a => a.BookAuthors)
                      .HasForeignKey(ba => ba.AuthorId);
            });

            // BookCategory Configuration
            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(bc => bc.BookCategoryId);
                entity.HasOne(bc => bc.Book)
                      .WithMany(b => b.BookCategories)
                      .HasForeignKey(bc => bc.BookId);

                entity.HasOne(bc => bc.Category)
                      .WithMany(c => c.BookCategories)
                      .HasForeignKey(bc => bc.CategoryId);
            });

            // ReadingProgress Configuration
            modelBuilder.Entity<ReadingProgress>(entity =>
            {
                entity.HasKey(rp => rp.ProgressId);
                entity.HasOne(rp => rp.User)
                      .WithMany(u => u.ReadingProgresses)
                      .HasForeignKey(rp => rp.UserId);

                entity.HasOne(rp => rp.Book)
                      .WithMany(b => b.ReadingProgresses)
                      .HasForeignKey(rp => rp.BookId);
            });

            // Review Configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.ReviewId);
                entity.Property(r => r.Comment).HasColumnType("text");
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId);

                entity.HasOne(r => r.Book)
                      .WithMany(b => b.Reviews)
                      .HasForeignKey(r => r.BookId);
            });

            // Wishlist Configuration
            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(w => w.WishlistId);
                entity.HasOne(w => w.User)
                      .WithMany(u => u.Wishlists)
                      .HasForeignKey(w => w.UserId);
            });

            // Lending Configuration
            modelBuilder.Entity<Lending>(entity =>
            {
                entity.HasKey(l => l.LendingId);
                entity.Property(l => l.BorrowedBy).HasMaxLength(200);
                entity.Property(l => l.BorrowedDate).IsRequired();
                entity.HasOne(l => l.Book)
                      .WithMany(b => b.Lendings)
                      .HasForeignKey(l => l.BookId);
            });
        }
    }


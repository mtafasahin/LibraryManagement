using System;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BookLoan> BookLoans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kitap-Yazar ilişkisi (Çok-Çok)
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

       modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<BookAuthor>(
                j => j.HasOne(ba => ba.Author)
                    .WithMany()
                    .HasForeignKey(ba => ba.AuthorId),
                j => j.HasOne(ba => ba.Book)
                    .WithMany()
                    .HasForeignKey(ba => ba.BookId)
            );

        // Kitap-Tür ilişkisi (Çok-Çok)
        // (Daha önceki örnekte olduğu gibi)

        // Kitap-Yayıncı ilişkisi (Bir-Bir)
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);

        // Kitap-Raf ilişkisi (Bir-Bir)
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Shelf)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);

        // Kullanıcı-Kitap Ödünç Verme İlişkisi
        // Kitap-Ödünç İlişkisi
        modelBuilder.Entity<BookLoan>()
            .HasOne(bl => bl.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(bl => bl.BookId);

        // Diğer ilişkiler ve yapılandırmalar...
    }
}

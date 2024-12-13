using System;
using System.Text.Json.Serialization;

namespace LibraryManagementApi.Models;
public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? SubTitle { get; set; } // Alt başlık
    public string? Description { get; set; } // Detaylı açıklama
    public bool IsPhysical { get; set; } = false; // Fiziksel veya dijital olma durumu
    public bool ExistInLibrary { get; set; } = false; // Fiziksel veya dijital olma durumu
    public string? ISBN { get; set; }
    public int PublicationYear { get; set; }
    public int PageCount { get; set; } // Sayfa sayısı
    public string? FileFormat { get; set; } // Dijital kitaplar için dosya formatı
    public long FileSize { get; set; } // Dijital kitaplar için dosya boyutu    
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>(); // Kitap yorumları
    public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
    public int? PublisherId { get; set; } // Yayıncıya ait yabancı anahtar
    public Publisher? Publisher { get; set; }
    public int? ShelfId { get; set; } // Yayıncıya ait yabancı anahtar
    public Shelf? Shelf { get; set; }
    public string? Edition { get; set; }
    public string? CoverImageUrl { get; set; }
}

public class BookLoan
{
    public int Id { get; set; }
    public required int BookId { get; set; }
    public required string BorrowerName { get; set; } // Sistemde kayıtlı olmayan kullanıcı için
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Notes { get; set; }
    public required Book Book { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Biography { get; set; }
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class BookAuthor
{
    public required int BookId { get; set; }
    public required int AuthorId { get; set; }

    public Book? Book { get; set; }
    public Author? Author { get; set; }
}
public class Publisher
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Shelf
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}


public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }    
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public Role? Role { get; set; }
    public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>(); // Bir kullanıcının tüm ödünç aldığı kitaplar 
    public ICollection<Book> Wishlists { get; set; } = new List<Book>();

}

public class Review
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public required User User { get; set; }
    public required Book Book { get; set; }
}


public enum Role
{
    Admin,
    User
}
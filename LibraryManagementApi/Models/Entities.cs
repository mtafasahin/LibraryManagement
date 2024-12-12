using System;

namespace LibraryManagementApi.Models;

public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public required string UserType { get; set; } // Admin, Child, etc.

    public ICollection<ReadingProgress> ReadingProgresses { get; set; } = new List<ReadingProgress>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    public ICollection<Book> AddedBooks { get; set; } = new List<Book>(); // Kullanıcının eklediği kitaplar
}


public class Book
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public bool IsDigital { get; set; }
    public string? ISBN { get; set; }
    public int PageCount { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? FileExtension { get; set; } // Digital kitaplar için
    public long? FileSize { get; set; } // Byte cinsinden
    public int AddedByUserId { get; set; } // FK to Users
    public User? AddedBy { get; set; }
    public bool IsInLibrary { get; set; } = true; // Kitabın kütüphanede olup olmadığını belirten özellik


    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    public ICollection<Lending> Lendings { get; set; } = new List<Lending>(); // Kitap ödünç verilmiş mi?
    public ICollection<ReadingProgress> ReadingProgresses { get; set; } = new List<ReadingProgress>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

public class Author
{
    public int AuthorId { get; set; }
    public required string Name { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}

public class Category
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }

    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}

public class BookAuthor
{
    public int BookAuthorId { get; set; }
    public int BookId { get; set; }
    public required Book Book { get; set; }
    public int AuthorId { get; set; }
    public required Author Author { get; set; }
}

public class BookCategory
{
    public int BookCategoryId { get; set; }
    public int BookId { get; set; }
    public required Book Book { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}

public class ReadingProgress
{
    public int ProgressId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int BookId { get; set; }
    public required Book Book { get; set; }
    public int PagesRead { get; set; }
    public int TimeSpentMinutes { get; set; }
    public DateTime LastUpdated { get; set; }
}

public class Review
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int BookId { get; set; }
    public required Book Book { get; set; }
    public required string Comment { get; set; }
    public int? Rating { get; set; }
}

public class Wishlist
{
    public int WishlistId { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int BookId { get; set; } // Kitap ID'si artık zorunlu
    public required Book Book { get; set; } // Kitap referansı    public string? Title { get; set; } // Henüz kütüphanede olmayan kitaplar için
}

public class Lending
{
    public int LendingId { get; set; }
    public int BookId { get; set; } // FK to Books
    public required Book Book { get; set; }
    public required string BorrowedBy { get; set; } // Kime ödünç verildi
    public DateTime BorrowedDate { get; set; } // Ödünç veriliş tarihi
    public DateTime? ReturnedDate { get; set; } // İade tarihi (null ise ödünçte)
}
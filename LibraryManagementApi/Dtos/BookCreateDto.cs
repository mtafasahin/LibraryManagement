using System;

namespace LibraryManagementApi.Dtos;

public class AuthorDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Biography { get; set; }
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
}

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Role { get; set; }
    
}
public class ReviewDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto User { get; set; }    
}


public class BookLoanDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int? UserId { get; set; } // Sistemde kayıtlı kullanıcı için
    public string BorrowerName { get; set; } // Sistemde kayıtlı olmayan kullanıcı için
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Notes { get; set; }
    public UserDto User { get; set; } // Sistemde kayıtlı kullanıcıya ilişki
}

public class PublisherDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ShelfDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class BookDto
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public string? SubTitle { get; set; } // Alt başlık
    public string? Description { get; set; } // Detaylı açıklama
    public bool IsPhysical { get; set; } // Fiziksel veya dijital olma durumu
    public bool ExistInLibrary { get; set; } // Fiziksel veya dijital olma durumu
    public string? ISBN { get; set; }
    public string PublicationYear { get; set; }
    public int PageCount { get; set; } // Sayfa sayısı
    public string? FileFormat { get; set; } // Dijital kitaplar için dosya formatı
    public long? FileSize { get; set; } // Dijital kitaplar için dosya boyutu    
    public ICollection<AuthorDto> Authors { get; set; }  = new List<AuthorDto>();
    public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
    public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>(); // Kitap yorumları
    public ICollection<BookLoanDto> Loans { get; set; } = new List<BookLoanDto>();
    public int? PublisherId { get; set; } // Yayıncıya ait yabancı anahtar
    public PublisherDto? Publisher { get; set; }
    public int? ShelfId { get; set; } // Yayıncıya ait yabancı anahtar
    public ShelfDto? Shelf { get; set; }
    public string? Edition { get; set; }
    public string? CoverImageUrl { get; set; }
}


public class BookShelfDto
{
    public List<int> BookIds { get; set; } = new List<int>();
    public int ShelfId { get; set; }
}




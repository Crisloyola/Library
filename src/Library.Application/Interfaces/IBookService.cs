namespace Library.Application.interfaces
{
    using Library.Application.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> UpdateBookAsync(int id, CreateBookDto updateBookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<BookDto> SearchBookByTitleAsync(string title);
    }
}
using AutoMapper;
using Library.Application.DTOs;
using Library.Application.interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWOrk _unitOfWork;
        private readonly IMapper _mapper;
        
        public BookService(IUnitOfWOrk unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(createBookDto.Id);
            if (existingBook != null)
            {
                throw new InvalidOperationException("A book with the same ID already exists.");
            }
            var createbook = _mapper.Map<Books>(createBookDto);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BookDto>(createbook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var books = await _unitOfWork.Books.GetByIdAsync(id);
            if (books == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }
            var  resultado = await _unitOfWork.Books.DeleteAsync(books);
            await _unitOfWork.SaveChangesAsync();
            return resultado;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }



        public async Task<BookDto> SearchBookByTitleAsync(string title)
        {
            var book = await _unitOfWork.Books.GetBooksByTitleAsync(title);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(int id, CreateBookDto updateBookDto)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }
            _mapper.Map(updateBookDto, existingBook);
            var updateBooks = _unitOfWork.Books.UpdateAsync(existingBook); 
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BookDto>(updateBooks);
        }
    }
}
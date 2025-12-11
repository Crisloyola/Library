using AutoMapper;
using Library.Application.DTOs;
using Library.Application.interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var existingBook = (await _unitOfWork.Books.GetBooksByTitleAsync(createBookDto.Title)).FirstOrDefault();
            if (existingBook != null)
                throw new InvalidOperationException("A book with the same title already exists.");

            var book = _mapper.Map<Books>(createBookDto);
            await _unitOfWork.Books.CreateAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            var result = await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> SearchBookByTitleAsync(string title)
        {
            var book = (await _unitOfWork.Books.GetBooksByTitleAsync(title)).FirstOrDefault();
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(int id, CreateBookDto updateBookDto)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException("Book not found.");

            _mapper.Map(updateBookDto, existingBook);
            await _unitOfWork.Books.UpdateAsync(existingBook);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(existingBook);
        }
    }
}

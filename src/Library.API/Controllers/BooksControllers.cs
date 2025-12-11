using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWOrk _unitOfWork;

        public BooksController(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return Ok(books);
        }

        // GET api/books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return NotFound();

            return Ok(book);
        }

        // GET api/books/{id}/loans
        [HttpGet("{id}/loans")]
        public async Task<IActionResult> GetBookWithLoans(int id)
        {
            var book = await _unitOfWork.Books.GetBookWithLoansAsync(id);
            if (book == null) return NotFound();

            return Ok(book);
        }

        // GET api/books/search/title?title=abc
        [HttpGet("search/title")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var books = await _unitOfWork.Books.GetBooksByTitleAsync(title);
            return Ok(books);
        }

        // GET api/books/search/author?author=xyz
        [HttpGet("search/author")]
        public async Task<IActionResult> SearchByAuthor(string author)
        {
            var books = await _unitOfWork.Books.GetBooksByAuthorAsync(author);
            return Ok(books);
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Books model)
        {
            var created = await _unitOfWork.Books.CreateAsync(model);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Books model)
        {
            var existing = await _unitOfWork.Books.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = model.Title;
            existing.Author = model.Author;
            existing.ISBN = model.ISBN;
            existing.Stock = model.Stock;

            await _unitOfWork.Books.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Books.DeleteAsync(id);
            if (!result) return NotFound();

            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}

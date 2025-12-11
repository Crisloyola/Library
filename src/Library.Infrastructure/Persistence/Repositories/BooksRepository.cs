using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Library.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class BooksRepository : Repository<Books>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BooksRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Books>> GetBooksByAuthorAsync(string author)
        {
            return await _context.Books
                .Where(b => b.Author.Contains(author))
                .ToListAsync();
        }

        public async Task<IEnumerable<Books>> GetBooksByTitleAsync(string title)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<Books?> GetBookWithLoansAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Loans)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Books?> SearchBookNameAsync()
        {
            return await _context.Books
                .OrderBy(b => b.Title)
                .FirstOrDefaultAsync();
        }
    }
}

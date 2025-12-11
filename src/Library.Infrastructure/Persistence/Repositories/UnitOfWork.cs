using Library.Domain.Ports.Out;
using Library.Infrastructure.Persistence.Context;

namespace Library.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWOrk, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IBookRepository Books { get; }
        public ILoansRepository Loans { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IBookRepository booksRepository,
            ILoansRepository loansRepository)
        {
            _context = context;

            Books = booksRepository;
            Loans = loansRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
